using UnityEngine;
using UnityEngine.UI; 
public class GameData : MonoBehaviour
{
    public static GameData singleton;// to access game data from using singleteon 
    public Text scoreText = null; // we set it's value in registerscore script attached to score elemnet in platforms tab;
    public int score = 0; //for holding our score

    public GameObject musicSlider;
    public GameObject soundSlider;
     

    private void Awake()
    {
        //this gamedata singleton will be created each time main menu scene is loaded. 
        // in order to prevent that we check if it's already there present or not.
        GameObject[] gd = GameObject.FindGameObjectsWithTag("gamedata");

        if (gd.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject); // this means dont destroy  this object if scene changes 
        singleton = this;
        //calling start of music Update code and updatesound script
        musicSlider.GetComponent<MusicUpdate>().Start();
        soundSlider.GetComponent<UpdateSound>().Start();
        PlayerPrefs.SetInt("score", 0); //setting the score zero when "gameData object" comes into being


    }
 // we call updatescore() from regtisterscore
    public void UpdateScore(int s)
    {
        score += s;
        PlayerPrefs.SetInt("score", score);// storing earned score in dictionary

        if (scoreText != null)
            scoreText.text = "Score: " + score; //showing score from dictionary if it isn't null
    }


}
