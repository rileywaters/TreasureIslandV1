using UnityEngine;
using System.Collections;

public class EnemyFirstController : MonoBehaviour {

	public float moveSpeed;
	private bool canMove;

	private Rigidbody2D myRigidbody;

	void Start () {
		//assign the rigidbody
		myRigidbody = GetComponent<Rigidbody2D> ();
	}
	

	void Update () {
		if (canMove) {
			//if the enemy is in sight, move left
			myRigidbody.velocity = new Vector3 (-moveSpeed, myRigidbody.velocity.y, 0f);
		}
	}

	void OnBecameVisible(){
		//when this enemy becomes visible, it can begin moving
		canMove = true;
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "KillPlane") {
			//disable the enemy when it touches a killplane
			gameObject.SetActive (false);
		}
	}

	void OnEnable(){
		//makes it so this enemy completely resets when reenabled
		canMove = false;
	}
}
