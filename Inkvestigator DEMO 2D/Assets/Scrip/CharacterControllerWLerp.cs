using System;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class CharacterControllerWLerp : MonoBehaviour
{
    public float _moveSpeed = 15f;
    private Vector3 _mousePosition;
    private Vector3 _targetPosition;

    // INK
    [Header("INK")]
    [SerializeField] private GameObject _ink;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        // If the mouse is pressed, set the target position
        if (Input.GetMouseButton(0))
        {
            //     _targetPosition = _mousePosition;
            _targetPosition = Vector3.Lerp(transform.position, _mousePosition, 5*Time.deltaTime);
            _targetPosition.z = 0f;

            //lerpedValue = Mathf.SmoothDamp(lerpedValue, _mousePosition, velocity, duration);
        }                                   // ADD THE DELAY HERE BEFOR the else
        //Vector3.Lerp(Start position(lerped value), End position, Interpolation value (time.deltatime));
        else
        {
            _targetPosition = transform.position;   // Stop the movement, And the jitering
        }

        Vector3 direction = (_targetPosition - transform.position).normalized;
        float moveStep = _moveSpeed * Time.deltaTime;
        float radius = 1f;  // radius of the Collider

        // Check for wall collision
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, radius, direction, moveStep, LayerMask.GetMask("Wall"));

        if (hits.Length == 0 && direction.magnitude > 0) // Move if there is no Wall
        {
            Vector2 moveForce = direction * _moveSpeed;
            rb.linearVelocity = moveForce;  // Use linearVelocity instead of velocity
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
                rb.linearVelocity = slideForce;
            }
            else
            {
                // Moving away from the wall, normal movement
                Vector2 moveForce = direction * _moveSpeed;
                rb.linearVelocity = moveForce;
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
                rb.linearVelocity = moveForce;
            }
            else
            {
                // Stop movement if tring to go into corner
                rb.linearVelocity = Vector2.zero;  // Zero out the velocity to stop jittering
            }
        }

        //// If no movement and no input, explicitly set velocity to zero to avoid jittering
        //if (direction.magnitude == 0)
        //{
        //	rb.linearVelocity = Vector2.zero;
        //}
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

                // Scale the ink based on the distance
                inkInstance.transform.localScale = new Vector3(5, distance, 1);
            }
        }
    }
}

