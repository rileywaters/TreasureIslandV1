using UnityEngine;
using System.Collections;

public class HealthPickup : MonoBehaviour {

	public int healthToGive;

	private LevelManager theLevelManager;

	void Start () {
		//Find the level manager
		theLevelManager = FindObjectOfType<LevelManager> ();
	}

	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			//if the player collides with a health pickup, give them health and remove pickup
			theLevelManager.GiveHealth (healthToGive);
			gameObject.SetActive (false);
		}
	}
}
