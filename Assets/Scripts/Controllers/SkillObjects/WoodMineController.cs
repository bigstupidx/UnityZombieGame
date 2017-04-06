using UnityEngine;
using System.Collections;

public class WoodMineController : MonoBehaviour {

	//start time of this skill
	private float startTime;
	public float skillDuration = 10.0F;

	//private bool illuminateActive;

	//private float lightIntensity = 3.0F;

	// Use this for initialization
	void Start () {
        //illuminateActive = false;
        startTime = Time.timeSinceLevelLoad;
	}
	
	// Update is called once per frame
	void Update () {
		float skillLifeTime = Time.timeSinceLevelLoad - startTime;
		if (skillLifeTime > skillDuration) {
            //GetComponentInChildren<Light>().range = 1;
        }
	}

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.CompareTag ("Bullet")) {
            //illuminateActive = true;
            GetComponentInChildren<Light>().range = 30;
            GetComponentInChildren<Light>().intensity = 8;
        } 
	}
}
