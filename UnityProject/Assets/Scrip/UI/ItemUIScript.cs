using TMPro;
using UnityEngine;

public class ItemUIScript : MonoBehaviour
{
	[SerializeField] private TMP_Text _clueText;

	public void UpdateNumberOfItems(int num)
	{
		_clueText.text = "Clues Left: " + num;
	}
}
