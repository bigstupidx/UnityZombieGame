using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LurkZombieController : ZombieController {

	// Use this for initialization
	void Start () {
		base.Start ();
		updateCount = 0;
		updateMax = 60;

		speed = 0.3F;
	}

	// Update is called once per frame
	void Update () {
		//Time to change zombie movement
		if (updateCount >= updateMax) {
			updateCount = 0;
			//randomize the movements along x and z axis
			float r1 = Random.Range (-10.0F, 10.0F);
			float r2 = Random.Range (-10.0F, 10.0F);

			moveDirection = new Vector3 (r1, 0.5F, r2);
			moveDirection = transform.TransformDirection (moveDirection);
			transform.LookAt (moveDirection);
			MoveZombie ();
		} else {
			MoveZombie ();
			updateCount++;
		}
	}

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.CompareTag ("Boundary") || other.gameObject.CompareTag ("LevelFinishBoundary")) {
			moveDirection = Vector3.zero - moveDirection;
		}
	}

	void MoveZombie() {
		//if the zombie is not stunned:
		if (canMove) {
			//The zombie is floating, apply gravity 
			//(Most likely due to collision with other zombie. so instead of applying gravity per second do it immediately)
			if (!controller.isGrounded) {
				moveDirection.y -= gravity * Time.deltaTime;
			}
			//The zombie is floating, apply gravity 
			//(Most likely due to collision with other zombie. so instead of applying gravity per second do it immediately)
			controller.Move (moveDirection * Time.deltaTime * speed);
		} 
	}
}
