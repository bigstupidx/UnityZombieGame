using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class RuntimeMenuManager : MonoBehaviour {

    public Canvas RuntimeCanvas;
    public Canvas KeyConf;
    public Canvas ExitMenu;
    
    private bool paused;
	// Use this for initialization
	void Start () {
        RuntimeCanvas = RuntimeCanvas.GetComponent<Canvas>();
        KeyConf = KeyConf.GetComponent<Canvas>();
        ExitMenu = ExitMenu.GetComponent<Canvas>();

        paused = false;

        KeyConf.enabled = false;
        ExitMenu.enabled = false;
   
        RuntimeCanvas.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape) &&
            !KeyConf.isActiveAndEnabled &&
            !ExitMenu.isActiveAndEnabled)
        {
            pauseGame();
        }
           
    }
    void pauseGame()
    {
        if (paused)
        {
            RuntimeCanvas.enabled = false;
            Time.timeScale = 1;
            paused = false;
        }
        else
        {
            RuntimeCanvas.enabled = true;
            Time.timeScale = 0;
            paused = true;

        }
    }

    public void BackToGame()
    {
        RuntimeCanvas.enabled = false;
        Time.timeScale = 1;
        paused = false;
    }


    public void KeyConfPressed()
    {
        KeyConf.enabled = true;
        RuntimeCanvas.enabled = false;
    }

    public void BackToPauseMenu(Canvas current)
    {
        current.enabled = false;
        RuntimeCanvas.enabled = true;
    }





    public void BackToMenu()
    {
        PlayerPrefs.SetFloat("gametime", 0);
        SceneManager.LoadSceneAsync("StartScreen", LoadSceneMode.Single);
    }






    public void ExitGame()
    {
        ExitMenu.enabled = true;
        RuntimeCanvas.enabled = false;
    }

    public void NoPressed()
    {
        ExitMenu.enabled = false;
        RuntimeCanvas.enabled = true;
    }
    public void YesPressed()
    {
        Application.Quit();
    }
}
