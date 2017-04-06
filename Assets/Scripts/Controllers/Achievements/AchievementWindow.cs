using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class AchievementWindow : MonoBehaviour
{
	public CanvasGroup windowUI;
	public KeyCode openKey;
	[Space(4)]
	public Transform achievementClonePrefab;
	public LayoutGroup achievementHolder;

	private bool isShowing;

	//Initialization on game start.
	void Awake()
	{

		windowUI.alpha = 0;
		RemoveAchievementClones();
	}

	//Running update every frame.
	void Update()
	{
		//Opening and closing the window via a key.
		if(Input.GetKeyDown(openKey))
		{
			isShowing = !isShowing;

			//Shows the window and instantiates the achievement clones.
			if(isShowing)
			{
				windowUI.alpha = 1;
				RefreshAchievementDisplay();

			}

			//Hides the window and destroys all the achievement clones.
			else if(!isShowing)
			{
				windowUI.alpha = 0;
				RemoveAchievementClones();

			}
		}
	}

    public void ActivateDisplay() {
		isShowing = !isShowing;

		//Shows the window and instantiates the achievement clones.
		if(isShowing)
		{
			windowUI.alpha = 1;
			RefreshAchievementDisplay();

		}

		//Hides the window and destroys all the achievement clones.
		else if(!isShowing)
		{
			windowUI.alpha = 0;
			RemoveAchievementClones();

		}
    }

	//This function is called when we want to instantiate the achievement clones.
	public void RefreshAchievementDisplay()
	{
		List<AchievementContainer> achievementList = SlideToSurviveAchievements.archerAchievement.GetAchievements ();

		foreach(AchievementContainer ac in achievementList)
		{
			Transform achievementClone = Instantiate(achievementClonePrefab, achievementHolder.transform.position, achievementHolder.transform.rotation) as Transform;
			achievementClone.SetParent(achievementHolder.transform, false);

			achievementClone.GetComponent<AchievementClone> ().playerAchievements = ac;
		}
	}

	//This function is called when we want to destroy the achievement clones.
	public void RemoveAchievementClones()
	{
		GameObject[] achievementClones = GameObject.FindGameObjectsWithTag("AchievementClone");

		if(achievementClones != null)
		{
			for(int a = 0; a < achievementClones.Length; a++)
			{
				Destroy(achievementClones[a].gameObject);
			}
		}
	}
}