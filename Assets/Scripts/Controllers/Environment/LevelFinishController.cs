using UnityEngine;
using System.Collections;

public class LevelFinishController : MonoBehaviour {
    GameObject playerOne;
    GameObject playerTwo;

	private bool nextLevel = true;
	
	// Update is called once per frame
	void Update () {
		if (playerOne != null && playerTwo != null && nextLevel) {
            if (GetComponent<Collider>().bounds.Contains(playerOne.transform.position) && GetComponent<Collider>().bounds.Contains(playerTwo.transform.position))
            {
				SlideToSurviveGameController.gameController.InitializeNextLevel();
				nextLevel = false;
            }
        }
	}

    public void PlayerOneReachedEnd(GameObject iplayerOne) {
        playerOne = iplayerOne;
    }

    public void PlayerTwoReachedEnd(GameObject iplayerTwo) {
        playerTwo = iplayerTwo;
    }
}
