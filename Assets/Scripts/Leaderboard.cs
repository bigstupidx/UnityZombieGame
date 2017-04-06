using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//Handles storing, displaying and updating the leaderboard
public class Leaderboard : MonoBehaviour {

    //the team name the user inputs
    public InputField teamName;
    private Text[] teamNames;

    //Upon the leaderboard canvas being enabled it will update, store and display the leaderboard
    public void OnEnable()
    {
     
        float newScore;
        string newName;
        float oldScore;
        string oldName;

        //the time taken and team name
        newScore = PlayerPrefs.GetFloat("gametime");
        newName = teamName.text;

        teamNames = GetComponentsInChildren<Text>();

        //checks that a game was played
        if (newScore != 0)
        {
            //iterates over the leaderboard to see if the recent score is better than any on the leaderboard
            for (int i = 0; i < 10; i++)
            {
                //if the is already a score for that position is then has to compare them
                if (PlayerPrefs.HasKey(i + "HScore"))
                {
                    //checks if the new time was faster
                    if (PlayerPrefs.GetFloat(i + "HScore") > newScore)
                    {
                        //store the previous score in oldscore so that it can be moved down the leaderboard
                        oldScore = PlayerPrefs.GetFloat(i + "HScore");
                        oldName = PlayerPrefs.GetString(i + "HScoreName");
                        //stores the new score
                        PlayerPrefs.SetFloat(i + "HScore", newScore);
                        PlayerPrefs.SetString(i + "HScoreName", newName);
                        //sets newscore to be the previous score so that on the next iteration be able to take the next position down
                        newScore = oldScore;
                        newName = oldName;
                    }
                }
                else
                {
                    //if newscore equals 0 it means the leaderboard doesnt have anymore scores bellow
                    if (newScore != 0)
                    {
                        //stores a new score
                        PlayerPrefs.SetFloat(i + "HScore", newScore);
                        PlayerPrefs.SetString(i + "HScoreName", newName);
                        //sets newScore to 0 because there is nothing bellow this
                        newScore = 0;
                        newName = "-";
                    }
                }


            }
        }
        //iterates over the leaderboard and updates the display
        for (int i = 0; i < 10; i++)
        {
            if (PlayerPrefs.HasKey(i + "HScore"))
            {
                teamNames[i * 2 + 1].text = PlayerPrefs.GetString(i + "HScoreName");
                teamNames[i * 2 + 2].text = PlayerPrefs.GetFloat(i + "HScore").ToString("n2");
            }
        }
    }
}

