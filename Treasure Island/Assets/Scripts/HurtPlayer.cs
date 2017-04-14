using UnityEngine;
using System.Collections;

public class HurtPlayer : MonoBehaviour {


	private LevelManager theLevelManager;

	public int damageToGive;


	void Start () {
		//find the level manager in the game
		theLevelManager = FindObjectOfType <LevelManager> ();
	}
	

	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			//if the Player enters a hurtbox, deal damage to him
			theLevelManager.HurtPlayer (damageToGive);
		}
	}

}
