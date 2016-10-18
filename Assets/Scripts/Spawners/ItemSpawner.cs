using UnityEngine;
using System.Collections;

public class ItemSpawner : MonoBehaviour {

    private float minLifeSpan = 3f;
    private float maxLifeSpan = 15f;

    //private float fadeSpeed = 1f;
    private float minSpawnTimer = .1f;
    private float maxSpawnTimer = 4f;

    private float maxItemLaunchSpeed = 50f;

    private float lifeSpan;
    private float spawnTimer;

    private bool fadeOut;
    private float fadeTime;

    private float itemLaunchSpeedX;
    private float itemLaunchSpeedY;

    //private Sprite sprite;


    public Rigidbody2D prefabToSpawn;

    void Awake()
    {
        //sprite = GetComponent<SpriteRenderer>().GetComponent<Sprite>();
        Color newColor = new Color(1, 1, 1, 0);
        GetComponent<SpriteRenderer>().material.color = newColor;
    }
    // Use this for initialization
    void Start()
    {
        lifeSpan = Random.Range(minLifeSpan, maxLifeSpan);
        Destroy(gameObject, lifeSpan);
        fadeTime = lifeSpan / 3;
        StartCoroutine(FadeTo(1.0f, fadeTime));
        //spawnTimer = Random.Range(minSpawnTimer, maxSpawnTimer);
        ResetSpawnTimer();

    }

    // Update is called once per frame
    void Update()
    {
        //REDUCE LIFE LEFT
        lifeSpan -= Time.deltaTime;
        //if lifespan is less than 1/3, fade out spawner
        if ((lifeSpan < fadeTime) && (lifeSpan > 0) && (fadeOut == false))
        {
            StartCoroutine(FadeTo(0.0f, fadeTime));
            fadeOut = true;
        }
        

        //SPAWN ITEM TIMER
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            //Instantiate Prefab
            itemLaunchSpeedX = Random.Range(-maxItemLaunchSpeed, maxItemLaunchSpeed);
            itemLaunchSpeedY = Random.Range(-maxItemLaunchSpeed, maxItemLaunchSpeed);
            Rigidbody2D newSpawned = Instantiate(prefabToSpawn, transform.position, Quaternion.Euler(new Vector3(0, 0, 0f))) as Rigidbody2D;
            newSpawned.velocity = new Vector2(itemLaunchSpeedX, itemLaunchSpeedY);
            ResetSpawnTimer();
        }
    }

    void ResetSpawnTimer()
    {
        spawnTimer = Random.Range(minSpawnTimer, maxSpawnTimer);
    }


    //fader
    IEnumerator FadeTo(float aValue, float aTime)
    {
        float alpha = GetComponent<SpriteRenderer>().material.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
            GetComponent<SpriteRenderer>().material.color = newColor;
            yield return null;
        }
    }
}
