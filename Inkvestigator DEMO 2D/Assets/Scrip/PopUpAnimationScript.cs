using UnityEngine;

public class PopUpAnimationScript : MonoBehaviour
{
	public float _bigSize = 1.5f;  // The size to grow to
	public float growDuration = 1f;  // Time to grow to _bigSize
	public float shrinkDuration = 0.3f;  // Time to shrink 10%
	public float shrinkAmount = 0.9f;  // Shrink by 10%
	public float growAgainDuration = 0.3f;  // Time to grow 20% again
	public float growAgainAmount = 1.2f;  // Grow by 20%

	private Vector3 originalScale;
	private bool isAnimating = false;

	// Start is called before the first frame update
	void Start()
	{
		originalScale = transform.localScale;  // Store the original scale
	}

	// Update is called once per frame
	void Update()
	{
		if (!isAnimating)
		{
			StartCoroutine(PlayAnimation());
		}
	}

	private System.Collections.IEnumerator PlayAnimation()
	{
		isAnimating = true;

		// Grow from 0 to _bigSize in 1 second
		float elapsedTime = 0f;
		while (elapsedTime < growDuration)
		{
			transform.localScale = Vector3.Lerp(originalScale, new Vector3(_bigSize, _bigSize, _bigSize), elapsedTime / growDuration);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		transform.localScale = new Vector3(_bigSize, _bigSize, _bigSize);  // Ensure it reaches _bigSize

		// Shrink 10% in 0.3 seconds
		elapsedTime = 0f;
		Vector3 shrunkScale = new Vector3(_bigSize * shrinkAmount, _bigSize * shrinkAmount, _bigSize * shrinkAmount);
		while (elapsedTime < shrinkDuration)
		{
			transform.localScale = Vector3.Lerp(new Vector3(_bigSize, _bigSize, _bigSize), shrunkScale, elapsedTime / shrinkDuration);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		transform.localScale = shrunkScale;

		// Grow 20% in 0.3 seconds
		elapsedTime = 0f;
		Vector3 grownScale = new Vector3(shrunkScale.x * growAgainAmount, shrunkScale.y * growAgainAmount, shrunkScale.z * growAgainAmount);
		while (elapsedTime < growAgainDuration)
		{
			transform.localScale = Vector3.Lerp(shrunkScale, grownScale, elapsedTime / growAgainDuration);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		transform.localScale = grownScale;

		// Optionally, you can make the object disappear here
		// For example, you could destroy the object after the animation
		Destroy(gameObject);
	}
}
