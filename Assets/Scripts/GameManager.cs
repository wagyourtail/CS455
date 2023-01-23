using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static float RESTART_TIME = 2f;
    private bool ended = false;

    public GameObject completeLevelScreen;

    public static GameManager Get()
    {
        return FindObjectOfType<GameManager>();
    }
    
    public void EndGame() {
        if (!ended)
        {
            ended = true;
            Score.Get().freeze();
            Invoke("StartGame", RESTART_TIME);
        }
    }

    public void CompleteLevel()
    {
        if (!ended)
        {
            ended = true;
            Debug.Log("level complete");
            completeLevelScreen.SetActive(true);
        }
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    private enum GameState
    {
        RUNNING,
        GAME_OVER
    }
}
