using UnityEngine;
using UnityEngine.UI;

public class MouseCrosshair : MonoBehaviour
{
	void Start()
	{
		//Cursor.visible = false; // Hide system cursor
	}

	void Update()
	{
		Vector2 mousePos = Input.mousePosition;
		transform.position = mousePos;
	}
}

