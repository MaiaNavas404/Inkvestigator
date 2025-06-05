using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
	public string menuSceneName; // e.g. "MainMenu"

	public void RestartButton()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void HomeButton()
	{
		SceneManager.LoadScene(menuSceneName);
	}
}

