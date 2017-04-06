using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// This class's responsibility is to enable all the functionalities related to the player-life system.
/// </summary>
public class PlayerDeathController : MonoBehaviour {
	//The UI element that reflects the life system
	public HealthPanelController healthPanelController;

	//Number of lives possible
	public static int maxLife = 3;

	//Number of lives this player currently has
	private int lives = 3;

	//Whether this player is alive or dead
	private bool alive = true;

	void Update() {
		//Return the booleans of whether the two players are alive during this frame
		bool p1Alive = GameObject.Find ("PlayerOne_Green").GetComponent<PlayerDeathController> ().isAlive();
		bool p2Alive = GameObject.Find("PlayerTwo_Blue").GetComponent<PlayerDeathController> ().isAlive();

		//If both players are dead, then the game has ended
		if (!p1Alive && !p2Alive) {
			SlideToSurviveGameController.gameController.ActivateEndingScreen ();
		}
	}

	//Upon collision with a zombie
	void OnControllerColliderHit (ControllerColliderHit hit) {
		if (hit.gameObject.CompareTag ("BasicZombie")) {
			//If the player is not immune (when recently hit) and is alive
			if(!gameObject.tag.Equals("Immune") && alive) {
				//Invoke the necessary function
				HitByZombie ();
			}
		}

		//If the player has hit a player tomb, i,e the dead player
		if (hit.gameObject.CompareTag ("PlayerTomb")) {
			//Remove the tomb gameobject
			hit.gameObject.SetActive (false);
			PlayerDeathController pdc = hit.transform.GetComponentInParent<PlayerDeathController> ();
			PlayerControllerMain pcm = hit.transform.GetComponentInParent<PlayerControllerMain> ();

			//If it was player one that was the dead player
			if (pcm.GetPlayerId () == 1) {
				GameObject playerOne = GameObject.Find ("PlayerOne_Green");
				//activate the player's rendering meshes again
				playerOne.transform.GetChild (2).gameObject.SetActive (true);
			}

			//If it was player two that was the dead player, do likewise
			if (pcm.GetPlayerId () == 2) {
				GameObject playerTwo = GameObject.Find ("PlayerTwo_Blue");
				playerTwo.transform.GetChild (2).gameObject.SetActive (true);
			}

			//set it to alive
			pdc.ToggleAlive ();
			//Has one life
			pdc.SetLives (1);
        }
	}

	void OnTriggerEnter (Collider other) {
		//has hit a life giving pick up 
		if (other.gameObject.CompareTag ("GiveLife")) {
			//If the player currently doesnt have max lives, then consume the pickup
			if (lives != maxLife) {
				lives++;
				//Update the UI to reflect the player's current lives
				healthPanelController.UpdateLives (lives);
				//Remove the pickup
				other.gameObject.SetActive (false);
			}
		}
	}

	/// <summary>
	/// Method invoked when player has been hit by zombies (Or any other enemy attack, refactoring the method name at this 
	/// point is too much risk)
	/// </summary>
	public void HitByZombie() {
		//If the player has enough lives to remain living
        if (lives > 0) {
			//Update necessary UI and attribute
			lives--;
			healthPanelController.UpdateLives (lives);
        }

		//If the player has no more lives
		if (lives == 0) {
			//Activate the tomb
			GameObject tomb = transform.GetChild (1).gameObject;
			tomb.SetActive (true);
			//Disable the player's rendering meshes
			transform.GetChild (2).gameObject.SetActive (false);
			alive = false;
		} 

		//Player still has lives, but has been hit. Therfore, make the player immune for a few seconds
		else {
			StartCoroutine (CollideFlash ());
		}
	}

	//Called when the player has a life taken off of. 
	IEnumerator CollideFlash () {
		//Make the player immune for the duration
		gameObject.tag = "Immune";
		//Blink the player's blinker game object on and off, for the duration the player will be immune
		GameObject blinker = transform.GetChild (0).gameObject;
		blinker.SetActive (true);
		yield return new WaitForSeconds (0.2F);
		blinker.SetActive (false);
		yield return new WaitForSeconds (0.2F);
		blinker.SetActive (true);
		yield return new WaitForSeconds (0.2F);
		blinker.SetActive (false);
		yield return new WaitForSeconds (0.2F);
		blinker.SetActive (true);
		yield return new WaitForSeconds (0.2F);
		blinker.SetActive (false);
		yield return new WaitForSeconds (0.2F);

		//Make the player not immune again
		gameObject.tag = "Player";
	}

	public HealthPanelController GetHealthPanelController() {
		return healthPanelController;
	}

	public void SetLives(int lifeCount) {
		lives = lifeCount;
		healthPanelController.UpdateLives (lives);
	}

	public int GetLives () {
		return lives;
	}

	public bool isAlive () {
		return alive;
	}

	public void ToggleAlive () {
		alive = !alive;
	}
}
