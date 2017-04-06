using UnityEngine;
using System.Collections;

public class LeaderboardManager : MonoBehaviour {
    public GameObject leaderboard;
	// Use this for initialization
	void Start () {
	    if (PlayerPrefs.GetFloat("gametime") == 0)
        {
            leaderboard.SetActive(true);
        }
	}
}
