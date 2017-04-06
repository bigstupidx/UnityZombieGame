using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//This class's responsibility is to control all behaviour associated with the player's skill function. This class is extended
//by playerone and playertwo, who will have unique attributes like their player ids
public class ArcherSkillsControllerMain : MonoBehaviour {
	//This player's animator
	protected Animator anim;
	//The player game object
	protected GameObject player;

	//The bullet skill cooldown
	protected float coolDown;

	//UI components associated with this bullet skill
	public Button skillButton;
	//Cooldown slider
	public Slider skillSlider;
	//The slider's fill area
	public Image Fill;
	//Color scheme of the cooldown slider; full green shows ready, and full red shows that it has just been used
	public Color MaxCoolDown = Color.green;
	public Color MinCoolDown = Color.red;

	//The last time that this player used the bullet skill
	protected float lastUsedTime;

	//prefab for the big bullet
	public GameObject megaBulletPrefab;
	//prefab for ordinary bullets
	public GameObject bulletPrefab;

	//Streak one corresponds to the big bullet upgrade being ready, streak two for the triple shot upgrade
	protected bool streakOne;
	protected bool streakTwo;

	void Start()
	{
		//Initialize the appropriate attributes
		anim = GetComponent<Animator>();
		player = this.gameObject;

		coolDown = 4.0F;
		lastUsedTime = Time.timeSinceLevelLoad;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <returns>The time remaining.</returns>
	public float GetCoolDownRemaining()
	{
		float difference = Time.timeSinceLevelLoad - lastUsedTime;
		return coolDown - difference;
	}

	/// <summary>
	/// Gets the current cool down, i.e the amount of time that has lapsed since the last time the bullet skill has been used
	/// </summary>
	/// <returns>The current cool down progress.</returns>
	public float GetCurrentCoolDown()
	{
		float difference = Time.timeSinceLevelLoad - lastUsedTime;
		return difference;
	}

	/// <summary>
	/// Determines if the skill is available to use, i.e has enough time lapsed
	/// </summary>
	/// <returns>Whether the bullet skill is ready to use</returns>
	public bool SkillAvailable()
	{
		if (Time.timeSinceLevelLoad - lastUsedTime > coolDown)
		{
			return true;
		}

		return false;
	}

	/// <summary>
	/// This method is invoked when the player has pressed the necessary key binding for activating the bullet skill.
	/// Determine how the player should activate the bullet skill
	/// </summary>
	protected void SkillActivated() {
		//An arrow has been fired, update achievement data
		SlideToSurviveAchievements.archerAchievement.ArrowFired ();
		lastUsedTime = Time.timeSinceLevelLoad;

		//If the player currently has the big bullet upgrade
		if (streakOne)
		{
			//Fire the big bullet prefab, relative to the player's position and rotation
			CmdFire(megaBulletPrefab, transform.position, transform.rotation);
			streakOne = false;
			//Deactiavte the big bullet indicator
			transform.GetChild (6).gameObject.SetActive(false);
		}
		//If the player currently has the triple shot upgrade
		else if (streakTwo)
		{
			CmdFireMultiple(bulletPrefab, transform.position, transform.rotation);
			streakTwo = false;
			//Deactivate the triple shot indicator
			transform.GetChild (7).gameObject.SetActive(false);
		}
		//Normal shot
		else
		{
			CmdFire(bulletPrefab, transform.position, transform.rotation);
		}
	}

	/// <summary>
	/// When a single bullet must be fired
	/// </summary>
	/// <param name="bulletType">Bullet type.</param>
	/// <param name="position">Position, relative to when player shot.</param>
	/// <param name="rotation">Rotation, relative to when player shot.</param>
	void CmdFire(GameObject bulletType, Vector3 position, Quaternion rotation)
	{
		//Change animator state to shooting
		player.GetComponent<Animator>().SetBool("isShooting", true);
		//Shoot the bullet after some time, in order to sync with the shooting animation
		StartCoroutine(ExecuteAfterTime(bulletType, 1.0f, position, rotation));

		lastUsedTime = Time.timeSinceLevelLoad;

	}

	/// <summary>
	/// Invoked when multiple bullets must be fired, i.e. the triple shot upgrade
	/// </summary>
	/// <param name="bulletType">Bullet type.</param>
	/// <param name="position">Position, relative to when player shot.</param>
	/// <param name="rotation">Rotation, relative to when player shot.</param>
	void CmdFireMultiple(GameObject bulletType, Vector3 position, Quaternion rotation)
	{
		//Change animator state to shooting
		player.GetComponent<Animator>().SetBool("isShooting", true);
		//Shoot the bullets after some time, in order to sync with the shooting animation
		StartCoroutine(ExecuteAfterTimeMultiple(bulletType, 1.0f, position, rotation));

		lastUsedTime = Time.timeSinceLevelLoad;

	}

	/// <summary>
	/// Logic for instantiating the triple shot skill bullets.
	/// </summary>
	/// <param name="bulletType">Bullet type.</param>
	/// <param name="time">Time before instantiating bullet.</param>
	/// <param name="position">Position, relative to when player shot.</param>
	/// <param name="rotation">Rotation, relative to when player shot.</param>
	IEnumerator ExecuteAfterTimeMultiple(GameObject bulletType, float time, Vector3 position, Quaternion rotation)
	{
		//Initial wait to sync bullet instantiation with animation
		yield return new WaitForSeconds(time);
		ShootBullet(bulletType);
		//Wait a few seconds before the next bullets fire
		yield return new WaitForSeconds(0.2F);
		ShootBullet(bulletType);
		yield return new WaitForSeconds(0.2F);
		ShootBullet(bulletType);
		//No longer shooting, set animation accordingly
		player.GetComponent<Animator>().SetBool("isShooting", false);
	}

	/// <summary>
	/// Logic for instantiating the singular bullet shots
	/// </summary>
	/// <returns>The after time.</returns>
	/// <param name="bulletType">Bullet type.</param>
	/// <param name="time">Time before instantiating bullet</param>
	/// <param name="position">Position, relative to when player shot.</param>
	/// <param name="rotation">Rotation, relative to when player shot.</param>
	IEnumerator ExecuteAfterTime(GameObject bulletType, float time, Vector3 position, Quaternion rotation)
	{
		//Initial wait to sync bullet instantiation with animation
		yield return new WaitForSeconds(time);

		//Get position and rotation of player
		position = player.transform.position;
		rotation = player.transform.rotation;

		//Instantiate bullet respective to the player's transform. One y axis up so bullet not too close to ground
		GameObject bullet = (GameObject)Instantiate(bulletType, position + Vector3.up, rotation);
		BulletController bc = bullet.GetComponent<BulletController>();

		//Set the bullet's player id 
		bc.SetPlayerId (GetPlayerId());

		//Direction of bullet, relative to where player is facing
		Vector3 bulletDirection = rotation * Vector3.forward;
		bc.SetDirection(bulletDirection);
		//No longer shooting
		player.GetComponent<Animator>().SetBool("isShooting", false);
	}

	/// <summary>
	/// Shoots the bullet i.e instantiate. Function used in conjunction for the triple shot function. Functionality is similar
	/// to the above method
	/// </summary>
	/// <param name="bulletType">Bullet type.</param>
	void ShootBullet (GameObject bulletType) {
		Vector3 position = player.transform.position;
		Quaternion rotation = player.transform.rotation;
		GameObject bullet = (GameObject)Instantiate(bulletType, position + Vector3.up, rotation);
		BulletController bc = bullet.GetComponent<BulletController>();
		bc.SetPlayerId (GetPlayerId());
		Vector3 bulletDirection = rotation * Vector3.forward;
		bc.SetDirection(bulletDirection);
	}

	/// <summary>
	/// Starts the big bullet upgrade for this player. activate the indicator to show the player that this upgrade is enabled
	/// </summary>
	public void StartStreakOne() {
		streakOne = true;
		transform.GetChild (6).gameObject.SetActive(true);
	}

	/// <summary>
	/// Starts the triple shot upgrade for this player. activate the indicator to show the player that this upgrade is enabled
	/// </summary>
	public void StartStreakTwo() {
		streakTwo = true;
		transform.GetChild (7).gameObject.SetActive(true);
	}

	/// <summary>
	/// Method to be overriden by player one/two implementations. 
	/// </summary>
	/// <returns>The player identifier.</returns>
	public virtual int GetPlayerId() {
		return 0;
	}
}
