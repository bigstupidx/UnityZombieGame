using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class AchievementDisplay : MonoBehaviour
{
	public CanvasGroup displayUI;
	public Text displayTitle;
	public Text displayMessage;

	[Range(0, 15)] public int displayTime;
	private bool showAchievementUI;

	//Initialization on game start.
	void Awake()
	{
		displayTitle = displayUI.transform.GetChild (1).GetComponent<Text> ();
		displayMessage = displayUI.transform.GetChild (2).GetComponent<Text> ();
		displayUI.alpha = 0;
	}

	//Running update every frame.
	void Update()
	{
		//Shows the Popup UI of the achievement.
		if(showAchievementUI)
		{
			displayUI.alpha = Mathf.Lerp(displayUI.alpha, 1, Time.fixedDeltaTime * 10);
		}
		
		//Fades the Popup UI of the achievement.
		else if(!showAchievementUI)
		{
			displayUI.alpha = Mathf.Lerp(displayUI.alpha, 0, Time.fixedDeltaTime * 10);
		}
	}

	//This function is called when an achievement is completed.
	public void DisplayAchievement(string title, string message, bool levelAchievement)
	{
		displayTitle.text = title;
		displayMessage.text = message;

		if (!levelAchievement) {
			StartCoroutine("ShowAchievementUI");
		}
	}

	//This function controlls the Popup UI show states.
	public IEnumerator ShowAchievementUI()
	{
		showAchievementUI = true;
		yield return new WaitForSeconds(displayTime);
		showAchievementUI = false;
	}
}
