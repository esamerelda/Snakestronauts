using UnityEngine;
using UnityEngine.UI;
using System.Collections;
//using System;

public class CletusController : MonoBehaviour {

	// Use this for initialization	

	//walking
	public float moveSpeed;  		//may need to turn private
	public float moveSpeedNormal = 50f;
    private bool isMoving;

	//jumping
	bool jump = false; 					// Condition for whether the player should jump
	//float jumpForce;  					//Amount of force added when the player jumps
	//float jumpForceNormal = 2250f;
	//private bool isOnGround = false;  	// Whether or not player is grounded
	private Transform groundCheck;  	//position marking where to check if the player is grounded

	//THROWING
	public int canCount = 0;  				//start game with 0 cans
	[HideInInspector]
	public bool facingRight = true;			//determine which direction player faces to throw can in that direction
	[HideInInspector]
	public float canSpeed;
	float canSpeedNormal = 60f;

    //HP
	int tolerance;
	int toleranceMargin = 25;			//how far below or above 50 the venom must be before alternate stats kick in
	int bac;

    //DYING
    [HideInInspector]
    public bool isDead;
	[HideInInspector]
	bool deadHP, deadNoHP, deadVenom, deadNoVenom;		//methods of death
    private string deathText;
	

	//death Box
	int guiBoxWidth;
	int guiBoxHeight;
	int guiBoxX;
	int guiBoxY;
	//death Button
	int guiButtonWidth;
	int guiButtonHeight;
	int guiButtonX;
	int guiButtonY; 

	//winning
	bool wonGame = false;
    private float currentScore;
    public GameObject currentScoreDisplay;
    private float highScore;
    public GameObject highScoreDisplay;


    //scripts
    GameObject player;  				//the player
	private PlayerSliders slidersScript;				//reference to player's bac script
    private HighScores scoreScript;
    

	public GameObject snakeCountDisplay;

	private AudioSource source;
    private ExternalAudio extAudio;


	void Awake()
	{
		// Setting up references...?
		transform.position = new Vector3(0,-16,0);   //puts character in starting position
		player = GameObject.FindGameObjectWithTag("Player");
		slidersScript = player.GetComponent<PlayerSliders> ();
        scoreScript = GameObject.Find("HighScoresObj").GetComponent<HighScores>();

        extAudio = GameObject.Find("ExternalAudioObj").GetComponent<ExternalAudio>();

		//groundCheck = transform.Find ("groundCheck");

		source = GetComponent<AudioSource>();  //reference to WalkinDude's audioSource component

		//guiDeathBoxVariables
		guiBoxWidth = 300;
		guiBoxHeight = 50;
		guiBoxX = (Screen.width / 2) - (guiBoxWidth / 2);  
		guiBoxY = (Screen.height / 2) - (guiBoxHeight / 2);
		guiButtonWidth = 40;
		guiButtonHeight = 20;
		guiButtonX = (Screen.width / 2) - (guiButtonWidth / 2);
		guiButtonY = guiBoxY + guiBoxHeight - guiButtonHeight;

		//set variables to starting
		moveSpeed = moveSpeedNormal;
		//jumpForce = jumpForceNormal;
		canSpeed = canSpeedNormal;

		//update can UI text, starts at 0
		UpdateSnakeCount();
	}

    void Start()
    {
        isMoving = false;
        isDead = false;
        StartTime();
        currentScore = 0;
        HighScoreDisplay();
    }


	
	// Update is called once per frame
	void Update ()
	{

		if (isDead == false)  		//TODO - Stop Camera if player dies
		{
            currentScore += Time.deltaTime;
            //Debug.Log(currentScore);
			UpdateSnakeCount();		//updates cancount on bottom left of screen

			CheckBacVariables();

            transform.Translate(Vector3.right * Time.deltaTime * Input.GetAxis("Horizontal") * moveSpeed);    //moves player < and >.  replace with "Vertical" for up and down 
            transform.Translate(Vector3.up * Time.deltaTime * Input.GetAxis("Vertical") * moveSpeed);
            if ((Input.GetKey("up")) || (Input.GetKey("right")) || (Input.GetKey("down")) || (Input.GetKey("left")))
            {
                isMoving = true;
                extAudio.PlayEngineSound(isMoving);
                
            }
            //if ((Input.GetKeyUp("up")) || (Input.GetKeyUp("right")) || (Input.GetKeyUp("down")) || (Input.GetKeyUp("left")))
            else
            {
                isMoving = false;
                extAudio.PlayEngineSound(isMoving);
            }


            //JUMPING
            // The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
            /*isOnGround = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"));
			if ((Input.GetKeyDown (KeyCode.UpArrow)) && (isOnGround))	//jump if player is on the ground and pressing ^
			{
				//jump = true;
			}*/

            //Check direction so can knows which way to face
            if (Input.GetKeyDown(KeyCode.RightArrow))
			{
				facingRight = true;
                transform.localScale = new Vector3(-7f, 7f, 1f);
            }
			if (Input.GetKeyDown(KeyCode.LeftArrow))
			{
				facingRight = false;
                transform.localScale = new Vector3(7f, 7f, 1f);
			}

            UpdateOnScreenTimer();


		}
	}

