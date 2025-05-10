using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TimerScript : MonoBehaviour
{
	//public void OnPointerClick(PointerEventData eventData)
	//{
	//	Pause = !Pause;
	//}

	[SerializeField] private Image _uiFill;

	public int _duration;

	private int _remainingDuration;

	[SerializeField] private GameObject _gameOverPanel;  // assign in inspector

	//private bool Pause;

	private void Start()
	{
		Being(_duration);
	}

	private void Being(int Second)
	{
		_remainingDuration = Second;
		StartCoroutine(UpdateTimer());
	}

	private IEnumerator UpdateTimer()
	{
		while (_remainingDuration >= 0)
		{
			_uiFill.fillAmount = Mathf.InverseLerp(0, _duration, _remainingDuration);
			_remainingDuration--;
			yield return new WaitForSeconds(1f);
			//if (!Pause)
			//{
				
			//}
			yield return null;
		}
		OnEnd();
	}

	private void OnEnd()
	{
		_gameOverPanel.SetActive(true);
	}

}
