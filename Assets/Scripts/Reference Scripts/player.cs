using UnityEngine;
using System.Collections;

public class player : MonoBehaviour {
	public int moveSpeed = 8;
	public int animatorSpeed = 2;

	private Animator animator;
	private int numShoes = 0;
	private bool movingUp, movingDown, movingRight, movingLeft;
	private bool blockedUp, blockedDown, blockedRight, blockedLeft;


	void Start () {
		animator = GetComponent<Animator>();
	}

	
	// Update is called once per frame
	void Update () {

		// --- input ---
		movingUp = Input.GetKey(KeyCode.UpArrow);
		movingDown = Input.GetKey(KeyCode.DownArrow);
		movingRight = Input.GetKey(KeyCode.RightArrow);
		movingLeft = Input.GetKey(KeyCode.LeftArrow);

		/*
		// Explicitly play the state of the animation when stopped so that we stop on a "hand down" instead of "hand 45 degrees out" state
		string animatorState = numShoes + "shoes_" + ((movingUp || movingDown) ? "vertical" : "horizontal");
		animator.Play(animatorState, 0, 0);			
		*/

		// --- movement ---
		if (movingUp && !movingDown) {
			transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
			animator.SetBool("isVertical", true);
			animator.speed = animatorSpeed;
		}
		if (movingDown) {
			transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
			animator.SetBool("isVertical", true);
			animator.speed = animatorSpeed;
		}
		if (movingRight && !movingLeft) {
			
			transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
			animator.SetBool("isVertical", false);
			animator.speed = animatorSpeed;
		}
		if (movingLeft) {
			transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
			animator.SetBool("isVertical", false);
			animator.speed = animatorSpeed;
		}
		if (!movingUp && !movingDown && !movingLeft && !movingRight) {
			animator.speed = 0;
		}

		animator.SetInteger("numShoes", numShoes);
	}


	void addShoe() {
		Debug.Log("Got shoe, numShoes: " + ++numShoes);
	}


	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "shoe") {
			addShoe();
			Destroy(other.gameObject); //destroys shoe
		} 
		else if ((other.gameObject.tag == "mat") && (numShoes == 4)) {
			winCondition();
			Application.LoadLevel ("room" + Random.Range (0,Application.levelCount));

		}
	}


	void winCondition() {
		Debug.Log("Winning!");
		Application.LoadLevel ("room" + Random.Range (0,Application.levelCount));

	}

}
