using UnityEngine;

public class Player : MonoBehaviour
{
	public Tile GetConnectedTile()
	{
		LayerMask mask = LayerMask.GetMask("Tile");
		Vector3 offset = new Vector3(0, 1, 0);

		Physics.Raycast(transform.position + offset, Vector3.down, out RaycastHit hit, 3f, mask);

		if (hit.transform)
		{
			return hit.transform.GetComponent<Tile>();
		}
		else
		{
			return null;
		}
	}
}
