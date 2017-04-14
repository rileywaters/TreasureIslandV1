using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {


	public GameObject target;
	public float followAhead;

	private Vector3 targetPosition;

	public float smoothing;

	public bool followTarget;

	void Start () {
		//start by following the set target
		followTarget = true;
	}
	

	void Update () {
		if (followTarget) {

			//set the position of the camera target to that of the target object
			targetPosition = new Vector3 (target.transform.position.x, target.transform.position.y, transform.position.z);
	

			if (target.transform.localScale.x > 0f) {
				//moves target of camera ahead of player when moving right
				targetPosition = new Vector3 (targetPosition.x + followAhead, targetPosition.y, targetPosition.z);
			} else {
				//moves target of camera ahead of player when moving left
				targetPosition = new Vector3 (targetPosition.x - followAhead, targetPosition.y, targetPosition.z);
			}
			
			//smooths the camera movement when player changes direction
			transform.position = Vector3.Lerp (transform.position, targetPosition, smoothing * Time.deltaTime);
		}
	}
}
