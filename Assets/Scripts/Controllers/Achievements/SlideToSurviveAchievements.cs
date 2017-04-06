using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// This class's responsibility is to control all functionality related to achievements. Instances of this class will be a singleton,
/// so that achievements are global and persistent across levels. 
/// </summary>
public class SlideToSurviveAchievements : MonoBehaviour {
	//These are the indices associated with the paritcular achievements the numbers are mapped to. These will translate 
	//to entries in the array 
	private int zombieSlayerIndex = 0;
	private int marksmanIndex = 1;
	private int banditIndex = 2;
	private int collectorIndex = 3;
	private int sprayAndPrayIndex = 4;
	private int explorerIndex = 5;
	private int noobFreezerIndex = 6;

	private int lvl1FinishIndex = 7;
	private int lvl2FinishIndex = 8;
	private int lvl3FinishIndex = 9;
	private int lvl4FinishIndex = 10;
	private int lvl5FinishIndex = 11;

	private int guardsmanIndex = 12;
	private int guardianIndex = 13;

	private int bigBulletIndex = 14;
	private int tripleBulletIndex = 15;

	public Sprite unlockIcon;

	//The array of achievement titles, respective to the achievements' indices
	private string[] achievementTitles = {
		"Zombie Slayer",
		"Zombie-murdering marksman",
		"Bandit",
		"Collector",
		"Spray n Pray",
		"The explorer",
		"Freeze nooblet",

		"Rookie",
		"Prodigy",
		"I ain't no noob",
		"Boot camp graduate",
		"mlg pro",

		"On guard!",
		"A Guardian's defence",

		"Obtained big bullet",
		"Obtained triple shot"
	};

	//The array of achievement messages, respective to the achievements' indices
	private string[] achievementMessages = {
		"Rewarded upon killing 20 zombies",
		"Rewarded upon killing 30 zombies",
		"Rewarded upon collecting 10 coins",
		"Rewarded upon collecting 40 coins",
		"Rewarded upon firing 30 shots",
		"an adventurous person",
		"Rewarded upon freezing 10 enemies",

		"Rewarded upon completing level 1",
		"Rewarded upon completing level 2",
		"Rewarded upon completing level 3",
		"Rewarded upon completing level 4",
		"Rewarded upon completing level 5",

		"Rewarded upon blocking 10 fireballs",
		"Rewarded upon blocking 40 fireballs",

		"Rewarded upon killing 5 zombies",
		"Rewarded upong killing 8 zombies"
	};

	//Groupings of the data associated with unlocking the achievements, and the boundaries for which the 
	//data must reach in order to get those achievements
	private int zombieKillCount = 0;
	public int zombieSlayerBoundary = 20;
	public int marksmanBoundary = 30;

	private int coinsCollected = 0;
	public int banditBoundary = 10;
	public int collectorBoundary = 40;

	private int shotsFired = 0;
	public int sprayAndPrayBoundary = 30;

	private int freezeCount = 0;
	public int noobfreezerBoundary = 10;

	private int fireballBlockedCount = 0;
	public int guardsmanBoundary = 10;
	public int guardianBoundary = 40;

	private int diamondCount = 0;

	//Temporary in game achievements, based on the player's zombie killing performance
	public int bigBulletBoundary = 5;
	public int tripleBulletBoundary = 8;
	private int bigBulletKillsCount = 0;
	private int tripleBulletKillsCount = 0;

	public static SlideToSurviveAchievements archerAchievement;

	void Awake () {
		//Singleton pattern functionality
		if (archerAchievement == null) {
			DontDestroyOnLoad (gameObject);
			archerAchievement = this;
		} 

		else if (archerAchievement != this) {
			Destroy (gameObject);
		}
	}

	//These are the methods called when particular levels are finished, so that relevant achievements can be unlocked (If
	//Not already unlocked)
	public void LevelOneFinished() {
		SaveAchievement (lvl1FinishIndex, true);
	}
	public void LevelTwoFinished() {
		SaveAchievement (lvl2FinishIndex, true);
	}
	public void LevelThreeFinished() {
		SaveAchievement (lvl3FinishIndex, true);
	}
	public void LevelFourFinished() {
		SaveAchievement (lvl4FinishIndex, true);
	}
	public void LevelFiveFinished() {
		SaveAchievement (lvl5FinishIndex, true);
	}

	//This method is invoked when a fireball is blocked by a barrier
	public void FireBallBlocked() {
		//Increment relevant data for this achievement
		fireballBlockedCount = fireballBlockedCount + 1;

		//Check if the data meets the boundaries, if they do, then unlock the achievement (if not already unlocekd)
		if (fireballBlockedCount == guardsmanBoundary) {
			SaveAchievement (guardsmanIndex, false);
		}

		if (fireballBlockedCount == guardianBoundary) {
			SaveAchievement (guardianIndex, false);
		}
	}

