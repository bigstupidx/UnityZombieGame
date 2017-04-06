using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

//The game's game controller. Main responsibilities include; saving player states between scene transitions,
//managing the scene transitions, and the keeping track of the score count of the current game session
public class SlideToSurviveGameController : MonoBehaviour {
	//Keys for saving items/lives stats inbetween scene sessions
	private string playerOneLivesKey = "playeronelives_key";
	private string playerTwoLivesKey = "playertwolives_key";

	private string playerOneItemsKey = "playeroneitems_key";
	private string playerTwoItemsKey = "playertwoitems_key";

	//Player controllers to manage the players' stat persistence
	private PlayerDeathController p1DC;
	private PlayerDeathController p2DC;

	private PlayerItemsControllerMain p1IC;
	private PlayerItemsControllerMain p2IC;

	//Input the scene sequence
	public string[] scenes;

	//Deprecated elements, consider removing these
	public Button playAgain;

	public Button continueToNext;

	public Text scoreText;

	//Necessary for initializing player components, as start method is called while objects are being initialized
	private bool initFlag;

	//Singleton instance
	public static SlideToSurviveGameController gameController;

	//Configuration to enable the singleton pattern functionality
	void Awake () {
		if (gameController == null) {
			DontDestroyOnLoad (gameObject);
			gameController = this;
		} 

		else if (gameController != this) {
			Destroy (gameObject);
		}

		//New scene loaded, so initialize the player components again
		gameController.SetInitFlag ();
	}

	void Start () {
		//Deprecated functionality, consider removing
		continueToNext.onClick.AddListener (() => {PlayNextFunction(); });
		playAgain.onClick.AddListener (() => {PlayAgainFunction(); });
	}

	void Update() {
		//Initialize the player components once for each loaded level
		if (initFlag) {
			InitializePlayerComponents ();
			initFlag = false;
		}
	}

	//Method to initialize the coins/lives for the level, depending on the saved player stat persistence
	void InitializePlayerComponents() {
		//Find the necessary player controllers
		p1DC = GameObject.Find("PlayerOne_Green").GetComponent<PlayerDeathController> ();
		p2DC = GameObject.Find("PlayerTwo_Blue").GetComponent<PlayerDeathController> ();

		p1IC = GameObject.Find("PlayerOne_Green").GetComponent<PlayerItemsControllerMain> ();
		p2IC = GameObject.Find("PlayerTwo_Blue").GetComponent<PlayerItemsControllerMain> ();

		//If it is the first level
		if (SceneManager.GetActiveScene ().name.Equals (scenes [0])) {
			//Set the players' lives to two each
			p1DC.SetLives (2);
			p2DC.SetLives (2);

			//Dont start with any coins
			p1IC.SetCurrencyCount (0);
			p1IC.SetCurrencyCount (0);
		} 

		//Not first level, so load saved player stats
		else {
			//Set the lives/coins based on prev. level accumulated
			p1DC.SetLives (PlayerPrefs.GetInt(playerOneLivesKey));
			p2DC.SetLives (PlayerPrefs.GetInt(playerTwoLivesKey));

			p1IC.SetCurrencyCount (PlayerPrefs.GetInt(playerOneItemsKey));
			p2IC.SetCurrencyCount (PlayerPrefs.GetInt(playerTwoItemsKey));
		}
	}

	//Call when transitioning between levels; save the players' current stats
	void SavePlayerComponents () {
		PlayerPrefs.SetInt (playerOneLivesKey, p1DC.GetLives());
		PlayerPrefs.SetInt (playerTwoLivesKey, p2DC.GetLives());

		PlayerPrefs.SetInt (playerOneItemsKey, p1IC.GetCurrencyCount());
		PlayerPrefs.SetInt (playerTwoItemsKey, p2IC.GetCurrencyCount());

	}
	//Deprecated, consider removing
	void PlayAgainFunction() {
		SceneManager.LoadSceneAsync (SceneManager.GetActiveScene().name, LoadSceneMode.Single);
	}
	//Deprecated, consider removing
	void PlayNextFunction() {
		LoadNextLevel (SceneManager.GetActiveScene().name);
	}

	//Method that loads the next level, based on the scene sequence input, and saves all relevant data before level progression
	void LoadNextLevel (string sceneName) {
		string nextScene = "";
		//the finished level's index for achievement
		int levelFinished = 0;

		for (int i = 0; i < scenes.Length; i++) {
			if (scenes [i].Equals (sceneName)) {
				levelFinished = i;

				//Get the next scene in the scene sequence
				int nextSceneIndex = i + 1;
				nextScene = scenes [nextSceneIndex];
				break;
			}
		}

		//print ("Before | " + PlayerPrefs.GetFloat("gametime"));
		PlayerPrefs.SetFloat("gametime", PlayerPrefs.GetFloat("gametime") + Time.timeSinceLevelLoad);
		//print ("After | " + PlayerPrefs.GetFloat("gametime"));

		//Save necessary player stats needed for the next level
		SavePlayerComponents ();
		DetermineLevelFinished (levelFinished);
		SceneManager.LoadSceneAsync (nextScene, LoadSceneMode.Single);
	}

	//Method to determine which level has finished, and award the appropriate achievement, depending on level finished index
	void DetermineLevelFinished(int levelIndex) {
		if (levelIndex == 0) {
			SlideToSurviveAchievements.archerAchievement.LevelOneFinished ();
		}
		if (levelIndex == 1) {
			SlideToSurviveAchievements.archerAchievement.LevelTwoFinished ();
		}
		if (levelIndex == 2) {
			SlideToSurviveAchievements.archerAchievement.LevelThreeFinished ();
		}
		if (levelIndex == 3) {
			SlideToSurviveAchievements.archerAchievement.LevelFourFinished ();
		}
	}

	//Method called when the players' lose. show the losing screen
	public void ActivateEndingScreen () {
		SceneManager.LoadSceneAsync ("Lose", LoadSceneMode.Single);
	}

	//Method called to load the next level
    public void InitializeNextLevel() {
        LoadNextLevel(SceneManager.GetActiveScene().name);
    }

	//Method called when player components must be initialized
	public void SetInitFlag() {
		initFlag = true;
	}
}
