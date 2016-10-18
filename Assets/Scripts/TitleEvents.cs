using UnityEngine;
using System.Collections;

public class TitleEvents : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LoadInstructions()
    {
        Application.LoadLevel("InstructionsScreen");
    }

    public void StartGame()
    {
        Application.LoadLevel("GameScene");
    }
}
