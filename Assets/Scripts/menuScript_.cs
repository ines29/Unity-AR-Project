using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class menuScript_ : MonoBehaviour
{
    public void PlayGame(int LevelNumber)
    {
        //PlayerPrefs.SetInt("Level", LevelNumber);
       // SceneManager.LoadScene("Game"); //Name "Game" of the scene changed!
    }
    public void OpenLink(string URL)
    {
        Application.OpenURL(URL);
    }
}
