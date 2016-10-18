using UnityEngine;
using System.Collections;

public class FireChangeColorScript : MonoBehaviour {

    private float minDestructTime = 1f;
    private float maxDestructTime = 10f;
    private float secondsTilDestruction;
    //private float fadeSpeed;
    //private float fadeTime = 1f;
    //private float fadeTimer;
    //public bool isRed;

    //public Color fireRed;
    //public Color fireYellow;

    private SpriteRenderer sprite;
    //private Sprite fireSprite;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        //secondsTilDestruction = Random.Range(minDestructTime, maxDestructTime);
        //fireSprite = sprite.sprite;
    }

	// Use this for initialization
	void Start () {
        secondsTilDestruction = Random.Range(minDestructTime, maxDestructTime);
        Destroy(gameObject, secondsTilDestruction);
        //isRed = true;
        //sprite.color = fireRed;

        //fadeSpeed = Random.Range(1, 2);
        //fadeTimer = fadeSpeed;


    }
	
	// Update is called once per frame
	void Update () {
        //renderer.color = fireRed;

        //fade in & out
        float a = Mathf.PingPong(Time.time / secondsTilDestruction, 1.0f);
        sprite.color = new Color(255f, 0f, 0f, a);

        //float step = secondsTilDestruction / Time.deltaTime;

        //sprite.color = new Color(1f, 1f, 1f, Mathf.Lerp(sprite.color.a, 0f, step));
        
	}
}
