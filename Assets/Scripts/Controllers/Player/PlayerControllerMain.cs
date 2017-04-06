using UnityEngine;
using System.Collections;

//This class's responsibility is to dictate the player's basic mechanics, such as movement on ice and movement on ground, 
public class PlayerControllerMain : MonoBehaviour {
	//Triggers that signal whether the player is on ice or ground	
	protected static string ON_ICE = "ONICE";
	protected static string ON_GROUND = "ONGROUND";

	//The current type of ground the player is on, based on the player's last known location
	protected string currentType;

	//Speed factor whilst on ground
	public float speed = 3.0F;
	//Turning speed during player rotation
	public float turnSpeed = 160.0F;
	//Maximum speed possible on ice
	public float maxSpeed = 14.0F;
	//Gravity on character controller
	public float gravity = 40.0F;

	//Move direction accumulated whilst on ice region
	protected Vector3 moveDirection;
	protected CharacterController controller;
	//Boundary of the last collided safe region
	protected Collider boundary;

	protected Animator anim;

	protected float n;

	void Start () {
		moveDirection = Vector3.zero;
		controller = GetComponent<CharacterController>();
		//The player must start on safe region
		currentType = ON_GROUND;
		anim = GetComponent<Animator>();
	}

	void Update () {
		PlayerDeathController pdc = GetComponent<PlayerDeathController> ();
		//If this player is alive, then process movement
		if (pdc.isAlive()) {
			AnimatorStateInfo currentBaseState = anim.GetCurrentAnimatorStateInfo(0);

			if (currentBaseState.IsName("Base Layer.standing_aim_recoil") || currentBaseState.IsName("Base Layer.standing_draw_arrow"))
			{          
				return;
			}

			if (currentType.Equals (ON_ICE)) {
				IceMovement ();
			}

			if (currentType.Equals (ON_GROUND)) {
				//The movement vector dependent on input. add in some speed
				GroundMovement();
			}
		}
	}

	protected void IceMovement() {
		slide();

		//The movement vector dependent on input. accumulate the ice speed
		Vector3 movement = GetInputMovement();
		moveDirection = moveDirection + movement;

		//if accumulative speed exceeds the maximum, roll back change
		if (moveDirection.magnitude > maxSpeed) {
			moveDirection = moveDirection - movement;
		} 

		//The player is floating, so apply gravity per second
		if(!controller.isGrounded) {
			moveDirection.y -= gravity * Time.deltaTime;
		}

		//Assign the accumulated speed to a temporary variable for calculation, otherwise update calls on a dynamically changing value
		Vector3 currentMovement = moveDirection;
		MovePlayer (currentMovement);
	}

	protected void GroundMovement() {
		Vector3 movement = GetInputMovement();
		//no movement 
		if (movement.normalized.magnitude == 0)
		{
			anim.SetBool("isRunning", false);
		}
		//move on ground
		else
		{
			anim.SetBool("isRunning",true);
		}

		movement *= speed;
		MovePlayer (movement);
	}

	void MovePlayer (Vector3 movement) {
		float rotation = GetInputRotation();
		rotation *= Time.deltaTime;

		if (movement.magnitude != 0.0F)
		{
			transform.Rotate(0, rotation, 0);
		}
		else
		{
			if (rotation < 0f)
			{
				//anim.SetFloat("Direction", -1f);
				transform.Rotate(0, -90f * Time.deltaTime, 0);

			}
			else if (rotation > 0f)
			{
				//anim.SetFloat("Direction", 1f);
				transform.Rotate(0, 90f * Time.deltaTime, 0);
			}
			else
			{
				//anim.SetFloat("Direction", 0f);
			}

		}

        //translate vector to world dimensions, and then move the player 
        //(This is necessary so that movement mechanics are relative to the player's orientation i.e. the way they are facing)
        if (IsVerticalInput() || currentType.Equals(ON_ICE)) {
            movement = transform.TransformDirection (movement);
            controller.Move (movement * Time.deltaTime);
        }
    }

	void OnTriggerEnter (Collider other) {
		//has crossed ice-ground boundary, change behaviour
		if (other.gameObject.CompareTag ("Boundary")) {
			if (GetPlayerId() == 1) {
				//other.gameObject.GetComponent<SafeBoundaryController>().PlayerOneEntered(this.gameObject);
			}
			if (GetPlayerId() == 2)
			{
				//other.gameObject.GetComponent<SafeBoundaryController>().PlayerTwoEntered(this.gameObject);
			}
		}
		//load the next level upon reaching the level progress region
		if (other.gameObject.CompareTag ("LevelFinishBoundary")) {
            if (GetPlayerId() == 1) {
                other.gameObject.GetComponent<LevelFinishController>().PlayerOneReachedEnd(this.gameObject);
            }
            if (GetPlayerId() == 2)
            {
                other.gameObject.GetComponent<LevelFinishController>().PlayerTwoReachedEnd(this.gameObject);
            }
		}
	}

	public void SetPlayerOnGround() {
		currentType = ON_GROUND;
		moveDirection = Vector3.zero;
	}

	public void SetPlayerOnIce() {
		currentType = ON_ICE;
	}

	void slide()
	{
		anim.SetTrigger("isSliding");
		float direction = anim.GetFloat("Direction");
		if (direction >= 1)
		{
			n = -1;
		}
		else if (direction <= 0)
		{
			n = 1;
		}
		anim.SetFloat("Direction", direction + 0.02f * n);
	}

	protected virtual float GetInputRotation() {
		return 0.0F;
	}

	//Get input movement of this player, to be implemented by player one/two implementations
    protected virtual Vector3 GetInputMovement()
    {
        return Vector3.zero;
    }

	//Determines if input is horizontal input
    protected virtual bool IsHorizontalInput() {
        return false;
    }

	//Determines if input is vertical input
    protected virtual bool IsVerticalInput()
    {
        return false;
    }

    public virtual int GetPlayerId() {
        return 0;
    }
}
