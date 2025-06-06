using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] TimerScript Timer;
    [SerializeField] CharcaterControllerIgnacio Player;

    private bool _isPaused;

	private void Update()
	{
        if ( _isPaused)
        {
            Player._isPaused = true;     
        }
	}

	public void Pause()
    {
        pauseMenu.SetActive(true);
        Timer._isPaused = true;
        _isPaused = true;
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
		Timer._isPaused = false;
		Player._isPaused = false;
        _isPaused = false;
	}

	public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Home()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
