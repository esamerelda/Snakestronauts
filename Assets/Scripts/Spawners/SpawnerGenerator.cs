using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnerGenerator : MonoBehaviour {

    float spawnTimer;
    float minSpawnTimer = 1f;
    float maxSpawnTimer = 5f;

    private int randomDisconautsToGenerate = 20;
    private int totalSpawners;
    List<Rigidbody2D> spawnerList = new List<Rigidbody2D>();

    private int disconautFires = 10;
    private int disconautSnakes = 10;
    private int disconautTeas = 5;

    public Rigidbody2D discoSnakesPrefab;
    public Rigidbody2D discoFiresPrefab;
    public Rigidbody2D discoTeasPrefab;

    private GameObject topBound;
    private GameObject rightBound;
    private GameObject bottomBound;
    private GameObject leftBound;

    private BoundaryScript topBoundScript;
    private BoundaryScript rightBoundScript;
    private BoundaryScript bottomBoundScript;
    private BoundaryScript leftBoundScript;

    float topY;
    float rightX;
    float bottomY;
    float leftX;

    float spawnerX;
    float spawnerY;

    

    // Use this for initialization

    void Awake()
    {
        topBound = GameObject.Find("TopBoundary");
        rightBound = GameObject.Find("RightBoundary");
        bottomBound = GameObject.Find("BottomBoundary");
        leftBound = GameObject.Find("LeftBoundary");

        topBoundScript = topBound.GetComponent<BoundaryScript>();
        rightBoundScript = rightBound.GetComponent<BoundaryScript>();
        bottomBoundScript = bottomBound.GetComponent<BoundaryScript>();
        leftBoundScript = leftBound.GetComponent<BoundaryScript>();
    }

	void Start () {
        //Add prefabs to list
        spawnerList.Add(discoSnakesPrefab);
        spawnerList.Add(discoFiresPrefab);
        spawnerList.Add(discoTeasPrefab);
        totalSpawners = spawnerList.Count;

        //Locate Boundaries
        /*topY = topBoundScript.GetY();
        rightX = rightBoundScript.GetX();
        bottomY = bottomBoundScript.GetY();
        leftX = leftBoundScript.GetX();*/
        FindBoundaries();

        GenerateRandomSpawners();
        ResetSpawnerTimer();
        
    }
	
	// Update is called once per frame
	void Update () {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            GenerateRandomSpawners();
            ResetSpawnerTimer();
        }
	
	}

    void ResetSpawnerTimer()
    {
        spawnTimer = Random.Range(minSpawnTimer, maxSpawnTimer);
    }

    void FindBoundaries()
    {
        topY = topBoundScript.GetY();
        rightX = rightBoundScript.GetX();
        bottomY = bottomBoundScript.GetY();
        leftX = leftBoundScript.GetX();
    }

    void GenerateRandomSpawners()
    {
        FindBoundaries();
        for (int i = 0; i < randomDisconautsToGenerate; i++)
        {
            spawnerX = Random.Range(leftX, rightX);
            spawnerY = Random.Range(bottomY, topY);
            int prefabIndex = Random.Range(0, totalSpawners);
            Instantiate(spawnerList[prefabIndex], new Vector3(spawnerX, spawnerY, 0), Quaternion.Euler(0, 0, 0));
        }
    }


}
