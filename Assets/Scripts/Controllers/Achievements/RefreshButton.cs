using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[AddComponentMenu("AchievementCreator/Other/RefreshButton")]

public class RefreshButton : MonoBehaviour
{
	private AchievementWindow window;

	//Initialization on game start.
	void Awake()
	{
		window = GameObject.Find ("Achievements").GetComponent<AchievementWindow>();
	}

	//This function is called when we refresh the achievement window.
	public void UseRefreshButton()
	{
		window.RemoveAchievementClones();
		window.RefreshAchievementDisplay();
	}
}
