using UnityEngine;
using System.Collections.Generic;

public class PlayerController : Singleton<PlayerController>
{
#pragma warning disable 0649

	[SerializeField] Player player;
	[SerializeField] float speed = 0.07f;

#pragma warning restore 0649

	Path path = new Path();
	Tile curGoal;

	private void Start()
	{
		path.Init(player.GetConnectedTile());
	}

	private void Update()
	{
		if (curGoal != null)
		{
			Move();
		}
	}

	void Move()
	{
		Vector3 playerPosition = player.transform.position;
		Vector3 goalPosition = curGoal.transform.position;

		float xToGoal = Mathf.Abs(playerPosition.x - goalPosition.x);
		float zToGoal = Mathf.Abs(playerPosition.z - goalPosition.z);
		float distanceToGoal = xToGoal + zToGoal;

		if (distanceToGoal <= 0.1)
		{
			player.transform.position = goalPosition;
			SetNextTile();
		}
		else
		{
			player.transform.position = Vector3.MoveTowards(playerPosition, goalPosition, speed * Time.deltaTime);
		}
	}

	void TileClicked(Tile tile)
	{
		path.AddTile(tile);

		if (curGoal == null)
		{
			SetNextTile();
		}
	}

	void SetNextTile()
	{
		curGoal = path.GetNextTile();
	}

	private void OnEnable()
	{
		Tile.TileClicked += TileClicked;
	}

	private void OnDisable()
	{
		Tile.TileClicked -= TileClicked;
	}

}
