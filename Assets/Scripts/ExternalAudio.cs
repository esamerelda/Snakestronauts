using UnityEngine;
using System.Collections;

public class ExternalAudio : MonoBehaviour {

    public AudioClip engineSound;

    public AudioClip clunkSound;
    public AudioClip fireSound;
    public AudioClip teaSlurp;
    public AudioClip snakeSlither;
    //private bool playEngineSound;

	//public AudioClip throwSound;
	//public AudioClip canHitEnemy;
	//public AudioClip canHitsGround;
	//public AudioClip stompSound;

	private AudioSource source;

	// Use this for initialization
	void Awake ()
	{
		source = GetComponent<AudioSource>();
	}
	void Start () 
	{
	    
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}


    public void PlayEngineSound(bool play)
    {
        if (play)
        {
            if (!source.isPlaying)
            {
                source.clip = engineSound;
                source.loop = true;
                source.Play();
            }
        }
        else
        {
            source.loop = false;
            //Debug.Log("Don't Play");
        }

    }
    
    public void PlayClunk()
    {
        source.PlayOneShot(clunkSound);
    }

    public void PlayFire()
    {
        source.PlayOneShot(fireSound);
    }

    public void PlayTeaSlurp()
    {
        source.PlayOneShot(teaSlurp);
    }

    public void PlaySnakeSlither()
    {
        source.PlayOneShot(snakeSlither);
    }
    //---------------------------------------------

	/*public void PlayCanHitsGround()
	{
		source.PlayOneShot(canHitsGround);
	}

	public void PlayCanHitsEnemy()
	{
		source.PlayOneShot(canHitEnemy);
	}
	public void PlayThrowSound()
	{
		source.PlayOneShot(throwSound);
	}
	public void PlayStompSound()
	{
		source.PlayOneShot(stompSound);
	}*/
}
