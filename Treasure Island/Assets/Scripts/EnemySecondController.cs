using UnityEngine;
using System.Collections;

public class EnemySecondController : MonoBehaviour {

	public Transform leftPoint;
	public Transform rightPoint;

	public float moveSpeed;

	private Rigidbody2D myRigidbody;

	public bool movingRight;

	void Start () {
		//assign the rigidbody
		myRigidbody = GetComponent<Rigidbody2D> ();
	}
	

	void Update () {
		
		if (movingRight && transform.position.x > rightPoint.position.x) {
			//If moved past rightPoint, turn around
			movingRight = false;
		}
		if (!movingRight && transform.position.x < leftPoint.position.x) {
			//If moved past leftPoint, turn around
			movingRight = true;
		}
		if (movingRight) {
			//move right
			myRigidbody.velocity = new Vector3 (moveSpeed, myRigidbody.velocity.y, 0f);
		} else {
			//move left
			myRigidbody.velocity = new Vector3 (-moveSpeed, myRigidbody.velocity.y, 0f);
		}
	}
}
