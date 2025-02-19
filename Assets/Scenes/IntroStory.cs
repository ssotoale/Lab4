using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroStory : MonoBehaviour
{
    // Update is called once per frame
    void OnEnable()
    {
        SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
    }
}
