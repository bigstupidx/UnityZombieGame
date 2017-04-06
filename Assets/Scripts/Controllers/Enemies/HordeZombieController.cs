using UnityEngine;
using System.Collections;

public class HordeZombieController : ZombieController {
	//The zone that this zombie must operate in
	public GameObject zoneObject;
	//The bounds of the zone that this zombie operates in
	private Bounds zone;

	// Use this for initialization
	protected new void Start () {
		base.Start ();
		//Get the zombie zone collider region
		zone = zoneObject.GetComponent<Collider>().bounds;

		updateCount = 0;
		updateMax = 40;

		speed = 0.3F;

		//can move upon instantiation
		canMove = true;
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
			transform.LookAt (moveDirection);
			MoveZombie ();
		} else {
			MoveZombie ();
			updateCount++;
		}
	}

	void MoveZombie() {
		//if the zombie is not stunned:
		if (canMove) {
			controller.Move (moveDirection * Time.deltaTime * speed);
			//The zombie is floating, apply gravity 
			//(Most likely due to collision with other zombie. so instead of applying gravity per second do it immediately)
			if (!controller.isGrounded) {
				moveDirection.y -= gravity * Time.deltaTime;
			}

			if (zone.Contains (transform.position)) {
			} else {
				moveDirection = Vector3.zero - moveDirection;
			}
		} 
	}
}
