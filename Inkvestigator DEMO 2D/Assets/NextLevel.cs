using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
	[SerializeField] private string _sceneToLoad;
	[SerializeField] private float _delay = 3f;

	private bool _isTriggered = false;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (!_isTriggered && other.CompareTag("Player"))
		{
			_isTriggered = true;
			StartCoroutine(LoadSceneAfterDelay());
		}
	}

	private IEnumerator LoadSceneAfterDelay()
	{
		yield return new WaitForSeconds(_delay);
		SceneManager.LoadScene(_sceneToLoad);
	}
}

