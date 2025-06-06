using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneAfterDelay : MonoBehaviour
{
	[SerializeField] private string sceneToLoad;
	[SerializeField] private float delay = 28f;

	private void Start()
	{
		Invoke(nameof(LoadScene), delay);
	}

	private void LoadScene()
	{
		SceneManager.LoadScene(sceneToLoad);
	}
}
