using UnityEngine;

public class ActivateGameObj : MonoBehaviour
{
	public GameObject _gameObj;

	private void OnTriggerEnter2D(Collider2D other)
	{
		// Assuming player has tag "Player"
		if (other.CompareTag("Player"))
		{
			_gameObj.SetActive(true);
		}
	}
}
