using UnityEngine;
using System.Collections;

/// <summary>
/// This class's responsibility is to control the functionality of the safe boundary zones, and mainly its interactions with 
/// the players movements. This script keeps track of all the safe boundaries contained in each level
/// </summary>
public class SafeBoundaryController : MonoBehaviour {
	//List of safe boundaries in the level
	public GameObject[] safeBoundaries;

	//Players one and two associations
	public GameObject playerOne;
	public GameObject playerTwo;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//Player one has entered a safe boundary some time during the course of this level
		if (playerOne != null) {
			//Trigger to determine if the player movement should be ground or ice movements
			bool onGround = false;

			//Loop through all the safe boundaries in the level to check if player one is in one of them
			for (int i = 0; i < safeBoundaries.Length; i++) {
				//This safe boundary, in this particular loop index, has the player one in it, so make the players movement be the ground movement
				if (safeBoundaries[i].GetComponent<Collider> ().bounds.Contains (playerOne.transform.position)) {
					playerOne.GetComponent<PlayerControllerMain> ().SetPlayerOnGround ();
					onGround = true;
					break;
				} 
			}

			//Player movement should be on ice
			if (onGround == false) {
				playerOne.GetComponent<PlayerControllerMain> ().SetPlayerOnIce ();
			}
		}

		//Player two has entered a safe boundary some time during the course of this level
		if (playerTwo != null) {
			//Trigger to determine if the player movement should be ground or ice movements
			bool onGround = false;

			//Loop through all the safe boundaries in the level to check if player two is in one of them
			for (int i = 0; i < safeBoundaries.Length; i++) {
				//This safe boundary, in this particular loop index, has the player two in it, so make the players movement be the ground movement
				if (safeBoundaries[i].GetComponent<Collider> ().bounds.Contains (playerTwo.transform.position)) {
					playerTwo.GetComponent<PlayerControllerMain> ().SetPlayerOnGround ();
					onGround = true;
					break;
				} 
			}

			//Player movement should be on ice
			if (onGround == false) {
				playerTwo.GetComponent<PlayerControllerMain> ().SetPlayerOnIce ();
			}
		}
	}

	/// <summary>
	/// This method is invoked when player one has entered a safe boundary
	/// </summary>
	public void PlayerOneEntered(GameObject iplayerOne) {
		playerOne = iplayerOne;
	}

	/// <summary>
	/// This method is invoked when player two has entered the safe boundary
	/// </summary>
	public void PlayerTwoEntered(GameObject iplayerTwo) {
		playerTwo = iplayerTwo;
	}
}
