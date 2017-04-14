using UnityEngine;
using System.Collections;

public class CheckpointController : MonoBehaviour {

	public Sprite flagClosed;
	public Sprite flagOpen;

	public bool checkpointActive;

	private SpriteRenderer theSpriteRenderer;

	void Start () {
		//assign the sprite renderer
		theSpriteRenderer = GetComponent<SpriteRenderer> ();
	}
	

	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			//if the player collides with a checkpoint, change the sprite to opened flag
			theSpriteRenderer.sprite = flagOpen;
			//activate the checkpoint
			checkpointActive = true;
		}
	}

}
