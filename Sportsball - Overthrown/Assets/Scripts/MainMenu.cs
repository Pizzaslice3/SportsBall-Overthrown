using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject titleScreen, controls, credits;

    public GameObject controls2, controls3;

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenTitle()
    {
        titleScreen.SetActive(true);
        controls.SetActive(false);
        controls2.SetActive(false);
        controls3.SetActive(false);
        credits.SetActive(false);
    }

    public void OpenControls()
    {
        titleScreen.SetActive(false);
        controls.SetActive(true);
        controls2.SetActive(false);
        controls3.SetActive(false);
        credits.SetActive(false);
    }

    public void OpenControls2()
    {
        titleScreen.SetActive(false);
        controls.SetActive(false);
        controls2.SetActive(true);
        controls3.SetActive(false);
        credits.SetActive(false);
    }

    public void OpenControls3()
    {
        titleScreen.SetActive(false);
        controls.SetActive(false);
        controls2.SetActive(false);
        controls3.SetActive(true);
        credits.SetActive(false);
    }

    public void OpenCredits()
    {
        titleScreen.SetActive(false);
        controls.SetActive(false);
        credits.SetActive(true);
    }
}
