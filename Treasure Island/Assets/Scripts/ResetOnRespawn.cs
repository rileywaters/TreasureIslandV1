using UnityEngine;
using System.Collections;

public class ResetOnRespawn : MonoBehaviour {

	private Vector3 startPosition;
	private Quaternion startRotation;
	private Vector3 startLocalScale;

	private Rigidbody2D myRigidbody;

	void Start () {
		//store the original location of the object
		startPosition = transform.position;
		startRotation = transform.rotation;
		startLocalScale = transform.localScale;

		if (GetComponent<Rigidbody2D> () != null) {
			//if there is no rigidbody attached to object, get one
			myRigidbody = GetComponent<Rigidbody2D> ();
		}
	}
	

	void Update () {
	
	}

	public void ResetObject(){
		//reset the objects position back to original
		transform.position = startPosition;
		transform.rotation = startRotation;
		transform.localScale = startLocalScale;

		if (myRigidbody != null) {
			//if there was no rigidbody originally, reset it to zero
			myRigidbody.velocity = Vector3.zero;
		}
	}
}
