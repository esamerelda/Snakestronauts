using UnityEngine;
using UnityEngine.UI; //text, images, sliders
using System.Collections;

public class PlayerSliders : MonoBehaviour {

    //Starting Item effects
    private int disconautHP = -5;
    private int fireHP = -15;
    private int snakeHP = -5;
    private int snakeVenom = 20;
    private int teaHP = 20;
    private int teaVenom = -5;
    

	//Venom & Timer
    [HideInInspector]
	public int startVenom = 50;
	public static int currentVenom;
	public int maxVenom = 100; 									//TODO slider maxValue set to same, should synchronize through code.
	public Slider venomSlider;								//slider bar on right, shows venOmeter & HP
	float venomFadeTimer;										//timer variable to count down to bac drop
	float venomTimerMax = .4f;								//how much time for bac to drop
	int sobriety = 1;										//how much bac drops when soberTimer reaches 0

	//HP
	int startTolerance = 50;
	public static int currentHP;
	float tolTimer;
	float tolTimerMax = 3.5f;
	public Slider hpSlider;

    //Spinning BG
    private GameObject spinningBG;
    private RotateBackground spinBgScript;

	//Blacking Out
	public Image blackoutImageGreen;  							//black out randomly when highly venomous
    public Image blackoutImageRed;
	float blackoutTimerGreen;
    float blackoutTimerRed;  //will set to random number and count down while player is really drunk
	float blackoutFrequencyMin = .01f;
	float blackoutFrequencyMax = 2f;								// TODO - give random value to randomize blackouts
	//float blackoutFlashSpeed = 1.8f;  						//the speed blackoutImageGreen will fade at
	float blackoutFlashSpeed;
	float blackoutFlashSpeedMin = .01f;
	float blackoutFlashSpeedMax = 2.5f;
								
	int blackoutLimit = 75;									// minimum bac necessary to induce blackouts
	bool blackedOut;										//if player is currently blacking out
    //private Color blackoutFlashColor = new Color(0, 0, 0, 255);  //Black
    private Color blackoutFlashColor = new Color(0, 255, 0, 1f);  //Green
    private Color redoutFlashColor = new Color(255, 0, 0, 1f); //Red

    //belching
    //public AudioClip belchSound;
    //private AudioSource source;

    //Dying
    bool isDead;  										//whether player is dead
	public static bool deadWithdrawal;						//tells WalkinDudeController if player died of withdrawal
	public static bool deadBloodPoisoning;					//tells WalkinDudeController if player drank too much


	//an error pops up saying this is never used, but don't listen to it.
	CletusController playerScript;  					//reference to player's movement script.

	

	void Awake()
	{
        //set up references

        playerScript = GetComponent <CletusController> ();
		//source = GetComponent<AudioSource>();  				//reference to walkinDude's audioSource Component

		//set initial stats
		//bool isDead;
		//deadWithdrawal = false;
		//deadBloodPoisoning = false;
		
		//currentVenom = startVenom;
		//currentHP = startTolerance;

		//set timers
		//venomFadeTimer = venomTimerMax;
		//tolTimer = tolTimerMax;
		//blackoutTimer = blackoutFrequencyMax / 2;

        spinBgScript = GameObject.Find("BG").GetComponent<RotateBackground>();

	}

    void Start()
    {
        isDead = false;
        venomFadeTimer = venomTimerMax;
        //deadWithdrawal = false;
        //deadBloodPoisoning = false;
  

        currentVenom = startVenom;
        currentHP = startTolerance;
        
        tolTimer = tolTimerMax;
        blackoutTimerGreen = blackoutFrequencyMax / 2;
        blackoutTimerRed = blackoutFrequencyMax / 2;
    }


	
	// Update is called once per frame
	void Update () 
	{

       venomFadeTimer -= Time.deltaTime;
	    if (venomFadeTimer <= 0)
		{
            venomTimerUpdate();
		}

		BlackoutGreen();
        BlackoutRed();
		

        //send currentVenom to spinning BG to alter its speed of rotation
        spinBgScript.SetRotateSpeed(currentVenom);

	}

