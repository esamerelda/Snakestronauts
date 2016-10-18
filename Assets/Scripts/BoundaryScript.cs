using UnityEngine;
using System.Collections;

public class BoundaryScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
      
	
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(transform.position.x);
	}

    public float GetX()
    {
        return transform.position.x;
    }

    public float GetY()
    {
        return transform.position.y;
    }
}
