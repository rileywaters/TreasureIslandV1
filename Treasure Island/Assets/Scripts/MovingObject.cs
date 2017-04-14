using UnityEngine;
using System.Collections;

public class MovingObject : MonoBehaviour {

	public GameObject objectToMove;

	public Transform startPoint;
	public Transform endPoint;

	public float moveSpeed;

	private Vector3 currentTarget;


	void Start () {
		//set the initial target to the endPoint;
		currentTarget = endPoint.position;

	}
	

	void Update () {
		//move the object toward the end point
		objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, currentTarget, moveSpeed * Time.deltaTime);

		if (objectToMove.transform.position == endPoint.position) {
			//if the object hits the endpoint, change target to startpoint
			currentTarget = startPoint.position;
		}

		if (objectToMove.transform.position == startPoint.position) {
			//if the object hits the startpoint, change target to endpoint
			currentTarget = endPoint.position;
		}
	}
}
