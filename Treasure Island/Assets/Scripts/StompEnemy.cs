using UnityEngine;
using System.Collections;

public class StompEnemy : MonoBehaviour {

	private Rigidbody2D playerRigidbody;
	public float bounceForce;

	public GameObject deathSplosion;

	void Start () {
		//find the players rigidbody
		playerRigidbody = transform.parent.GetComponent<Rigidbody2D> ();
	}
	

	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Enemy") {
			//when the player feet touches top of enemy, set enemy to false
			other.gameObject.SetActive (false);
			//play a death effect on the enemy
			Instantiate(deathSplosion, other.transform.position, other.transform.rotation);
			//bounce the player up a bit
			playerRigidbody.velocity = new Vector3 (playerRigidbody.velocity.x, bounceForce, 0f);
		}
		if (other.tag == "Boss") {
			playerRigidbody.velocity = new Vector3 (playerRigidbody.velocity.x, bounceForce, 0f);
			other.transform.parent.GetComponent<Boss> ().takeDamage = true;
		}
	}


}
