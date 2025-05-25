using UnityEngine;
using UnityEngine.UI;

public class RippleUI : MonoBehaviour
{
	[SerializeField] private Vector3 _minSize = new Vector3(0.3f, 0.3f, 0.3f);
	[SerializeField] private float _scaleSpeed = 1f;
	[SerializeField] private float _fadeDuration = 1f;

	private float _fadeTimer = 0f;
	private Image _image;
	private RectTransform _rectTransform;

	void Start()
	{
		_rectTransform = GetComponent<RectTransform>();
		_rectTransform.localScale = _minSize;

		_image = GetComponent<Image>();
	}

	void Update()
	{
		// Scale up the UI element
		_rectTransform.localScale += Vector3.one * _scaleSpeed * Time.deltaTime;

		// Fade out
		_fadeTimer += Time.deltaTime;
		float alpha = Mathf.Lerp(1f, 0f, _fadeTimer / _fadeDuration);
		if (_image != null)
		{
			Color c = _image.color;
			c.a = alpha;
			_image.color = c;
		}

		// Destroy after fade
		if (_fadeTimer >= _fadeDuration)
		{
			Destroy(gameObject);
		}
	}
}
