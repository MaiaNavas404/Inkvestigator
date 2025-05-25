using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CluesCollectionScript : MonoBehaviour
{
	[SerializeField] private List<Image> questions;
	[SerializeField] private List<Image> items;
	[SerializeField] private List<GameObject> worldClues;
	[SerializeField] private GameObject _ripple;
	[SerializeField] private RectTransform rippleParent; // Assign a UI container (like a Canvas)

	private List<bool> revealed; // Tracks which clues have been revealed

	private void Start()
	{
		revealed = new List<bool>(new bool[worldClues.Count]);

		for (int i = 0; i < questions.Count; i++)
		{
			questions[i].enabled = true;
			items[i].enabled = false;
		}
	}

	private void Update()
	{
		for (int i = 0; i < worldClues.Count; i++)
		{
			if (!revealed[i] && worldClues[i] == null)
			{
				revealed[i] = true;

				questions[i].enabled = false;
				items[i].enabled = true;

				// Instantiate ripple at item's UI position
				GameObject ripple = Instantiate(_ripple, rippleParent);
				ripple.transform.position = items[i].transform.position;
			}
		}
	}
}
