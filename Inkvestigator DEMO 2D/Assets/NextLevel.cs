using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
	[SerializeField] private string _sceneToLoad;
	[SerializeField] private float _delay = 3f;

	[SerializeField] private bool _isWinState = false;
	[SerializeField] private bool _isLoadNextScene = true;

	[SerializeField] private GameObject _winPanel;
	[SerializeField] private GameObject _timer;

	[SerializeField] private CharcaterControllerIgnacio _CharcaterControlerI;

	private bool _isTriggered = false;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (!_isTriggered && other.CompareTag("Player"))
		{
			_isTriggered = true;

			if (_isWinState)
			{
				_winPanel.SetActive(true);
				_timer.SetActive(false);
				_CharcaterControlerI._isPaused = true;
			}

			if (_isLoadNextScene)
				StartCoroutine(LoadSceneAfterDelay());
		}
	}

	private IEnumerator LoadSceneAfterDelay()
	{
		yield return new WaitForSeconds(_delay);
		SceneManager.LoadScene(_sceneToLoad);
	}
}

