using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CluesCollectionScript : MonoBehaviour
{
	[SerializeField] private List<Image> questions;
	[SerializeField] private List<Image> items;
	[SerializeField] private List<GameObject> worldClues;
	[SerializeField] private GameObject _ripple;
	[SerializeField] private RectTransform rippleParent; // Assign your UI canvas or a UI container here


	private void Start()
	{
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
			if (worldClues[i] == null) // If the world object has been destroyed
			{
				//GameObject ripple = Instantiate(_ripple, rippleParent);
				//ripple.GetComponent<RectTransform>().position = items[i].transform.position;
				questions[i].enabled = false;
				items[i].enabled = true;
			}
		}
	}
}

