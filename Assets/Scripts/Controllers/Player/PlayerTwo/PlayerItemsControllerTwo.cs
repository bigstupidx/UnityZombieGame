using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Player two's implementation of the player items controller. Link player two specific controls to the relevant functionality contained
/// in the main class
/// </summary>
public class PlayerItemsControllerTwo : PlayerItemsControllerMain {

	// Update is called once per frame
	void Update () {
		PlayerDeathController pdc = GetComponent<PlayerDeathController> ();
		//If the player is alive
		if (pdc.isAlive ()) {
			//If the player has enough currency, and the appropriate button pressed
			if ((currencyCount>=freezeCost) && Input.GetKeyDown (KeyCode.Alpha2)) {
				//Freeze ability activated from player two
				FreezeAbilityActivated (2);
			}
			//If the player has enough currency, and the appropriate button pressed
			if ((currencyCount>=barrierCost) && Input.GetKeyDown (KeyCode.Alpha3)) {
				//Barrier ability activated from player two
				BarrierAbilityActivated (2);
			}
		}
	}
}
