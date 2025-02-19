using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject startButton;
    public GameObject titleCard;
    public GameObject creditsButton;
    public GameObject howButton;
    public GameObject creditsPanel;
    public GameObject howToPlayPanel;
    public GameObject pip;
    public GameObject levelsPanel;

    public void StartLevel(int levelNumber)
    {
        PlayerPrefs.SetInt("CurrentLevel", levelNumber);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Levels"); // Reload the same scene
    }

    public void ShowLevel()
    {
        startButton.SetActive(false);
        titleCard.SetActive(false);
        creditsButton.SetActive(false);
        howButton.SetActive(false);
        pip.SetActive(false);
        levelsPanel.SetActive(true);
    }


    public void HideLevel()
    {
        startButton.SetActive(true);
        titleCard.SetActive(true);
        creditsButton.SetActive(true);
        howButton.SetActive(true);
        pip.SetActive(true);
        levelsPanel.SetActive(false);
    }

    public void ShowCredits()
    {
        startButton.SetActive(false);
        titleCard.SetActive(false);
        creditsButton.SetActive(false);
        howButton.SetActive(false);
        pip.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void HideCredits()
    {
        startButton.SetActive(true);
        titleCard.SetActive(true);
        creditsButton.SetActive(true);
        howButton.SetActive(true);
        pip.SetActive(true);
        creditsPanel.SetActive(false);
    }

    public void ShowHowToPlay()
    {
        startButton.SetActive(false);
        titleCard.SetActive(false);
        creditsButton.SetActive(false);
        howButton.SetActive(false);
        pip.SetActive(false);
        howToPlayPanel.SetActive(true);
    }

    public void HideHowToPlay()
    {
        startButton.SetActive(true);
        titleCard.SetActive(true);
        creditsButton.SetActive(true);
        howButton.SetActive(true);
        pip.SetActive(true);
        howToPlayPanel.SetActive(false);
    }
}
