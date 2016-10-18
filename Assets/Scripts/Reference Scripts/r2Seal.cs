using UnityEngine;
using System.Collections;

public class r2Seal : MonoBehaviour {

	// Use this for initialization;
	float sealGrowthTimer = 0.0f;
	float sealGrowthTimerMax = 0.1f;

	float sealWidth = .1f;
	float sealHeight = .1f;

	float sealGrowthRate = .02f;
	float sealMaxScale = 5;

	float conversationTimer = 0.0f;
	float conversationRate = 1.20f;
	float conversationNumber = 0;

	float column1 = 0;
	float column2 = Screen.width - 150;

	bool gameWon = false;
	//bool gameLost = false;

	void Start () {
		
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		//reduce seal growth timer
		sealGrowthTimer -= Time.deltaTime;

		//reduce conversation timer
		conversationTimer -= Time.deltaTime;

		//Seal grows every time the counter reaches 0
		if ((sealWidth < sealMaxScale) && (sealGrowthTimer <= 0))
		{
			//seal grows
			sealWidth += sealGrowthRate;
			sealHeight += sealGrowthRate;
			transform.localScale = new Vector3(sealWidth, sealHeight, 0);


			//reset timer
			sealGrowthTimer = sealGrowthTimerMax;

		}

		//New message pops up every time convo timer reaches 0
		if (conversationTimer <= 0)
		{
			conversationNumber ++;
			conversationTimer = conversationRate;
		}

		
	}

	void OnGUI()
	{

		//display seal size for testing
		//GUI.Box (new Rect(10, 10, 100, 25), sealWidth.ToString ());
		//GUI.Box (new Rect(10, 35, 100, 25), conversationNumber.ToString());

		//Random conversation from the creepy seal
		if (conversationNumber >= 1)
		{
			GUI.Box (new Rect(column1, 10, 150, 25), "Hi!!!!");
		}
		if (conversationNumber >= 2)
		{
			GUI.Box (new Rect(column1, 135, 150, 25), "Whatcha DOOOOin???");
		}
		if (conversationNumber >= 3)
		{
			GUI.Box (new Rect(column1, 160, 150, 25), "Wanna hang out?");
		}
		if (conversationNumber >= 4)
		{
			GUI.Box (new Rect(column2, 10, 150, 25), "I have an idea!!!");
		}
		if (conversationNumber >= 5)
		{
			GUI.Box (new Rect(column1, 35, 160, 25), "Let's be BEST FRIENDS!!");
		}
		if (conversationNumber >= 6)
		{
			GUI.Box (new Rect(column1, 185, 150, 25), "Stay right there....");
		}
		if (conversationNumber >= 7)
		{
			GUI.Box (new Rect(column1, 210, 150, 25), "You look hurt...");
		}
		if (conversationNumber >= 8)
		{
			GUI.Box (new Rect(column2, 35, 150, 25), "Let me lick you...");
		}
		if (conversationNumber >= 9)
		{
			GUI.Box (new Rect(column1, 60, 150, 25), "THEN WE CAN DANCE!");
		}
		if (conversationNumber >= 10)
		{
			GUI.Box (new Rect(column1, 85, 150, 25), "LET'S GO CLUBBIN'!");
		}
		if (conversationNumber >= 11)
		{
			GUI.Box (new Rect(column2, 60, 150, 25), "How to seal the deal...");
		}
		if (conversationNumber >= 12)
		{
			GUI.Box (new Rect(column1, 235, 300, 25), "What do plumbers and eskimos have in common?");
		}
		if (conversationNumber >= 13)
		{
			GUI.Box (new Rect(column2 - 20, 235, 175, 25), "They both love a tight seal!");
		}
		if (conversationNumber >= 14)
		{
			GUI.Box (new Rect(column2, 85, 150, 25), "Show some sealidarity.");
		}
		if (conversationNumber >= 15)
		{
			GUI.Box (new Rect(column1, 110, 200, 25), "I'm coming to seal your heart...");
		}
		if (conversationNumber >= 16)
		{
			GUI.Box (new Rect(column2 - 50, 210, 200, 25), "I wonder what blood tastes like...");
		}
		if (conversationNumber >= 17)
		{
			GUI.Box (new Rect(column2, 135, 150, 25), "Let me try it!!!");
		}
		if (conversationNumber >= 18)
		{
			GUI.Box (new Rect(column2, 185, 150, 25), "You're so yummy...");
		}
		if (conversationNumber >= 19)
		{
			GUI.Box (new Rect(column2, 160, 150, 25), "I bet souls are good...");
		}
		if (conversationNumber >= 20)
		{
			GUI.Box (new Rect(column2, 110, 150, 25), "YOU ARE MINE!!!");
		}
		if ((conversationNumber >= 21) && (conversationNumber <= 23))
		{
			if (GUI.Button(new Rect(Screen.width/2 - 75 ,Screen.height - 25,150,25),"RUN!!!"))
			{
				gameWon = true;
			}
		}


		//Losing the game
		if ((conversationNumber >= 23) && (gameWon == false))
		{
			GUI.Box(new Rect(Screen.width/2 - 150 , Screen.height / 4 * 3, 300, 25), "You have been mauled by a slow-moving animal.");
			if (conversationNumber >= 24)
			{
			GUI.Box(new Rect(Screen.width/2 - 100 , Screen.height / 4 * 3 + 35, 200, 25), "Your fate has been sealed.");
			if (conversationNumber >=25)
				{
					Application.LoadLevel ("room" + Random.Range (0,Application.levelCount));
				}
			}
		}



		//Winning the game
		if ((conversationNumber >= 23) && (gameWon == true))
		{
			GUI.Box(new Rect(Screen.width/2 - 150 , Screen.height / 4 * 3, 300, 25), "You have narrowly escaped...");
			if (conversationNumber >= 24)
				{
					GUI.Box(new Rect(Screen.width/2 - 100 , Screen.height / 4 * 3 + 35, 200, 25), "Your fate has not been sealed.");
					if (conversationNumber>=25)
					{
						Application.LoadLevel("room" + Random.Range (0, Application.levelCount));
					}
				}
		}
	}

}
