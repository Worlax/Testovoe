using System.Collections.Generic;

public class Path
{
	Queue<Tile> targetedTiles = new Queue<Tile>();
	Queue<Tile> path = new Queue<Tile>();

	Tile lastTile;
	Tile curTarget;

	public void Init(Tile startTile)
	{
		lastTile = startTile;
	}

	public void AddTile(Tile tile)
	{
		if (targetedTiles.Contains(tile) || curTarget == tile)
			return;

		targetedTiles.Enqueue(tile);
		tile.AddedInQueue();
	}

	public Tile GetNextTile()
	{
		UpdatePath();

		if (path.Count > 0)
		{
			lastTile = path.Dequeue();
			return lastTile;
		}
		else
		{
			if (curTarget)
			{
				curTarget.Untargeted();
			}

			return null;
		}
	}

	void UpdatePath()
	{
		if (path.Count == 0 && targetedTiles.Count > 0)
		{
			if (curTarget)
			{
				curTarget.Untargeted();
			}

			curTarget = targetedTiles.Dequeue();
			curTarget.Targeted();
			path = CalculatePath(lastTile, curTarget);
		}
	}

	Queue<Tile> CalculatePath(Tile start, Tile goal)
	{
		Queue<Tile> path = Pathfinder.FindPath(start, goal);

		return path;
	}
}
