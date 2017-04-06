using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//This class's responsibility is to manage the lives-system UI, and tie it with the lives-system functionality. 
public class HealthPanelController : MonoBehaviour {
	
	//The images of the hearts, with each heart corresponding to one life. number of hearts dependent on max lives possible
	private Image[] lives = new Image[PlayerDeathController.maxLife];

	// Use this for initialization
	void Start () {
		//Initialize the hearts array
		for (int i = 0; i < lives.Length; i++) {
			lives [i] = transform.GetChild (i).gameObject.GetComponent<Image>();
		}
	}

	public void UpdateLives (int currentlives) {
		//Number of hearts activated
		int changeIndex = 0;

		for (int i = 0; i < lives.Length; i++) {
			//If there are less hearts activated (i.e visible on UI) than there are player lives, then enable another heart
			if (changeIndex < currentlives) {
				lives [i].enabled = true;
			} 
			//UI accurately reflects number of player lives, so stop activating hearts
			else {
				lives [i].enabled = false;
			}
			changeIndex++;
		}
	}
	
	// Update is called once per frame
	void Update () {}
}
