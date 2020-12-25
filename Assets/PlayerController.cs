using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Animator anim;
    public static GameObject player;
    public static GameObject currentPlatform;
    bool canTurn = false;
    Vector3 startPosition;
    public static bool isDead = false;
    Rigidbody rb;

    public GameObject magic;
    public Transform magicStartPos;
    Rigidbody mRb;

    bool falling = false;
    
    // dealing with livescount
    int livesLeft;
    public Texture aliveIcon;
    public Texture deadIcon;
    public RawImage[] icons; //using UnityEngine.UI; // ye death icons images hain jo inspector mein dalry hum
    public GameObject gameOverPanel;
    public Text highScore; // to display while playing game 

    //ddealing with sound
    public static AudioSource[] sfx;


    

    void OnCollisionEnter(Collision other)
    {
        if ((falling || other.gameObject.tag == "Fire" || other.gameObject.tag == "Wall") && !isDead)
        {
            if (falling) 
                anim.SetTrigger("isFalling");
            else
                anim.SetTrigger("isDead");
            isDead = true;
            sfx[2].Play();
            livesLeft--; //after dying we decrease liveleft count
            PlayerPrefs.SetInt("lives", livesLeft); //we set the lives in dictionary 'lives' with value  of livesleft
            if (livesLeft > 0)
                Invoke("RestartGame", 2);
            else   
            {
                icons[0].texture = deadIcon;  //change icon immediately when death occurs
                gameOverPanel.SetActive(true);
                //saving last score
                PlayerPrefs.SetInt("lastscore", PlayerPrefs.GetInt("score"));

                //if player has a highscore value then get it then compare it with scrore 
                //if high score is less than score then set high score =value of score (line62)
                if (PlayerPrefs.HasKey("highscore"))                    
                {
                    int hs = PlayerPrefs.GetInt("highscore");
                    if (hs < PlayerPrefs.GetInt("score"))
                        PlayerPrefs.SetInt("highscore", PlayerPrefs.GetInt("score"));
                }
                else 
                { 
                    //if there is no high score then it means that we have to set it for the first time
                    PlayerPrefs.SetInt("highscore", PlayerPrefs.GetInt("score"));
                }

            }
             
        
        }
        else
            currentPlatform = other.gameObject;
    }

    void RestartGame()
    {
        SceneManager.LoadScene("Platforms", LoadSceneMode.Single);
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody>();
        mRb = magic.GetComponent<Rigidbody>();
        sfx = GameObject.FindWithTag("gamedata").GetComponentsInChildren<AudioSource>();

        player = this.gameObject;
        startPosition = player.transform.position;
        GenerateWorld.RunDummy();

        // if player has high score display it in high score tab while playing game else display zero
        if (PlayerPrefs.HasKey("highscore"))
            highScore.text = "High Score: " + PlayerPrefs.GetInt("highscore");
        else
            highScore.text = "High Score: 0";

        isDead = false; // when scene restarts we again set boolean to say that player is not dead
        livesLeft = PlayerPrefs.GetInt("lives");// Getting the lives here from playerprefs created in loadgamescene method in mainmenu script
        
        // we change the value of 'livesleft' from oncollision enter above where player dies
        //then when theestart is called again when new scene is loaded ,livesleft will have one less than it's value 
        
        
        
        
        
        for(int i = 0; i < icons.Length; i++)
        {
            if (i >= livesLeft)
                icons[i].texture = deadIcon; //setting the icons of death if livesleft is less than original we set in menu
        }
    }

    void CastMagic()
    {
        magic.transform.position = magicStartPos.position;
        magic.SetActive(true);
        mRb.AddForce(this.transform.forward * 1000);
        Invoke("KillMagic", 1);
        sfx[1].Play();
    }

    void KillMagic()
    {
        magic.SetActive(false);
    }

     void Footstep1()
    {
        sfx[4].Play();
    }

    void Footstep2()
    {
        sfx[5].Play();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other is BoxCollider && GenerateWorld.lastPlatform.tag != "platformTSection")
            GenerateWorld.RunDummy();

        if (other is SphereCollider)
            canTurn = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other is SphereCollider)
            canTurn = false;
    }

    void StopJump()
    {
        anim.SetBool("isJumping", false);
    }

    void StopMagic()
    {
        anim.SetBool("isMagic", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.isDead) return;

        //causing death if player falls down 5 units  below the current player
        if (currentPlatform != null)
        {
            if(this.transform.position.y< currentPlatform.transform.position.y - 5)
            {
                falling = true;
                OnCollisionEnter(null); 
            }
        }




        if (Input.GetKeyDown(KeyCode.Space) && anim.GetBool("isMagic") == false)
        {
            anim.SetBool("isJumping", true);
            rb.AddForce(Vector3.up * 200);
            sfx[6].Play();

        }
        else if (Input.GetKeyDown(KeyCode.M) && anim.GetBool("isJumping") == false)
        {
            anim.SetBool("isMagic", true);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && canTurn)
        {
            this.transform.Rotate(Vector3.up * 90);
            GenerateWorld.dummyTraveller.transform.forward = -this.transform.forward;
            GenerateWorld.RunDummy();

            if (GenerateWorld.lastPlatform.tag != "platformTSection")
                GenerateWorld.RunDummy();

            this.transform.position = new Vector3(startPosition.x,
                                                this.transform.position.y,
                                                startPosition.z);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && canTurn)
        {
            this.transform.Rotate(Vector3.up * -90);
            GenerateWorld.dummyTraveller.transform.forward = -this.transform.forward;
            GenerateWorld.RunDummy();

            if (GenerateWorld.lastPlatform.tag != "platformTSection")
                GenerateWorld.RunDummy();

            this.transform.position = new Vector3(startPosition.x,
                                    this.transform.position.y,
                                    startPosition.z);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            this.transform.Translate(-0.5f, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            this.transform.Translate(0.5f, 0, 0);
        }
    }
}

