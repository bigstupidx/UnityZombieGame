using UnityEngine;
using System.Collections;

public class WallTrapController : MonoBehaviour {
	public float coolDown = 5.0F;

	private float lastUsedTime;

	public GameObject fireBall;

	// Use this for initialization
	void Start () {
		lastUsedTime = Time.timeSinceLevelLoad;
	}
	
	// Update is called once per frame
	void Update () {
		if (SkillAvailable ()) {
			lastUsedTime = Time.timeSinceLevelLoad;

			Vector3 position = transform.position;
			Quaternion rotation = transform.rotation;
			GameObject fireball = (GameObject)Instantiate(fireBall, position, Quaternion.identity);
			EnemyFireController ef = fireball.GetComponent<EnemyFireController>();
			Vector3 fireballDirection = rotation * Vector3.forward;
			ef.SetDirection(fireballDirection);
		}	
	}

	bool SkillAvailable()
	{
		if (Time.timeSinceLevelLoad - lastUsedTime > coolDown)
		{
			return true;
		}

		return false;
	}
}
