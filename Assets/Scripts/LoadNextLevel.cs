using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextLevel : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadNext()
    {
        int max = SceneManager.sceneCount;
        int idx = SceneManager.GetActiveScene().buildIndex + 1;
        if (idx < max)
        {
            Debug.Log("LAST LEVEL COMPLETE");
        }
        else
        {
            SceneManager.LoadScene(idx);
        }
    }
}
