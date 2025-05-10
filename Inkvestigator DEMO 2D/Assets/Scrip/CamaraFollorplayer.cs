using UnityEngine;

public class CamaraFollorplayer : MonoBehaviour
{
	public Transform _target;
	public Vector3 _offset;

	void Update()
	{
		Vector3 pos = transform.position;
		pos.x = _target.position.x;
		pos.y = _target.position.y;
		transform.position = pos;
	}
}
