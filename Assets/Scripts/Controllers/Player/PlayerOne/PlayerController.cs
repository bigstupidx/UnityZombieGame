using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Networking;

/// <summary>
/// Player one implementation of the player controller main. Link player controller main functionality with the 
/// relevant player one controls.
/// </summary>
public class PlayerController : PlayerControllerMain {

	//Get the rotation, based on player one's input
	protected override float GetInputRotation ()
	{
		return Input.GetAxis("Horizontal") * 180.0F;
	}

	//Get the player's movement, based on player one's input binding
	protected override Vector3 GetInputMovement ()
	{
		Vector3 movement = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
		return movement;
	}

	//Player one's id is 1
    public override int GetPlayerId()
    {
        return 1;
    }

	//Check if the input is horizontal input, relative to player one's control binding
    protected override bool IsHorizontalInput()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

	//Check if the input is vertical input, relative to player one's control binding
    protected override bool IsVerticalInput()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
