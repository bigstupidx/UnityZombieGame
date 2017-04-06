using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Networking;

/// <summary>
/// Player two implementation of the player controller main. Link player controller main functionality with the 
/// relevant player two controls.
/// </summary>
public class PlayerControllerTwo : PlayerControllerMain {

	//Get the rotation, based on player two's input
	protected override float GetInputRotation ()
	{
		return Input.GetAxis("Horizontal_Player2") * 180.0F;
	}

	//Get the player's movement, based on player two's input binding
	protected override Vector3 GetInputMovement ()
	{
		Vector3 movement = new Vector3 (Input.GetAxis ("Horizontal_Player2"), 0, Input.GetAxis ("Vertical_Player2"));
		return movement;
	}

	//Player two's id is 2
    public override int GetPlayerId()
    {
        return 2;
    }

	//Check if the input is horizontal input, relative to player two's control binding
    protected override bool IsHorizontalInput()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            return true;
        }
        else {
            return false;
        }
    }

	//Check if the input is vertical input, relative to player two's control binding
    protected override bool IsVerticalInput()
    {
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
