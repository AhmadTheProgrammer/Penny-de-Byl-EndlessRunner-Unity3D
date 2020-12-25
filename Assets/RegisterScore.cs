using UnityEngine.UI;
using UnityEngine;

public class RegisterScore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameData.singleton.scoreText = this.GetComponent<Text>(); //that's how the text component score goes from scene platforms to main.
        GameData.singleton.UpdateScore(0); //just displays the score in scoretext in scene without adding anything
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
