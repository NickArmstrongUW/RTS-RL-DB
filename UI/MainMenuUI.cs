using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void GoToDeckEditor()
    {
        SceneManager.LoadScene("DeckBuilder");
    }

    public void StartNewRun()
    {
        SceneManager.LoadScene("Survival");
    }
}
