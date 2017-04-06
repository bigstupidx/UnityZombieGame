using UnityEngine;
using System.Collections;

//This class's responsibility is to control the behaviour of the bullet skill, and how it interacts with other game objects
public class BulletController : MonoBehaviour {
	//the bullet's rigid body
	private Rigidbody rb;
	//the bullet's direction
	private Vector3 direction;

	//The player's id of the one who casted the skill
	private int playerId;

	//speed of bullet
	public float bulletSpeed;

	//start time of this skill
	private float startTime;
	//how long the bullet lasts for
	public float skillDuration = 1.5F;

	// Use this for initialization
	void Start () {
		//instantiate the appropriate attributes
		startTime = Time.timeSinceLevelLoad;
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		//How long the skill has been active
		float skillLifeTime = Time.timeSinceLevelLoad - startTime;
		//The skill has outlived its lifetime, delete the bullet
		if (skillLifeTime > skillDuration) {
			this.gameObject.SetActive (false);
		}

		rb.velocity = direction*bulletSpeed;
	}

	public void SetDirection (Vector3 idirection) {
		this.direction = idirection;
	}

	void OnTriggerEnter (Collider other) {
		//It has hit a normal zombie
		if (other.gameObject.CompareTag ("BasicZombie")) {
			//Kill the zombie by invoking the zombie's killed method
			other.gameObject.GetComponent<ZombieController> ().Killed ();
			//Invoke the appropriate method for the achievements
			SlideToSurviveAchievements.archerAchievement.ZombieKilled ();
		} 
		//It has hit the big zombie
		if (other.gameObject.CompareTag ("BigEnemy")) {
			//Allow the appropriate controller to process which player has attacked it
			BigEnemyController be = other.GetComponent<BigEnemyController> ();
			be.ProcessBigEnemyLife (playerId);
		}
	}

	public void SetPlayerId (int id) {
		playerId = id;
	}

	public int GetPlayerId () {
		return playerId;
	}

}
