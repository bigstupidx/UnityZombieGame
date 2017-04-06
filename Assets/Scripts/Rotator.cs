using UnityEngine;
using System.Collections;

//Script that rotates game objects. Customize the attributes according to the desired effect
public class Rotator : MonoBehaviour {
	public float rotationSpeed = 4.0F;

	//x, y, z degree rotations
    public int xRot;
    public int yRot;
    public int zRot;

	void Start() {
		
	}

	// Update is called once per frame
	void Update () {
		//rotate game object based on attribute inputs
		this.transform.Rotate (new Vector3 (xRot, yRot, zRot) * Time.deltaTime * rotationSpeed);
	}
}
