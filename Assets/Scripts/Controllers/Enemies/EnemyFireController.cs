using UnityEngine;
using System.Collections;

public class EnemyFireController : MonoBehaviour {

	private Rigidbody rb;
	private Vector3 direction;

	public float bulletSpeed;

	//start time of this skill
	private float startTime;

	// Use this for initialization
	void Start () {
		startTime = Time.timeSinceLevelLoad;
		rb = GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	void Update () {
		float skillLifeTime = Time.timeSinceLevelLoad - startTime;

		rb.velocity = direction*bulletSpeed;
	}

	public void SetDirection (Vector3 idirection) {
		this.direction = idirection;
	}

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.CompareTag ("Player")) {
			PlayerDeathController pdc = other.GetComponent<PlayerDeathController> ();
			pdc.HitByZombie ();
		} 

		if (other.gameObject.CompareTag ("BarrierLevel1") || other.gameObject.CompareTag ("BarrierLevel2")) {
			SlideToSurviveAchievements.archerAchievement.FireBallBlocked ();
			gameObject.SetActive (false);
		}

        if (other.gameObject.CompareTag("IceZoneWall") || other.gameObject.CompareTag("Door") || other.gameObject.CompareTag("Boundary") || other.gameObject.CompareTag("LevelFinishBoundary"))
        {
            gameObject.SetActive(false);
        }
    }
}
