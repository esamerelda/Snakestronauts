using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HighScores : MonoBehaviour {

    //private GameObject highScoreObj;
    public static HighScores highScoreScript;
    private float highScore;

    List<float> highScoreList = new List<float>();

    void Awake()

    {
        //check if instance already exists
        if (highScoreScript == null)
        {
            DontDestroyOnLoad(this);
            highScoreScript = this;
        }
        //if another copy already exists, self-destruct
        else if (highScoreScript != this)
        {
            Destroy(this);
            //Destroy(gameObject);
        }

    }
        // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AddNewScore(float newScore)
    {
        highScoreList.Add(newScore);
        //Debug.Log("Score added!");
    }

    public float GetHighScore()
    {
        highScore = 0;
        for(int i = 0; i < highScoreList.Count; i++)
        {
            if (highScoreList[i] > highScore)
            {
                highScore = highScoreList[i];
            }
            else
            {
                //remove
            }
        }
        return highScore;
    }
}
