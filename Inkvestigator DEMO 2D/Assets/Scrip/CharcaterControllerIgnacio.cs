using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.InputSystem;
using Image = UnityEngine.UI.Image;

public class CharcaterControllerIgnacio : MonoBehaviour
{
	[Header("Movement")]
	public float _moveSpeed = 10f;
	private Vector3 _mousePosition;
	private Vector3 _targetPosition;
	private Vector3 _targetRotation;
	public bool _isPaused = false;

	// INK
	[Header("INK")]
	[SerializeField] private GameObject _ink;
    [SerializeField] private GameObject _inkEnd;
    [SerializeField] private GameObject _inkBase;
    private float _inkCooldown = 1.5f;
	private bool _isCooldown = true;
	private float _inkXSize = 8;
	public CooldownScript _cooldownScript;

	// RIPPEL
	[Header("RIPPLE")]
	[SerializeField] private GameObject _ripple;
	public PlayerInventory PlayerInventory;
	public int _numOfRipples = 1;
	private int _latsNumItems = 8;
	public Image _fisrtReppleImige;
	public Image _secondReppleImige;
	public Image _thirdReppleImige;

	private Rigidbody2D _rb;

	public Animator inkvestigatorAnim;

	private void Start()
	{
		_rb = GetComponent<Rigidbody2D>();
		_targetPosition = transform.position;
	}

	private void Update()
	{
		// Get Mouse POSITION
		_mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		Movement();

		ThrowInk();

		Repple();
	}

	private void Movement()
	{
		// If the mouse is pressed, set the target position
		if (Input.GetMouseButton(0) && !_isPaused)
		{
            inkvestigatorAnim.SetBool("IsMoving", true);
            _targetPosition = _mousePosition;
			_targetPosition.z = 0f;
		}
		else
		{
			inkvestigatorAnim.SetBool("IsMoving", false);
			_targetPosition = transform.position;   // Stop the movement, And the jitering
		}

		Vector3 direction = (_targetPosition - transform.position).normalized;
		float moveStep = _moveSpeed * Time.deltaTime;
		float radius = 1f;  // radius of the Collider

		if (Input.GetMouseButton(0) && !_isPaused)
		{
			_targetRotation = direction;
		}

		// Rotation, Facing the direction.
		_rb.MoveRotation(Mathf.Atan2(_targetRotation.y, _targetRotation.x) * Mathf.Rad2Deg - 90f);

		// Check for wall collision
		RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, radius, direction, moveStep, LayerMask.GetMask("Wall"));

		if (hits.Length == 0 && direction.magnitude > 0) // Move if there is no Wall
		{
			Vector2 moveForce = direction * _moveSpeed;
			_rb.linearVelocity = moveForce;  // Use linearVelocity instead of velocity
		}
		else if (hits.Length == 1) // Move if there is 1 Wall
		{
			Vector2 wallNormal = hits[0].normal;

			// If moving into the wall, slide along the wall
			if (Vector2.Dot(direction, wallNormal) < 0f)
			{
				Vector2 slideDirection = Vector2.Perpendicular(wallNormal);

				if (Vector2.Dot(slideDirection, direction) < 0)
					slideDirection = -slideDirection;

				// Apply sliding force
				Vector2 slideForce = slideDirection * _moveSpeed;
				_rb.linearVelocity = slideForce;
			}
			else
			{
				// Moving away from the wall, normal movement
				Vector2 moveForce = direction * _moveSpeed;
				_rb.linearVelocity = moveForce;
			}
		}
		else
		{
			// Corner detected: Apply a slight movement force to get out of the corner
			Vector2 avgNormal = Vector2.zero;
			foreach (var hit in hits)
			{
				avgNormal += hit.normal;
			}
			avgNormal.Normalize();

			// Check if the player is trying to move out of the corner
			float dot = Vector2.Dot(direction, avgNormal);
			if (dot > 0f)
			{
				// Apply movement force
				Vector2 moveForce = direction * _moveSpeed;
				_rb.linearVelocity = moveForce;
			}
			else
			{
				// Stop movement if tring to go into corner
				_rb.linearVelocity = Vector2.zero;  // Zero out the velocity to stop jittering
			}
		}
	}

	private void ThrowInk()
	{
		if (Input.GetMouseButtonDown(1) && _isCooldown && !_isPaused)
		{
			_isCooldown = false;
			StartCoroutine(Cooldown());

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

				// Base and ENd Ink
				Instantiate(_inkBase, this.transform.position, rotation);
                Instantiate(_inkEnd, new Vector2 (hit.point.x, hit.point.y), rotation);
				

				// Scale the ink on the distance
				inkInstance.transform.localScale = new Vector3(_inkXSize, distance, 1);
			}
		}
	}

	IEnumerator Cooldown()
	{
		float elapsedTime = 0f;

		while (elapsedTime < _inkCooldown)
		{
			_cooldownScript.Being(elapsedTime, _inkCooldown);
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		_isCooldown = true;
	}

	private void Repple()
	{
		if (PlayerInventory._maxNumOfItems < _latsNumItems - 1)
		{
			if (_numOfRipples < 3)
			{
				_numOfRipples++;
			}
			_latsNumItems = PlayerInventory._maxNumOfItems;
		}

		if (Input.GetMouseButtonDown(2) && _numOfRipples > 0) // 2 is middle mouse button
		{
			_numOfRipples--;
			GameObject ripple = Instantiate(_ripple, transform.position, Quaternion.identity);
			RippleScript RippleScript = ripple.GetComponent<RippleScript>();
			RippleScript._isPlayer = true;
		}

		if (_numOfRipples == 0)
		{
			_fisrtReppleImige.gameObject.SetActive(false);
			_secondReppleImige.gameObject.SetActive(false);
			_thirdReppleImige.gameObject.SetActive(false);
		}
		else if (_numOfRipples == 1)
		{
			_fisrtReppleImige.gameObject.SetActive(true);
			_secondReppleImige.gameObject.SetActive(false);
			_thirdReppleImige.gameObject.SetActive(false);
		}
		else if (_numOfRipples == 2)
		{
			_fisrtReppleImige.gameObject.SetActive(true);
			_secondReppleImige.gameObject.SetActive(true);
			_thirdReppleImige.gameObject.SetActive(false);
		}
		else if (_numOfRipples == 3)
		{
			_fisrtReppleImige.gameObject.SetActive(true);
			_secondReppleImige.gameObject.SetActive(true);
			_thirdReppleImige.gameObject.SetActive(true);
		}
	}
}

