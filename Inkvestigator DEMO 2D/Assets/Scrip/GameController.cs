using UnityEngine;

public class GameController : MonoBehaviour
{
	public PlayerInventory playerInventory;

	[SerializeField] private GameObject _winPanel;
	[SerializeField] private GameObject _timer;

	void Update()
	{
		if (playerInventory != null && _winPanel != null)
		{
			if (playerInventory._maxNumOfItems == 0 && !_winPanel.activeSelf)
			{
				_winPanel.SetActive(true);
				_timer.SetActive(false);	//Stop Timer	
			}
		}
	}
}
