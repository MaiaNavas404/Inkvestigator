using System.Collections;
using UnityEngine;

public class RippleScript : MonoBehaviour
{
	[SerializeField] private Vector3 _minSize = new Vector3(0.3f, 0.3f, 0.3f);
	[SerializeField] private float _scaleSpeed = 1f;
	[SerializeField] private float _fadeDuration = 1f;

	private bool _isFading = false;
	private float _fadeTimer = 0f;
	private SpriteRenderer _spriteRenderer;

	void Start()
	{
		transform.localScale = _minSize;
		_spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void Update()
	{
		// Always scale up
		transform.localScale += Vector3.one * _scaleSpeed * Time.deltaTime;

		if (_isFading)
		{
			_fadeTimer += Time.deltaTime;
			float alpha = Mathf.Lerp(1f, 0f, _fadeTimer / _fadeDuration);
			if (_spriteRenderer != null)
			{
				Color c = _spriteRenderer.color;
				c.a = alpha;
				_spriteRenderer.color = c;
			}

			if (_fadeTimer >= _fadeDuration)
			{
				Destroy(gameObject);
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		// Assuming player has tag "Player"
		if (!_isFading && other.CompareTag("Player"))
		{
			_isFading = true;
			_fadeTimer = 0f;
		}
	}
}

