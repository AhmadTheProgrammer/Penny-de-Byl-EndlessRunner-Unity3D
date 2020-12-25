using UnityEngine;


public class PickUp : MonoBehaviour
{
    // we are turning off the mesh renderers on the coin prefabs infact children's mesh renderer's also turning off.
    MeshRenderer[] mrs;

    public GameObject scorePrefab; //  floating text prefab
    GameObject canvas;// we need a canvas to see the floating text above the coin
    public GameObject particlePrefab; // for  instantiating the particle system of coin pickup 
    private void Start()
    {
        mrs = this.GetComponentsInChildren<MeshRenderer>();
        canvas = GameObject.Find("Canvas");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            GameData.singleton.UpdateScore(10);
            /// produce sound

            PlayerController.sfx[7].Play();
           
           GameObject scoreText=Instantiate(scorePrefab);//floating text over coin prefab initialization
            scoreText.transform.parent = canvas.transform;// putting floating text inside a canvas

            GameObject pE = Instantiate(particlePrefab, this.transform.position, Quaternion.identity);
            Destroy(pE, 1);

            //repositioning the score text to get released from the position of the coin
            //we are taking the position of the coin and projecting it into teh camera and then converting it
            //into screen coordinate because the world and the screen have different coordinate systems
            //this is how you transfer a 3d point in space into where it is banged to on the screen
            Vector3 screenPoint = Camera.main.WorldToScreenPoint(this.transform.position);
            scoreText.transform.position = screenPoint;
         // disappearing the coins when player has picked them up by turning off the mesh renderers in their childeren 
            foreach (MeshRenderer m in mrs) 
            {
                m.enabled = false;
            }
        }
    }
    //when this game object pickup coin comes again we turn its mesh renderers ON.
    private void OnEnable()
    {
        if( mrs!=null)// but we also check that if it's the very 1st time that the platform with coins has come up? then the array would be empty
        foreach (MeshRenderer m in mrs)
        {
            m.enabled = true;
        }
    }







}

