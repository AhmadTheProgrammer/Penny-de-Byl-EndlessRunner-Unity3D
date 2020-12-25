using UnityEngine.UI;
using UnityEngine;

public class DisplayStats : MonoBehaviour
{
    public Text lastScore;
    public Text highestScore;

    private void OnEnable()
    {
        //for last score
        if (PlayerPrefs.HasKey("lastscore"))
        {
            lastScore.text = "Last Score " + PlayerPrefs.GetInt("lastscore");
        }
        else
        {
            lastScore.text = "Last Score: 0"; 
        }
        //for highest score\

        if (PlayerPrefs.HasKey("highscore"))
        {
            highestScore.text = "Highest  Score " + PlayerPrefs.GetInt("highscore");
        }
        else
        {
            highestScore.text = "Highest Score: 0";
        }
    }
}
