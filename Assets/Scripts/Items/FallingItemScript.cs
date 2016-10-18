using UnityEngine;
using System.Collections;

public class FallingItemScript : MonoBehaviour {

    float secondsTilDestruction = 10;

    private GameObject camera;
    ExternalAudio extAudio;

    void Awake()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        extAudio = camera.GetComponent<ExternalAudio>();
    }


	// Use this for initialization
	void Start ()
    {
        //destroy after x seconds
        Destroy(gameObject, secondsTilDestruction);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D gameObj)
    {
        if (gameObj.gameObject.tag == "ground")
        {
            Destroy(gameObject);
        }
    }
}
