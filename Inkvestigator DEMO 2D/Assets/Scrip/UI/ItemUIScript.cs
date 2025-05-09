using TMPro;
using UnityEngine;

public class ItemUIScript : MonoBehaviour
{
	[SerializeField] private TMP_Text clueText;

	public void UpdateNumberOfItems(int num)
	{
		clueText.text = "Clues Left: " + num;
	}
}
