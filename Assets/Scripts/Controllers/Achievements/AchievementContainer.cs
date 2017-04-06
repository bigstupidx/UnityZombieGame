using UnityEngine;
using System.Collections;

public class AchievementContainer : MonoBehaviour {

	private string title;
	private string message;
	private bool unlocked;
	private Sprite icon;

	public AchievementContainer (string ititle, string imessage, bool iunlocked, Sprite iicon) {
		title = ititle;
		message = imessage;
		unlocked = iunlocked;
		icon = iicon;
	}

	public bool IsUnlocked () {
		return unlocked;
	}

	public string GetTitle() {
		return title;
	}

	public string GetMessage() {
		return message;
	}
	public Sprite GetIcon() {
		return icon;
	}
}
