using UnityEngine;

public class CamaraFollorplayer : MonoBehaviour
{
	public Transform target;
	public Vector3 offset;

	void Update()
	{
		Vector3 pos = transform.position;
		pos.x = target.position.x;
		pos.y = target.position.y;
		transform.position = pos;
	}
}
