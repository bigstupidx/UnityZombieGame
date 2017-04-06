using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

//Class that controls all basic zombie functionality
public class ZombieController : MonoBehaviour {

	//boolean to determine if zombie is able to move i.e not stunned
	protected bool canMove;

	protected CharacterController controller;

	protected Vector3 moveDirection;

	//To determine how often the zombie's movement will change
	protected int updateMax;
	protected int updateCount;

	//Relevant physics attributes
	protected float speed;
	protected float gravity = 40.0F;

	protected Animator anim;

	protected bool isKilled = false;

	// Use this for initialization
	protected void Start () {
		controller = GetComponent<CharacterController>();

		//Initialize the initial movement to zero
		moveDirection = Vector3.zero;

		//can move upon instantiation
		canMove = true;

		anim = GetComponent<Animator> ();
	}

	protected void OnControllerColliderHit (ControllerColliderHit hit) {
		//If collided with another zombie, change direction
		if (hit.gameObject.CompareTag ("BasicZombie")) {
			moveDirection = Vector3.zero;
		} 
	}

	void OnTriggerEnter (Collider other) {
		//If the zombie is hit by a bullet
		if (other.gameObject.CompareTag ("Bullet")) {
			//Trigger the relevant animation state
			SlideToSurviveAchievements.archerAchievement.ZombieKilled();
			anim.SetTrigger("isDying");

			GetComponent<CharacterController> ().detectCollisions = false;
			//gameObject.SetActive (false);
		} 
	}

	public bool CanMove () {
		return canMove;
	}

	//Change the zombies current movement trigger, when either being released from freeze or is hit by freeze
	public void TriggerCanMove () {
		canMove = !canMove;
	}

	//This method is invoked when the zombie is killed
	public void Killed() {
		GetComponent<Animator> ().SetTrigger ("isDying");
		isKilled = true;
	}

	public void Kill() {
		this.gameObject.SetActive (false);
	}
}
