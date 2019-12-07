using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOver : MonoBehaviour {

    public void tryagain()
    {
        SceneManager.LoadScene(1); 
    }
    public void Exit()
    {
        Application.Quit();
    }
}
