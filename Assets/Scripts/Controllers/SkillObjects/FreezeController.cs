using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//This class's responsibility is to dictate the behaviour of the freeze skill, and its interactions with other game objects
public class FreezeController : MonoBehaviour {
	//List of zombies within this freeze zone
	private List<ZombieController> frozenZombies = new List<ZombieController>();
	//start time of this skill
	private float startTime;
	//How long the freeze skill will last
	public float skillDuration = 2.5F;

	public GameObject frozenZombieTombPrefab;

	//List of zombie tombs instantiated
	private List<GameObject> frozenZombieTombs = new List<GameObject>();

	// Use this for initialization
	void Start () {
		startTime = Time.timeSinceLevelLoad;
	}
	
	// Update is called once per frame
	void Update () {
		//Life time of the freeze skill
		float skillLifeTime = Time.timeSinceLevelLoad - startTime;
		//It has outlived its lifetime, free the zombies and destroy this skill
		if (skillLifeTime > skillDuration) {
			//make zombies move again
			foreach(ZombieController zc in frozenZombies) {
				zc.TriggerCanMove ();
			}

			//remove the frozen zombie tombs
			foreach(GameObject tomb in frozenZombieTombs) {
				tomb.gameObject.SetActive (false);
			}
			//Destroy the freeze skill
			this.gameObject.SetActive (false);
		}
	}

	void OnTriggerEnter (Collider other) {
		//Zombie has entered the freeze zone
		if (other.gameObject.CompareTag ("BasicZombie")) {
			ZombieController zc = other.gameObject.GetComponent<ZombieController> ();

			//The zombie is currently moving
			if (zc.CanMove ()) {
				//Make it stop
				zc.TriggerCanMove ();
				//Add this zombie to this skills list of frozen zombies
				frozenZombies.Add (zc);
				SlideToSurviveAchievements.archerAchievement.ZombieFrozen ();

				//make frozen zombie tomb in that spot
				GameObject frozenTomb = (GameObject)Instantiate (frozenZombieTombPrefab, other.gameObject.transform.position, Quaternion.identity);
				frozenZombieTombs.Add (frozenTomb);
			}
		} 
	}
}
