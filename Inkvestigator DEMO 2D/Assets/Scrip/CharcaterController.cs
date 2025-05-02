using System;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class CharcaterController : MonoBehaviour
{
	public float _moveSpeed = 15f;
	private Vector3 _mousePosition;
	private Vector3 _targetPosition;

	// INK
	[Header("INK")]
	[SerializeField] private GameObject _ink;

	private void Start()
	{
		_targetPosition = transform.position;
	}

	private void Update()
	{
		// Get Mouse POSITION
		_mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		Movement();

		ThrowInk();

	}


	private void Movement()
	{
		// MOVE where you Press
		//if (Input.GetMouseButtonDown(0))
		//{
		//	_targetPosition = _mousePosition;
		//	_targetPosition.z = 0f;
		//}

		// MOVE while Holding
		if (Input.GetMouseButton(0))
		{
			_targetPosition = _mousePosition;
			_targetPosition.z = 0f;
		}
		if (Input.GetMouseButtonUp(0)) // Stop Moving
		{
			_targetPosition = transform.position;
		}

		if (!IsWallAhead())
		{
			transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _moveSpeed * Time.deltaTime);
		}

	}

	private bool IsWallAhead()
	{
		Vector3 direction = (_targetPosition - transform.position).normalized;
		float radius = 1f; // adjust to your character's size
		float distance = _moveSpeed * Time.deltaTime;
		LayerMask wallMask = LayerMask.GetMask("Wall"); // make sure your walls are on a "Wall" layer

		RaycastHit2D hit = Physics2D.CircleCast(transform.position, radius, direction, distance, wallMask);

		return hit.collider != null;
	}
	private void ThrowInk()
	{
		if (Input.GetMouseButtonDown(1))
		{
			Vector3 direction = (_mousePosition - transform.position).normalized;
			RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, Mathf.Infinity, LayerMask.GetMask("Wall"));

			if (hit.collider != null)
			{
				// Get angle from direction
				float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

				// Create rotation
				Quaternion rotation = Quaternion.Euler(0, 0, angle - 90f); // subtract 90 if your prefab points upward

				// Instantiate the ink at the hit point
				Vector3 halfPoint = (transform.position + new Vector3(hit.point.x, hit.point.y, 0f)) / 2;

				GameObject inkInstance = Instantiate(_ink, halfPoint, rotation);
				float distance = hit.distance;
				Debug.Log("IS HITTING  " + distance);

				// Scale the ink based on the distance
				inkInstance.transform.localScale = new Vector3(5, distance, 1);
			}
		}
	}



}
