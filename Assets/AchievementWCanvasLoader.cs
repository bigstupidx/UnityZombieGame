using UnityEngine;
using System.Collections;

public class AchievementWCanvasLoader : MonoBehaviour {

	public static AchievementWCanvasLoader dLoad;


	void Awake () {

		if (dLoad == null) {
			DontDestroyOnLoad (gameObject);
			dLoad = this;
		} 

		else if (dLoad != this) {
			Destroy (gameObject);
		}
	}
}
