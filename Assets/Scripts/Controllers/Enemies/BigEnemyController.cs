using UnityEngine;
using System.Collections;

/// <summary>
/// This class's responsibility is to control the behaviour of the big zombie type enemy. This particular enemy must be hit 
/// by both player one and player two, in a specific period of time, to be killed 
/// </summary>
public class BigEnemyController : MonoBehaviour {
	//The triggers to determine if player one and two have recently attacked the zombie
	private bool playerOneHit;
	private bool playerTwoHit;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//This method is invoked when the zombie is hit by one of the players, identified the player id
	public void ProcessBigEnemyLife (int playerId) {
		//Make the zombie vulnerable for a short time
		StartCoroutine (TagTimeout());
		//Trigger the relevant trigger
		if (playerId == 1) {
			playerOneHit = true;
		}
		if (playerId == 2) {
			playerTwoHit = true;
		}

		//If the big zombie has been hit by both players, and the timeout has not exceeded
		if (playerOneHit && playerTwoHit) {
			//Set the relevant achievement
			SlideToSurviveAchievements.archerAchievement.ZombieKilled ();
			GetComponent<Animator>().SetTrigger("isDead");
			//gameObject.SetActive (false);
		} 

		//Not dead yet
		else {
			GetComponent<Animator>().SetTrigger("isHit");
		}
	}

	IEnumerator TagTimeout() {
		//After the allocated time, make the triggers set to false. 
		yield return new WaitForSeconds (2.5F);
		playerOneHit = false;
		playerTwoHit = false;
	}

	public void Kill() {
		this.gameObject.SetActive (false);
	}
}




