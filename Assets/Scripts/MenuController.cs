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

    public void StartGame()
    {
        SceneManager.LoadScene("Levels");
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
