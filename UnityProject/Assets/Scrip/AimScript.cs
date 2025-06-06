using UnityEngine;

public class AimScript : MonoBehaviour
{
	void Update()
	{
		Vector3 mouseScreenPos = Input.mousePosition;
		Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
		mouseWorldPos.z = transform.position.z;

		Vector3 direction = mouseWorldPos - transform.position;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

		transform.rotation = Quaternion.Euler(0, 0, angle);
	}
}

