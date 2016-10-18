using UnityEngine;
using System.Collections;

public class RandomRotationAndLaunch : MonoBehaviour {

    private float minRotateTimer = 0.5f;
    private float maxRotateTimer = 3f;
    private float minRotationSpeed = -75f;
    private float maxRotationSpeed = 75f;
    private float minLaunchTimer = 0.5f;
    private float maxLaunchTimer = 5f;
    private float minThrust = 40f;
    private float maxThrust = 80f;
    private float thrust;

    private Rigidbody2D rb;

    private float rotationTimer;
    private float launchTimer;
    private float rotationSpeed;

    void Awake()
    {
        //FadeIn(fadeInTime);
        rb = GetComponent<Rigidbody2D>();
    }
	// Use this for initialization
	void Start () {
        ResetRotationTimer();
        ResetLaunchTimer();
	
	}
	
	// Update is called once per frame
	void Update () {

        //constantly rotate astronaut
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        //ROTATION TIMER
        rotationTimer -= Time.deltaTime;
        if (rotationTimer <= 0)
        {

            //reset timer
            ResetRotationTimer();
        }

        //LAUNCH TIMER
        launchTimer -= Time.deltaTime;
        if (launchTimer <= 0)
        {
            thrust = Random.Range(minThrust, maxThrust);
            transform.position += transform.up * Time.deltaTime * thrust;
        }

    }

    void ResetRotationTimer()
    {
        //newRotation = Random.Range(minRotation, maxRotation);
        rotationTimer = Random.Range(minRotateTimer, maxRotateTimer);
        rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);

    }

    void ResetLaunchTimer()
    {
        launchTimer = Random.Range(minLaunchTimer, maxLaunchTimer);
    }
}
