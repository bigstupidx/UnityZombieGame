using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

//Script for the 'Return to Main Menu' buttons in the End and Lose scenes
public class ToStartScreen : MonoBehaviour
{
    //Loads the StartScreen Scene
    public void OnMain()
    {
        SceneManager.LoadSceneAsync("StartScreen", LoadSceneMode.Single);
    }
}
