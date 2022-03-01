using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void OnPlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnExitGame()
    {
        Application.Quit();
    }
}