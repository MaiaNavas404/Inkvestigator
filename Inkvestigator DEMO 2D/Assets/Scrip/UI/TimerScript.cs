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

	[SerializeField] private Image uiFill;

	public int Duration;

	private int remainingDuration;

	//private bool Pause;

	private void Start()
	{
		Being(Duration);
	}

	private void Being(int Second)
	{
		remainingDuration = Second;
		StartCoroutine(UpdateTimer());
	}

	private IEnumerator UpdateTimer()
	{
		while (remainingDuration >= 0)
		{
			uiFill.fillAmount = Mathf.InverseLerp(0, Duration, remainingDuration);
			remainingDuration--;
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
