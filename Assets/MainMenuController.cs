using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{

    GameObject[] panels;
    GameObject[] mmbutton;

    int maxLives = 3; //decide how many lives player will have

    


    public void LoadGameScene()
    {
        PlayerPrefs.SetInt("lives", maxLives); // we set lives here in playerprefs when game starts from menu scene
        SceneManager.LoadScene("Platforms",LoadSceneMode.Single);
    }


     void Start()
    {
        panels = GameObject.FindGameObjectsWithTag("subpanel");
        mmbutton = GameObject.FindGameObjectsWithTag("mmbutton");

        foreach(GameObject p in panels)
        {
            p.SetActive(false); 
        }
       
        
    }

    public void ClosePanel(Button button) 
    {
        button.gameObject.transform.parent.gameObject.SetActive(false); //deactivating the panel here.

        foreach (GameObject b in mmbutton)
        {
            b.SetActive(true) ;
        }


    }

    public void OpenPanel(Button button)   
    {
        button.gameObject.transform.GetChild(1).gameObject.SetActive(true);// button kay child yani panel ko set active kr do 
        foreach (GameObject b in mmbutton)
        {
            if(b!=button.gameObject)// turn off all buttons except the one which caused this panel to open
            b.SetActive(false);
        }
    }







    // for fully quiting game
    public void QuitGame() {
        Application.Quit();
    }

    // 
    private void Update()
    {
        if (Input.GetKey("escape"))
        {
            QuitGame();
        }   
    }
}
