using UnityEngine;
using System.Collections;

public class RotateBackground : MonoBehaviour {

    //float rotateSpeedMin = 1f; 
	//float rotateSpeedMax = 40f;  //how many degrees bg image will rotate per second

    float rotateSpeed;
    private SpriteRenderer sprite;
    private float fadeTime = 4f;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

	// Use this for initialization
	void Start () {
        rotateSpeed = 30f;
        fadeTime = 4f;
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        //rotate background
    	transform.Rotate (0, 0, rotateSpeed * Time.deltaTime); //rotates x degrees per second around z axis
        //ping pong color of background
        float a = Mathf.PingPong(Time.time / fadeTime, 1.0f);
        sprite.color = new Color(a, a, a, 1f);
    }

    public void SetRotateSpeed(float newSpeed)
    {

        rotateSpeed = newSpeed * 5f;
        fadeTime = newSpeed / 10f;
    }
}
