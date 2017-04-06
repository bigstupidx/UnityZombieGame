using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AchievementClone : MonoBehaviour
{
	public Text nameText;
	public Text descriptionText;
	[Space(4)]
	public Image achievementIcon;
	public Image completeIcon;

	[HideInInspector]
	public AchievementContainer playerAchievements;

	private Sprite icon;

	void Start()
	{
		//Refreshing every information of this achievement clone.
		nameText.text = playerAchievements.GetTitle();
		descriptionText.text = playerAchievements.GetMessage();
		icon = playerAchievements.GetIcon();
		transform.gameObject.tag = "AchievementClone";



		//if(myAchievement.showProgress && !myAchievement.isCompleted)
	//	{
		//	nameText.text = nameText.text + " [" + myAchievement.currentValue + "/" + myAchievement.requiredValue + "]";
		//}

		//Hides the complete icon.
		if(!playerAchievements.IsUnlocked())
		{
			completeIcon.color = new Color(completeIcon.color.r, completeIcon.color.g, completeIcon.color.b, 0f);

		}

		//Shows the complete icon. 
		if(playerAchievements.IsUnlocked())
		{
			completeIcon.color = new Color(completeIcon.color.r, completeIcon.color.g, completeIcon.color.b, 1f);
			nameText.color = new Color (0f, 1f, 0f, 1f);
			achievementIcon.sprite = icon;
		}
	}
}
