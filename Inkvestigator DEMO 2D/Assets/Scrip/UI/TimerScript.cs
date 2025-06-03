using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
	[SerializeField] private Image _uiFill;
	[SerializeField] private RectTransform _clockHand;

	public int _duration;
	private float _remainingDuration;

	[SerializeField] private GameObject _gameOverPanel;
	public CharcaterControllerIgnacio _CharacterController;

	private void Start()
	{
		Begin(_duration);
	}

	private void Begin(int seconds)
	{
		_remainingDuration = seconds;
		StartCoroutine(UpdateTimer());
	}

	private IEnumerator UpdateTimer()
	{
		while (_remainingDuration >= 0)
		{
			_remainingDuration -= Time.deltaTime;

			float progress = Mathf.InverseLerp(0, _duration, _remainingDuration);
			_uiFill.fillAmount = progress;

			float angle = 360f * progress;
			_clockHand.localRotation = Quaternion.Euler(0, 0, angle); // Clockwise

			yield return null;
		}

		OnEnd();
	}

	private void OnEnd()
	{
		_CharacterController._isPaused = true;
		_gameOverPanel.SetActive(true);
	}
}

