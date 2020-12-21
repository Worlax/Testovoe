using UnityEngine;
using System;
using System.Collections.Generic;

public class Tile : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] Material blockedMaterial;
	[SerializeField] Material baseMaterial;
	[SerializeField] Material mouseOverMaterial;
	[SerializeField] Material targetTileMaterial;
	[SerializeField] Material queueTileMaterial;

#pragma warning restore 0649

	Vector3 obstacleCollision = new Vector3(0.5f, 0.5f, 0.5f);

	public bool Blocked { get; private set; }

	public List<Tile> ConnectedTiles { get; private set; } = new List<Tile>();
	public Tile Parent { get; set; }
	public int HCost { get; set; }
	public int FCost { get; set; }
	public int GCost { get; set; }

	bool inAction;

	public static Action<Tile> TileClicked;

	private void Start()
	{
		UpdateBlockedStatus();
		SetConnectedTiles();
	}

	void UpdateBlockedStatus()
	{
		LayerMask mask = LayerMask.GetMask("Obstacle");

		if (Physics.CheckBox(transform.position, obstacleCollision, Quaternion.identity, mask))
		{
			BlockMe();
		}
	}

	public void AddedInQueue()
	{
		inAction = true;
		SetMaterial(queueTileMaterial);
	}

	public void RemovedFromQueue()
	{
		inAction = false;
		SetMaterial(baseMaterial);
	}

	public void Targeted()
	{
		inAction = true;
		SetMaterial(targetTileMaterial);
	}

	public void Untargeted()
	{
		inAction = false;
		SetMaterial(baseMaterial);
	}

	void BlockMe()
	{
		Blocked = true;
		SetMaterial(blockedMaterial);
	}

	void SetConnectedTiles()
	{
		if (!Blocked)
		{
			SetConnectedTile(Vector3.left);
			SetConnectedTile(Vector3.right);
			SetConnectedTile(Vector3.forward);
			SetConnectedTile(Vector3.back);
		}
	}

	void SetConnectedTile(Vector3 direction)
	{
		LayerMask mask = LayerMask.GetMask("Tile");

		Physics.Raycast(transform.position, direction, out RaycastHit hit, 4f, mask);

		if (hit.transform)
		{
			ConnectedTiles.Add(hit.transform.GetComponent<Tile>());
		}
	}

	void SetMaterial(Material mat)
	{
		GetComponent<MeshRenderer>().material = mat;
	}

	private void OnMouseDown()
	{
		if (!Blocked)
		{
			TileClicked?.Invoke(this);
		}
	}

	// Cursor over for PC
	//private void OnMouseEnter()
	//{
	//	if (!inAction && !Blocked)
	//	{
	//		SetMaterial(mouseOverMaterial);
	//	}
	//}

	//private void OnMouseExit()
	//{
	//	if (!inAction && !Blocked)
	//	{
	//		SetMaterial(baseMaterial);
	//	}
	//}
}
