using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// This class's responsibility is to provide all necessary functionality for the purchasable player skills. 
/// </summary>
public class PlayerItemsControllerMain : MonoBehaviour {
	//currency for this player
	protected int currencyCount = 0;

	//Text showing the current currency
	public Text coinCountText;

	//Cost of using freeze
	public int freezeCost = 3;

	//Freeze skill gameobject prefab
	public GameObject freezePrefab;

	//Cost of using barrier
	public int barrierCost = 3;

	//Prefabs of the barriers
	public GameObject barrierLevel1Prefab;
	public GameObject barrierLevel2Prefab;

	//Only allow one barrier at a time
	protected GameObject playerBarrier;

	//Only allow one freeze at a time
	protected GameObject playerFreeze;

	//Deduct cost of using ability
	void DeductAbilityCost(int cost) {
		currencyCount = currencyCount - cost;
		coinCountText.text = currencyCount + "";
	}

	//Invoked when player has activated the freeze skill
	protected void FreezeAbilityActivated (int playerId) {
		//Activate only if freeze is inactive or there are no freezes spawned by this player
		if (playerFreeze == null || !playerFreeze.activeSelf) {
			//Place freeze on top of player
			GameObject freezeObject = (GameObject)Instantiate (freezePrefab, transform.position, Quaternion.identity);
			playerFreeze = freezeObject;
			DeductAbilityCost (freezeCost);
		}
	}

	//Invoked when player has activated the barrier skill
	protected void BarrierAbilityActivated (int playerId) {
		//Activate only if barrier is inactive or there are no barriers spawned by this player
		if (playerBarrier == null || !playerBarrier.activeSelf) {
			//Place barrier on top of player
			if (BarrierSyncPossible (playerId)) {
				InstantiateBarrier (barrierLevel2Prefab);
			} 
			else {
				InstantiateBarrier (barrierLevel1Prefab);
			}
		}
	}

	//Instantiate the barrier skill, depending on the barrier prefab type
	void InstantiateBarrier(GameObject barrierType) {
		//Instantiate on the players position
		GameObject barrierObject = (GameObject) Instantiate (barrierType, transform.position, Quaternion.identity);
		//Link association between player and barrier
		playerBarrier = barrierObject;
		BarrierController bc = barrierObject.transform.GetComponent<BarrierController> ();
		bc.SetPlayer (this.gameObject);

		DeductAbilityCost (barrierCost);
	}

	//Determines whether the barrier is able to be synced
	bool BarrierSyncPossible(int playerId) {
		//Get the other player's game object
		string playerObjectName = "";

		if (playerId == 1) {
			playerObjectName = "PlayerTwo_Blue";
		}
		if (playerId == 2) {
			playerObjectName = "PlayerOne_Green";
		}

		//Get the other player's barrier
		GameObject playerGameObject = GameObject.Find (playerObjectName);
		GameObject playerBarrierGameObject = playerGameObject.transform.GetComponent<PlayerItemsControllerMain> ().GetPlayerBarrier();

		//If there is no other player's barrier, no sync is possible
		if (playerBarrierGameObject == null || !playerBarrierGameObject.activeSelf) {
			return false;
		} 
		//The other player has a barrier active
		else {
			//Check if barrier sync is possible, check if this player is in the other player's barrier zone, as well as the other player being in the same zone
			if (playerBarrierGameObject.GetComponent<Collider> ().bounds.Contains (transform.position) && playerGameObject.GetComponent<PlayerItemsControllerMain> ().ContainsPlayer ()) {
				playerBarrierGameObject.SetActive (false);
				return true;
			} else {
				return false;
			}
		}

	}

	void OnTriggerEnter (Collider other) {
		//Pick up the currency pickup
		if (other.gameObject.CompareTag ("Currency")) {
			currencyCount++;
			//Increment the relevant achievement data
			SlideToSurviveAchievements.archerAchievement.CoinCollected ();
			coinCountText.text = currencyCount + "";
			other.gameObject.SetActive (false);
		}

		if (other.gameObject.CompareTag ("Diamond")) {
			SlideToSurviveAchievements.archerAchievement.DiamondCollected ();
			other.gameObject.SetActive (false);
		}
	}

	public GameObject GetPlayerBarrier() {
		return playerBarrier;
	}

	public GameObject GetPlayerFreeze() {
		return playerFreeze;
	}

	//To determine whether this player is in the player barrier the player has instantiated; for barrier level2's sake
	public bool ContainsPlayer() {
		Collider collider = playerBarrier.GetComponent<Collider> ();
		if (collider.bounds.Contains (transform.position)) {
			return true;
		}
		return false;
	}

	public void SetCurrencyCount (int icurrency) {
		currencyCount = icurrency;
		coinCountText.text = currencyCount + "";
	}

	public int GetCurrencyCount () {
		return currencyCount;
	}
}
