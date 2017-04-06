using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;

/// <summary>
/// player one implementation of the archer skills controller main class. Bind player two specific control bindings to the
/// archer skills controller main functionality
/// </summary>
public class ArcherSkillsController : ArcherSkillsControllerMain {
	
	void Update () {
		PlayerDeathController pdc = GetComponent<PlayerDeathController> ();
		//If the player is alive
		if (pdc.isAlive()) {
			//If the cool down for the bullet is still inactive
			if (GetCurrentCoolDown () < coolDown) {
				//Update the bullet skill slider cooldown UI
				skillSlider.value = GetCurrentCoolDown ();
				Fill.color = Color.Lerp (MinCoolDown, MaxCoolDown, GetCurrentCoolDown () / coolDown);
			} 
			//Bullet skill ready, update the relevant UI component
			else {
				skillButton.interactable = true;
			}

			//The player has pressed the bullet fire control
			if (Input.GetKeyDown(KeyCode.RightControl)) {
				//If skill is able to be used
				if (SkillAvailable ()) {
					SkillActivated ();
				} 
			}
		}
	}

	//The player one's id is 2
	public override int GetPlayerId() {
		return 1;
	}
}
