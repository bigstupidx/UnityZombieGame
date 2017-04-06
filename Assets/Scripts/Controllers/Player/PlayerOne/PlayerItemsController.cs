using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Player one's implementation of the player items controller. Link player two specific controls to the relevant functionality contained
/// in the main class
/// </summary>
public class PlayerItemsController : PlayerItemsControllerMain {
	// Update is called once per frame
	void Update () {
		PlayerDeathController pdc = GetComponent<PlayerDeathController> ();
		//If the player is alive
		if (pdc.isAlive ()) {
			//If the player has enough currency, and the appropriate button pressed
			if ((currencyCount>=freezeCost) && Input.GetKeyDown (KeyCode.Keypad8)) {
				//Freeze ability activated from player one
				FreezeAbilityActivated (1);
			}

			//If the player has enough currency, and the appropriate button pressed
			if ((currencyCount>=barrierCost) && Input.GetKeyDown (KeyCode.Keypad9)) {
				//Barrier ability activated from player one
				BarrierAbilityActivated (1);
			}
		}
	}
}
