using UnityEngine;
using System.Collections;

//This class's responsibility is to control the behaviour of the barrier skill.
public class BarrierController : MonoBehaviour {
	//start time of this skill
	private float startTime;
	//Skill dissappears after the allocated value
	public float skillDuration = 10.0F;

	//The barrier's position at time of skill instantiation
	private Vector3 barrierStartPosition;

	//Player who casted this ability
	private GameObject player;

	// Use this for initialization
	void Start () {
		startTime = Time.timeSinceLevelLoad;
	}

	// Update is called once per frame
	void Update () {
		//How long the skill has been active for
		float skillLifeTime = Time.timeSinceLevelLoad - startTime;

		//Exceeded its lifetime, so disable the skill
		if (skillLifeTime > skillDuration) {
			this.gameObject.SetActive (false);
		}

		//If this barrier skill is in its level2 form, then move the barrier with the player
		if (gameObject.CompareTag ("BarrierLevel2")) {
			transform.position = player.transform.position;
		}
	}

	void OnTriggerEnter (Collider other) {
		
	}

	public void SetPlayer(GameObject iplayer) {
		player = iplayer;
	}
}
