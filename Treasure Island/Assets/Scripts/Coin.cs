using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

	private LevelManager theLevelManager;

	public int coinValue;

	void Start () {
		//Find the level manager
		theLevelManager = FindObjectOfType<LevelManager> ();
	}
	

	void Update () {
	
	}


	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			//when the player collides with coin, add the value to score and deactivate coin
			theLevelManager.AddCoins (coinValue);
			gameObject.SetActive (false);
		}
	}
}
