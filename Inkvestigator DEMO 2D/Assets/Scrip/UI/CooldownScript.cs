using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CooldownScript : MonoBehaviour
{
	//public void OnPointerClick(PointerEventData eventData)
	//{
	//	Pause = !Pause;
	//}

	[SerializeField] private Image _uiFill;

	private float _totalCooldownDuration;

	private float _remainingDuration;

	//private bool Pause;

	public void Being(float countDown, float totalCooldown)
	{
		_totalCooldownDuration = totalCooldown;
		_remainingDuration = countDown;
		StartCoroutine(UpdateTimer());
	}

	private IEnumerator UpdateTimer()
	{
		while (_remainingDuration >= 0)
		{
			_uiFill.fillAmount = Mathf.InverseLerp(0, _totalCooldownDuration, _remainingDuration);
			//_remainingDuration--;
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
		//End Time , if want Do something
		print("End");
	}
}
