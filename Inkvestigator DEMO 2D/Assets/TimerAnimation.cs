using System.Collections;
using UnityEngine;

public class TimerAnimation : MonoBehaviour
{
	[SerializeField] private RectTransform _uiElement;    // The UI element you want to animate
	[SerializeField] private RectTransform _referenceImage; // The image to copy position and size from
	[SerializeField] private float _duration = 4f; // Time to stay at the start position
	[SerializeField] private float _lerpDuration = 2f; // Duration to lerp back to original position

	private Vector3 _originalPosition;
	private Vector3 _targetPosition;
	private Vector2 _originalSize;
	private Vector2 _targetSize;

	void Start()
	{
		// Initially, set the target position and size to match the reference image
		_originalPosition = _uiElement.position;
		_originalSize = _uiElement.rect.size;

		// Set the target position and size from the reference image
		_targetPosition = _referenceImage.position;
		_targetSize = _referenceImage.rect.size;

		// Start the animation
		StartCoroutine(MoveAndResize());
	}

	private IEnumerator MoveAndResize()
	{
		// Move and resize the UI element to the target position and size in the first second
		float timeElapsed = 0f;
		Vector3 initialPosition = _uiElement.position;
		Vector2 initialSize = _uiElement.rect.size;

		// Wait for the first second before moving
		yield return new WaitForSeconds(1f);

		// Animate position and size change over duration
		while (timeElapsed < _duration)
		{
			// Interpolate position
			_uiElement.position = Vector3.Lerp(initialPosition, _targetPosition, timeElapsed / _duration);

			// Interpolate size (width and height)
			float newWidth = Mathf.Lerp(initialSize.x, _targetSize.x, timeElapsed / _duration);
			float newHeight = Mathf.Lerp(initialSize.y, _targetSize.y, timeElapsed / _duration);

			// Apply new width and height using SetSizeWithCurrentAnchors
			_uiElement.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newWidth);
			_uiElement.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, newHeight);

			timeElapsed += Time.deltaTime;
			yield return null;
		}

		// Optionally, after the animation ends, you can move back to the original position and size
		// Add additional logic here if needed
	}
}
