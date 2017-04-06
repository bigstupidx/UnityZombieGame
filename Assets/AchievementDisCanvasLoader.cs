using UnityEngine;
using System.Collections;

public class AchievementDisCanvasLoader : MonoBehaviour {

	public static AchievementDisCanvasLoader dLoad;


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