	//This method is invoked when a zombie has been killed
	public void ZombieKilled () {
		//Increment relevant data for this achievement
		zombieKillCount = zombieKillCount + 1;
		//The in-game achievement data 
		bigBulletKillsCount = bigBulletKillsCount + 1;
		tripleBulletKillsCount = tripleBulletKillsCount + 1;

		//Check if the data meets the boundaries, if they do, then unlock the achievement (if not already unlocekd)
		if (zombieKillCount == zombieSlayerBoundary) {
			SaveAchievement (zombieSlayerIndex, false);
		}

		if (zombieKillCount == marksmanBoundary) {
			SaveAchievement (marksmanIndex, false);
		}

		//In game achievments, aka upgrades. Check if the conditions for the upgrades are met. This upgrade is the big bullet
		if (bigBulletKillsCount >= bigBulletBoundary) {
			bigBulletKillsCount = 0;

			//Unlock the upgrade for both the players
			GameObject playerOne = GameObject.Find ("PlayerOne_Green");
			GameObject playerTwo = GameObject.Find ("PlayerTwo_Blue");

			playerOne.GetComponent<ArcherSkillsControllerMain> ().StartStreakOne();
			playerTwo.GetComponent<ArcherSkillsControllerMain> ().StartStreakOne();
			SaveAchievement (bigBulletIndex, false);
		}

		//In game achievments, aka upgrades. Check if the conditions for the upgrades are met. This upgrade is the triple shot
		if (tripleBulletKillsCount >= tripleBulletBoundary) {
			tripleBulletKillsCount = 0;

			//Unlock the upgrade for both the players
			GameObject playerOne = GameObject.Find ("PlayerOne_Green");
			GameObject playerTwo = GameObject.Find ("PlayerTwo_Blue");

			playerOne.GetComponent<ArcherSkillsControllerMain> ().StartStreakTwo();
			playerTwo.GetComponent<ArcherSkillsControllerMain> ().StartStreakTwo();
			SaveAchievement (tripleBulletIndex, false);
		}
	}

	//This method is invoked when a player fires a shot
	public void ArrowFired () {
		//Increment relevant data for this achievement
		shotsFired++;

		//Check if the data meets the boundaries, if they do, then unlock the achievement (if not already unlocekd)
		if (shotsFired == sprayAndPrayBoundary) {
			SaveAchievement (sprayAndPrayIndex, false);
		}
	}

	//This method is invoked when a zombie freezes
	public void ZombieFrozen () {
		//Increment relevant data for this achievement
		freezeCount++;

		//Check if the data meets the boundaries, if they do, then unlock the achievement (if not already unlocekd)
		if (freezeCount == noobfreezerBoundary) {
			SaveAchievement (noobFreezerIndex, false);
		}
	}

	//This method is invoked when a coin is collected 
	public void CoinCollected () {
		//Increment relevant data for this achievement
		coinsCollected = coinsCollected + 1;

		//Check if the data meets the boundaries, if they do, then unlock the achievement (if not already unlocekd)
		if (coinsCollected == banditBoundary) {
			SaveAchievement (banditIndex, false);
		}

		if (coinsCollected == collectorBoundary) {
			SaveAchievement (collectorIndex, false);
		}
	}

	public void DiamondCollected () {
		diamondCount = diamondCount + 1;
	}

	/// <summary>
	/// This method is called when an achievement condition has been met. Contains the index relevant to that achievement, and 
	/// a check to see if it is a level achievment
	/// </summary>
	void SaveAchievement (int achievementIndex, bool levelAchievement) {
		//Get the achievement title and messsage, respective to the achievmeent index given to it
		string achievementTitle = achievementTitles [achievementIndex];
		string achievementMessage = achievementMessages [achievementIndex];

		//The player hasn't achieved it yet, so take steps to make the achievment persist
		if (!PlayerPrefs.HasKey (achievementTitle)) {
			PlayerPrefs.SetString (achievementTitle, achievementTitle);
			PlayerPrefs.SetString (achievementMessage, achievementMessage);

			//Display only if the achievements haven't been completed yet
			DisplayAchievement (achievementTitle, achievementMessage, levelAchievement);
		}
	}

	/// <summary>
	/// Gets all achievements unlocked in a list form. The list elements are a type of AchievementContainer, and these 
	/// contain all the necessary information associated with each achievmeent
	/// </summary>
	/// <returns>The achievements.</returns>
	public List<AchievementContainer> GetAchievements() {
		List<AchievementContainer> achievementList = new List<AchievementContainer> ();

		//For all the achievements available
		for(int i = 0; i < achievementTitles.Length; i++) {
			//Get the relevant fields, respective to the arrays
			string aTitle = achievementTitles [i];
			string aMessage = achievementMessages [i];
			bool aUnlocked = false;

			//If the player already has this achivement, then set the relevant trigger
			if (PlayerPrefs.HasKey (achievementTitles [i])) {
					aUnlocked = true;
			}

			//Add the element to return list
			achievementList.Add (new AchievementContainer(aTitle, aMessage, aUnlocked, unlockIcon));
		}

		return achievementList;
	}

	void DisplayAchievement (string title, string message, bool levelAchievement) {
		GetComponent<AchievementDisplay>().DisplayAchievement (title, message, levelAchievement);
	}
}