    //RUNNING INTO SHIT
	public void SnakeBite ()  //TakeDamage in tutorial
	{
		//currentVenom += snakeVenom;
        //currentHP -= snakeHP;
        AdjustHP(snakeHP);
        AdjustVenom(snakeVenom);
	}
    public void DrinkTea()
    {
        AdjustHP(teaHP);
        AdjustVenom(teaVenom);
    }
    public void HitFire()
    {
        AdjustHP(fireHP);
    }
    public void HitDisconaut()
    {
        AdjustHP(disconautHP);
    }

	//When soberTimer reaches 0
	private void venomTimerUpdate()
	{
		//reduce currentVenom
		currentVenom -= sobriety;
		AdjustVenom(0);

        //reset timer
        venomFadeTimer = venomTimerMax;
	}

	private void AdjustHP(int change)  //update tolerance timer
	{
        currentHP += change;            //change currentHP variable
		hpSlider.value = currentHP;     //update slider
		//TODO - trigger death if too little HP
        if (currentHP <= 0)
        {
            playerScript.DeadNoHP();
        }
        if (currentHP >= blackoutLimit)
        {
            BlackoutRed();
        }
        if (currentHP >= 100)
        {
            playerScript.DeadHP();
        }
		tolTimer = tolTimerMax;         //reset timer
    }


	private void AdjustVenom(int venom) {
        currentVenom += venom;
		//adjust slider accordingly
		venomSlider.value = currentVenom;

		//if Venom reaches 0, player dies of withdrawal
		if((currentVenom <= 0) && !isDead)
		{
            playerScript.DeadNoVenom();
		}
		else if (currentVenom >= maxVenom)
		{
            
            playerScript.DeadVenom();
		}

		//if player is really drunk, blackouts happen 
		if (currentVenom >= blackoutLimit)
		{
			BlackoutGreen();
		}
	}


	//When really drunk, blackouts can occur
	void BlackoutGreen()
	{
		
		if (currentVenom >= blackoutLimit)
		{
			blackoutTimerGreen -= Time.deltaTime;
			if (blackoutTimerGreen <= 0)
			{
					//TODO fade to black
				//set color of blackoutImageGreen to flash color 
				blackoutImageGreen.color = blackoutFlashColor;
				//reset blackout timer  
				//blackoutTimer = blackoutFrequencyMax;
				blackoutFlashSpeed = Random.Range(blackoutFlashSpeedMin, blackoutFlashSpeedMax);
				blackoutTimerGreen = Random.Range(blackoutFrequencyMin, blackoutFrequencyMax);
			}
			else
			{
				blackoutImageGreen.color = Color.Lerp (blackoutImageGreen.color, Color.clear, blackoutFlashSpeed * Time.deltaTime);
			}
		}
		else
		{
			//transition color back to clear
			blackoutImageGreen.color = Color.Lerp (blackoutImageGreen.color, Color.clear, blackoutFlashSpeed * Time.deltaTime);
		}
	}
    void BlackoutRed()
    {

        if (currentHP >= blackoutLimit)
        {
            blackoutTimerRed -= Time.deltaTime;
            if (blackoutTimerRed <= 0)
            {
                //TODO fade to black
                //set color of blackoutImageRed to flash color 
                blackoutImageRed.color = redoutFlashColor;
                //reset blackout timer  
                //blackoutTimer = blackoutFrequencyMax;
                blackoutFlashSpeed = Random.Range(blackoutFlashSpeedMin, blackoutFlashSpeedMax);
                blackoutTimerRed = Random.Range(blackoutFrequencyMin, blackoutFrequencyMax);
            }
            else
            {
                blackoutImageRed.color = Color.Lerp(blackoutImageRed.color, Color.clear, blackoutFlashSpeed * Time.deltaTime);
            }
        }
        else
        {
            //transition color back to clear
            blackoutImageRed.color = Color.Lerp(blackoutImageRed.color, Color.clear, blackoutFlashSpeed * Time.deltaTime);
        }
    }
}
