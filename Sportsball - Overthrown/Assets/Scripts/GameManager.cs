using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool paused = false;

    public GameObject pauseMenu;
    public GameObject gameUI;
    // Start is called before the first frame update
    void Start()
    {
        paused = false;
        gameUI.SetActive(true);
        pauseMenu.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Tab))
        //{
        //    if(paused)
        //    {
        //        UnPause();
        //    }
        //    else
        //    {
        //        Pause();
        //    }    
        //}

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            EndGame();
        }

    }

    void Pause()
    {
        Time.timeScale = 0;
        paused = true;

        gameUI.SetActive(false);
        pauseMenu.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void UnPause()
    {
        Time.timeScale = 1;
        paused = false;

        gameUI.SetActive(true);
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
