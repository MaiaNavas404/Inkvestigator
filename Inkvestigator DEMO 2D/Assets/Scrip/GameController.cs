using UnityEngine;

public class GameController : MonoBehaviour
{
	public PlayerInventory playerInventory;

	[SerializeField] private GameObject _winPanel;
	[SerializeField] private GameObject _timer;
	[SerializeField] private Transform _winFocusPoint;
	[SerializeField] private CamaraFollorplayer _cameraController;

	private bool hasTriggeredWinSequence = false;

	void Update()
	{
		if (playerInventory._maxNumOfItems == 0 && !_winPanel.activeSelf && !hasTriggeredWinSequence)
		{
			hasTriggeredWinSequence = true;

			_cameraController.StartWinTransition(_winFocusPoint);

		}
	}
}
