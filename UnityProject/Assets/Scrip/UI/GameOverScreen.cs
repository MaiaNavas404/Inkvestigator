using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // For UI Image

public class GameOverScreen : MonoBehaviour
{
	public string menuSceneName; // e.g. "MainMenu"
	public Image imageToHide; // Reference to the UI Image
	private float timer = 5f; // Time in seconds for the image to disappear

	private void OnEnable()
	{
		// Start the timer when the object is enabled
		if (imageToHide != null)
		{
			imageToHide.gameObject.SetActive(true); // Ensure the image is visible when the object is enabled
			Invoke("HideImage", timer); // Call HideImage method after 5 seconds
		}
	}

	private void HideImage()
	{
		// Disable the image after 5 seconds
		if (imageToHide != null)
		{
			imageToHide.gameObject.SetActive(false);
		}
	}

	public void RestartButton()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void HomeButton()
	{
		SceneManager.LoadScene(menuSceneName);
	}
}
