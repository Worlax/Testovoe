using UnityEngine;
using System;
using System.Collections.Generic;

static public class Pathfinder
{
	static public Queue<Tile> FindPath(Tile start, Tile end)
	{
		if (start == end)
			return null;

		List<Tile> open = new List<Tile>();
		List<Tile> closed = new List<Tile>();

		open.Add(start);

		while (open.Count > 0)
		{
			Tile curTIle = GetLowestScore(open);

			open.Remove(curTIle);
			closed.Add(curTIle);

			foreach (Tile connectedTile in curTIle.ConnectedTiles)
			{
				if (connectedTile == end)
				{
					closed.Add(connectedTile);
					connectedTile.Parent = curTIle;
					Queue<Tile> path = CalculatePath(end);

					ClearInfo(open);
					ClearInfo(closed);

					return path;
				}

				if (connectedTile.Blocked || closed.Contains(connectedTile))
				{
					continue;
				}
				else if (open.Contains(connectedTile))
				{
					if (connectedTile.GCost > curTIle.GCost + 1)
					{
						Tile oldParent = connectedTile.Parent;
						connectedTile.Parent = curTIle;
					}
				}
				else
				{
					connectedTile.Parent = curTIle;
					connectedTile.HCost = CalculateH(connectedTile, end);
					open.Add(connectedTile);
				}
			}
		}

		return null;
	}

	static Queue<Tile> CalculatePath(Tile destination)
	{
		List<Tile> path = new List<Tile>();
		Tile curTile = destination;

		while (curTile)
		{
			path.Add(curTile);
			curTile = curTile.Parent;
		}

		path.Reverse();
		path.RemoveAt(0);

		return new Queue<Tile>(path);
	}

	static Tile GetLowestScore(List<Tile> tiles)
	{
		Tile lowestCostTile = tiles[0];

		foreach (Tile tile in tiles)
		{
			if (tile.FCost < lowestCostTile.FCost ||
				tile.FCost == lowestCostTile.FCost && tile.HCost < lowestCostTile.HCost)
			{
				lowestCostTile = tile;
			}
		}

		return lowestCostTile;
	}

	static int CalculateH(Tile from, Tile to)
	{
		Vector3 firstPosition = from.transform.position;
		Vector3 secondPosition = to.transform.position;

		int x = (int)Math.Abs(firstPosition.x - secondPosition.x);
		int y = (int)Math.Abs(firstPosition.y - secondPosition.y);

		return x + y;
	}

	static void ClearInfo(List<Tile> tiles)
	{
		foreach (Tile tile in tiles)
		{
			tile.HCost = 0;
			tile.Parent = null;
		}
	}
}