	/*void FixedUpdate()
	{
		if (jump) 
		{
				GetComponent<Rigidbody2D>().AddForce (new Vector2 (0f, jumpForce));		//Add a vertical force to the player
				jump = false;		//Make sure player can't jump again until the jump conditions from UPdate are satisfied
		}
	}*/


	void OnTriggerEnter2D(Collider2D gameObj) //collision detection
	{
        if (gameObj.gameObject.tag == "fire")
        {
            extAudio.PlayFire();
            Destroy(gameObj.gameObject);
            slidersScript.HitFire();
        }

        if (gameObj.gameObject.tag == "snake")
        {
            extAudio.PlaySnakeSlither();
            canCount += 1;					//add a crushed can to the inventory
			Destroy(gameObj.gameObject); 	//destroys snake on screen
			slidersScript.SnakeBite();		//function in PlayerSliders to adjust player's BAC
		}
        if (gameObj.gameObject.tag == "spawner")
        {
            extAudio.PlayClunk();
            slidersScript.HitDisconaut();
            //Destroy(gameObj.gameObject);
        }

        if (gameObj.gameObject.tag == "tea")
        {
            extAudio.PlayTeaSlurp();
            Destroy(gameObj.gameObject);
            slidersScript.DrinkTea();
        }
    }

	void OnGUI()
	{
        if (isDead)
        {
            StopTime();
            scoreScript.AddNewScore(currentScore);
            GUI.Box(new Rect(guiBoxX, guiBoxY, guiBoxWidth, guiBoxHeight), deathText);
            if (GUI.Button(new Rect(guiButtonX - (guiButtonWidth / 2), guiButtonY, guiButtonWidth, guiButtonHeight), "OK"))
            {
                Death();
            }
            if (GUI.Button(new Rect(guiButtonX + (guiButtonWidth / 2), guiButtonY, guiButtonWidth, guiButtonHeight), "Quit"))
            {
                Application.Quit();
            }
        }
	}

	void UpdateSnakeCount()
	{
		Text canDisplayText = snakeCountDisplay.GetComponent<Text>();
		canDisplayText.text = canCount.ToString();
	}

    void UpdateOnScreenTimer()
    {
        Text timeDisplayText = currentScoreDisplay.GetComponent<Text>();
        timeDisplayText.text = currentScore.ToString();
    }

    void HighScoreDisplay()
    {
        Text highScoreDisplayText = highScoreDisplay.GetComponent<Text>();
        float score = scoreScript.GetHighScore();
        highScoreDisplayText.text = "High Score: " + scoreScript.GetHighScore().ToString();
    }

    public void DeadHP()
    {
        deathText = "You have drowned in space tea.";
        isDead = true;
    }
    public void DeadNoHP()
    {
        deathText = "You should drink more space tea.";
        isDead = true;
    }
    public void DeadNoVenom()
    {
        deathText = "You need some venom to survive...";
        isDead = true;
    }
    public void DeadVenom()
    {
        deathText = "You got bit by too many snakes and died.";
        isDead = true;
    }

    //DESTROY THIS WHOLE THING
	void CheckBacVariables()
	{
		
		if (PlayerSliders.deadBloodPoisoning == true)
		{
			//deadBloodPoisoning = true;
			//isDead = true;
		}
		//if it is, set variables to true and negate movements

		tolerance = PlayerSliders.currentHP;
		bac = PlayerSliders.currentVenom;
		if (bac >= 75)//(bac > (tolerance + toleranceMargin)) //if above tolerance / is schwasted, wider range of random stats
		{
			moveSpeed = Random.Range(0f, moveSpeedNormal * 2f);
			//jumpForce = Random.Range(0f, jumpForceNormal * 2f);
			//canSpeed = Random.Range(0f, canSpeedNormal * 2f);
			//TODO - CHANGE BAC SLIDER COLOR
		}
		else if ((bac < 75) && (bac > 25))//((bac <= (tolerance + toleranceMargin)) && (bac >= (tolerance - toleranceMargin))) //if within tolerance, stats normal
		{
			moveSpeed = moveSpeedNormal;
			//jumpForce = jumpForceNormal;
			//canSpeed = canSpeedNormal;
		}
		else if (bac < 20)//(bac < (tolerance - toleranceMargin)) //if below tolerance range / having withdrawals, lower randomized stats.
		{
			moveSpeed = Random.Range(moveSpeedNormal / 4, moveSpeedNormal);
			//jumpForce = Random.Range(jumpForceNormal / 2f, jumpForceNormal);
			//canSpeed = Random.Range(canSpeedNormal / 2f, canSpeedNormal);
			//TODO - CHANGE BAC SLIDER COLOR
		}

	}

	//death function
	void Death()
	{
		//TODO make something cooler happen when you die.  Just reset for now.
		Application.LoadLevel(Application.loadedLevel);		//reloads this same level
	}
	void Win()
	{
		Application.LoadLevel(Application.loadedLevel);	
	}

    void StartTime()
    {
        Time.timeScale = 1;
    }
    void StopTime()
    {
        Time.timeScale = 0;
    }
}