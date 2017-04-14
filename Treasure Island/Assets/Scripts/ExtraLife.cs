using UnityEngine;
using System.Collections;

public class ExtraLife : MonoBehaviour {

	public int livesToGive;

	private LevelManager theLevelManager;

	void Start () {
		//find the level manager
		theLevelManager = FindObjectOfType<LevelManager> ();
	}

	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			//when the player collides with an extra life object, add life and deactivate object
			theLevelManager.AddLives (livesToGive);
			gameObject.SetActive (false);
		}
	}
}
