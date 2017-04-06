using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour {

    public Canvas exitMenu;
    public Canvas optionMenu;
    public Canvas keyScreen;
    public Canvas HowToPlayScreen;
    public Canvas HelperScreen;

    public Text surviveToSlideText;
    public Text playText;
    public Text menuText;
    public Text exitText;

    public RawImage Item1Image;
    public Text Item1Text;

    public RawImage Item2Image;
    public Text Item2Text;

    public RawImage Skill1Image;
    public Text Skill1Text;

    public RawImage Skill2Image;
    public Text Skill2Text;

    public RawImage Skill3Image;
    public Text Skill3Text;

    public MovieTexture FreezeMovie;
    public MovieTexture BarrierMovie;
    public MovieTexture SKillArrow1Movie;
    public MovieTexture SKillArrow2Movie;
    public MovieTexture SKillArrow3Movie;

    public Button achievementButton;

    // Use this for initialization
    void Start () {
       


        exitMenu = exitMenu.GetComponent<Canvas>();
        optionMenu = optionMenu.GetComponent<Canvas>();
        keyScreen = keyScreen.GetComponent<Canvas>();
        HowToPlayScreen = HowToPlayScreen.GetComponent<Canvas>();
        HelperScreen = HelperScreen.GetComponent<Canvas>();

        achievementButton = achievementButton.GetComponent<Button>();

        surviveToSlideText = surviveToSlideText.GetComponent<Text>();
        playText = playText.GetComponent<Text>();
        menuText = menuText.GetComponent<Text>();
        exitText = exitText.GetComponent<Text>();

        Item1Image = Item1Image.GetComponent<RawImage>();
        Item2Image = Item2Image.GetComponent<RawImage>();
        Skill1Image = Skill1Image.GetComponent<RawImage>();
        Skill2Image = Skill2Image.GetComponent<RawImage>();
        Skill3Image = Skill3Image.GetComponent<RawImage>();

        FreezeMovie = Item1Image.texture as MovieTexture;
        BarrierMovie = Item2Image.texture as MovieTexture;
        SKillArrow1Movie = Skill1Image.texture as MovieTexture;
        SKillArrow2Movie = Skill2Image.texture as MovieTexture;
        SKillArrow3Movie = Skill3Image.texture as MovieTexture;

        FreezeMovie.loop = true;
        BarrierMovie.loop = true;
        SKillArrow1Movie.loop = true;
        SKillArrow2Movie.loop = true;
        SKillArrow3Movie.loop = true;

        Item1Text = Item1Text.GetComponent<Text>();
        Item2Text = Item2Text.GetComponent<Text>();
        Skill1Text = Skill1Text.GetComponent<Text>();
        Skill2Text = Skill2Text.GetComponent<Text>();
        Skill3Text = Skill3Text.GetComponent<Text>();

        achievementButton.interactable = false;
        Item1Image.enabled = false;
        Item2Image.enabled = false;
        Skill1Image.enabled = false;
        Skill2Image.enabled = false;
        Skill3Image.enabled = false;

        Item1Text.enabled = false;
        Item2Text.enabled = false;
        Skill1Text.enabled = false;
        Skill2Text.enabled = false;
        Skill3Text.enabled = false;

        HelperScreen.enabled = false;
        HowToPlayScreen.enabled = false;
        keyScreen.enabled = false;
        optionMenu.enabled = false;
        exitMenu.enabled = false;
       
      
    }

    //Play button is pressed. Start the game.
    public void StartGame()
    {
        PlayerPrefs.SetFloat("gametime", 0);
        SceneManager.LoadSceneAsync("Level1", LoadSceneMode.Single);
        //Application.LoadLevel(1);
    }
	

    //Menu button is pressed. Show the menu list.
    public void MenuButtonPressed()
    {
        optionMenu.enabled = true;
        menuText.enabled = false;
        playText.enabled = false;
        exitText.enabled = false;
    }


    //How To Play Button is pressed in the Menu list
    public void HowToPlayButtonInMenuPressed()
    {
        optionMenu.enabled = false;
        HowToPlayScreen.enabled = true;
    }

    //The Item1 button is pressed, go to description for the item1.
    public void Item1PressedInHowToPlay()
    {

        HowToPlayScreen.enabled = false;
        HelperScreen.enabled = true;
        Item1Image.enabled = true;
        Item1Text.enabled = true;
        FreezeMovie.Play();
    }

    //The Item2 button is pressed, go to description for the item2.
    public void Item2PressedInHowToPlay()
    {
        HowToPlayScreen.enabled = false;
        HelperScreen.enabled = true;
        Item2Image.enabled = true;
        Item2Text.enabled = true;
        BarrierMovie.Play();
    }

    //The skill button is pressed, go to description for the skill.
    public void Skill1PressedInHowToPlay()
    {
        HowToPlayScreen.enabled = false;
        HelperScreen.enabled = true;
        Skill1Image.enabled = true;
        Skill1Text.enabled = true;
        SKillArrow1Movie.Play();
       
    }

    //The skill button is pressed, go to description for the skill.
    public void Skill2PressedInHowToPlay()
    {
        HowToPlayScreen.enabled = false;
        HelperScreen.enabled = true;
        Skill2Image.enabled = true;
        Skill2Text.enabled = true;
        SKillArrow2Movie.Play();
    }

    //The skill button is pressed, go to description for the skill.
    public void Skill3PressedInHowToPlay()
    {
        HowToPlayScreen.enabled = false;
        HelperScreen.enabled = true;
        Skill3Image.enabled = true;
        Skill3Text.enabled = true;
        SKillArrow3Movie.Play();
    }
    //Back button in the description to go back to How to play menu.
    public void BackToHowToPlay()
    {
        FreezeMovie.Stop();
        BarrierMovie.Stop();
        SKillArrow1Movie.Stop();
        SKillArrow2Movie.Stop();
        SKillArrow3Movie.Stop();
        Item1Text.enabled = false;
        Item1Image.enabled = false;
        Item2Text.enabled = false;
        Item2Image.enabled = false;
        Skill1Text.enabled = false;
        Skill1Image.enabled = false;
        Skill2Text.enabled = false;
        Skill2Image.enabled = false;
        Skill3Text.enabled = false;
        Skill3Image.enabled = false;
        HelperScreen.enabled = false;
        HowToPlayScreen.enabled = true;
    }




    //Keyconf button is pressed, go to Keyconf Screen.
    public void KeyConfButtonInMenuPressed()
    {
        optionMenu.enabled = false;
        keyScreen.enabled = true;
    }

    //Back to Menu
    public void BackToMenu(Canvas current)
    {
        current.enabled = false;
        optionMenu.enabled = true;
    }

    public void BackToMenuFromAchievement()
    {
		SlideToSurviveAchievements.archerAchievement.GetComponent<AchievementWindow>().ActivateDisplay();
        achievementButton.interactable = false;

       // button.gameObject.SetActive (false);
        optionMenu.enabled = true;
    }

    //Achievemnt button is pressed.
    public void AchievementButtonInMenuPressed()
    {
        GameObject.Find("AchievementWindowCanvas").transform.GetChild(0).gameObject.SetActive(true);
        SlideToSurviveAchievements.archerAchievement.GetComponent<AchievementWindow>().ActivateDisplay();
        optionMenu.enabled = false;
        achievementButton.interactable = true;
    }
    
    //Leaderboard button is pressed.
    public void LeaderBoardButtonInMenuPressed()
    {
        optionMenu.enabled = false;
        PlayerPrefs.SetFloat("gametime", 0);
        SceneManager.LoadSceneAsync("End", LoadSceneMode.Single);
    }




    //Back button in the menu is pressed. Back to the main screen.
    public void BackButtonInMenuPressed()
    {
        optionMenu.enabled = false;
        menuText.enabled = true;
        playText.enabled = true;
        exitText.enabled = true;
    }


    //Exit button is pressed.
    public void ExitButtonPressed()
    {
        exitMenu.enabled = true;
        menuText.enabled = false;
        playText.enabled = false;
        exitText.enabled = false;
    }
    //Yes button in the exit menu is pressed. Turn off the game.
    public void ExitGame()
    {
        Application.Quit();
    }
    //No button in the exit menu is pressed. Back to the main screen.
    public void NoPressedInExitMenu()
    {
        exitMenu.enabled = false;
        menuText.enabled = true;
        playText.enabled = true;
        exitText.enabled = true;
    }


  
}
