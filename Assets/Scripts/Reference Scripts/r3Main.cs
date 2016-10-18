using UnityEngine;
using System.Collections;

//ROOM 3 - Random Arrow Room
public class r3Main : MonoBehaviour {

	//public ArrayList directions = new ArrayList();
	public float timer = 0.0f;
	float timerMax = 1.25f;

	public float playerX;
	public float playerY;

	public int speed = 8;
	public int fallSpeed = 12;

	public int upNum;
	public int downNum;
	public int leftNum;
	public int rightNum;

	public bool moveUp;
	public bool moveDown;
	public bool moveLeft;
	public bool moveRight;

	public bool isTooHigh;

	public bool isDying;
	public bool isDead;

	// Use this for initialization
	void Start () 
	{
		isDying = false;
		isDead = false;
		timer = timerMax;


		//begin with normal key function... for the first 3 seconds.
		upNum = 1;
		downNum = 2;
		leftNum = 3;
		rightNum = 4;

		moveUp = false;
		moveDown = false;
		moveLeft = false;
		moveRight = false;

		isTooHigh = false;
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		//make timer count down
		timer -= Time.deltaTime;

		//get playerX and playerY coordinates
		playerX = transform.position.x;
		playerY = transform.position.y;

		if (timer <= 0) 
		{
			//randomly assign up/down/left/right to an arrow, removing
			upNum = UpdateKeyNumbers(upNum);
			downNum = UpdateKeyNumbers(downNum);
			leftNum = UpdateKeyNumbers(leftNum);
			rightNum = UpdateKeyNumbers(rightNum);
			Debug.Log(upNum);

			//reset timer
			timer = timerMax;

			//reset keys
			moveUp = false;
			moveDown = false;
			moveLeft = false;
			moveRight = false;
		}


		//Check for user input, pass to KeyEffect to activate semi-randomized controls
		if (isDying == false)
		{
			if (Input.GetKeyDown (KeyCode.UpArrow)) 
			{
				KeyDownEffect(upNum);
			}

			if (Input.GetKeyDown (KeyCode.DownArrow)) 
			{
				KeyDownEffect(downNum);
			}

			if (Input.GetKeyDown (KeyCode.LeftArrow)) 
			{
				KeyDownEffect(leftNum);
			}

			if (Input.GetKeyDown (KeyCode.RightArrow)) 
			{	
				KeyDownEffect(rightNum);
			}
		}

		//Pull player to bottom of screen if they fall off the ledge
		else if (isDying == true)
		{
			transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
		}


		//stop player if key is up
		if (Input.GetKeyUp(KeyCode.UpArrow))
		{
			KeyUpEffect(upNum);
		}
		if (Input.GetKeyUp(KeyCode.DownArrow))
		{
			KeyUpEffect(downNum);
		}
		if (Input.GetKeyUp(KeyCode.LeftArrow))
		{
			KeyUpEffect(leftNum);
		}
		if (Input.GetKeyUp(KeyCode.RightArrow))
		{
			KeyUpEffect(rightNum);
		}


		//move player if key is down
		if (moveUp == true)
		{
			transform.Translate(Vector3.up * speed * Time.deltaTime);
		}
		if (moveDown == true){
			transform.Translate(Vector3.down * speed * Time.deltaTime);
		}
		if (moveLeft == true){
			transform.Translate(Vector3.left * speed * Time.deltaTime);
		}
		if (moveRight == true){
			transform.Translate(Vector3.right * speed * Time.deltaTime);
		}

	
	}

	//every x seconds, this function is called to scramble the motion activated by arrow keys
	int UpdateKeyNumbers(int num)
	{
		num +=1;

		if (num > 4)
		{
			num = 1;
		}
		return num;
	}

	//assigns movement to keys after keys are pressed
	void KeyDownEffect(int keyNum)
	{
		if ((keyNum==1)&&(isTooHigh == false))
		{
			moveUp = true;
		}
		else if (keyNum == 2){
			moveDown = true;
		}
		else if (keyNum == 3){
			moveLeft = true;
		}
		else if (keyNum == 4){
			moveRight = true;
		}	
	}

	//turns off movement when arrow keys are released
	void KeyUpEffect(int keyNum){
		if (keyNum == 1){
			moveUp = false;
		}
		if (keyNum == 2){
			moveDown = false;
		}
		if (keyNum == 3){
			moveLeft = false;
		}
		if (keyNum == 4){
			moveRight = false;
		}

	}

	//COLLISION DETECTION, HANDLING
	void OnTriggerEnter2D(Collider2D gameObj) 
	{
		if(gameObj.gameObject.tag == "Death")
		{
			//Debug.Log("Collision!");
			//Destroy(gameObject);
			isDying = true;
		}
		if(gameObj.gameObject.tag == "isDead")
		{
			isDead = true;
		}
		if(gameObj.gameObject.tag == "Wall")
		{
			isTooHigh = true;
			moveUp = false;
			//if is no longer touching, set isTooHigh back to false
		}
		if(gameObj.gameObject.tag == "LeftEdge")
		{
			moveLeft = false;
		}
		if(gameObj.gameObject.tag == "Exit")
		{
			//Exit this level, continue to next.
			Application.LoadLevel ("room" + Random.Range (0,Application.levelCount));
		}

	}

	void OnTriggerExit2D(Collider2D gameObj)
	{
		if(gameObj.gameObject.tag == "Wall")
		{
			isTooHigh = false;
		}
	}

	//displays "you're an idiot" message when you die
	void OnGUI()
	{
		if (isDead == true)
		{
			GUI.Box(new Rect(250,150,200,90), "You Have Died, Loser.");
			if(GUI.Button(new Rect(315,195,80,20),"Bawww..."))
			{
				transform.position = new Vector3(-15,-1,0);
				isDying = false;
				isDead = false;
			}
		}
	}

}
