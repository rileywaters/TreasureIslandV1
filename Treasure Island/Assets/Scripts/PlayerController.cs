using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	private float activeMoveSpeed;

	public bool canMove;

	public Rigidbody2D myRigidbody;

	public float jumpSpeed;

	public Transform groundCheck;
	public float groundCheckRadius;
	public LayerMask whatIsGround;

	public bool isGrounded;

	private Animator myAnim;

	public Vector3 respawnPosition;

	public LevelManager theLevelManager;

	public GameObject stompBox;

	public float knockbackForce;
	public float knockbackLength;
	private float knockbackCounter;

	public float invincibilityLength;
	private float invincibilityCounter;

	public AudioSource jumpSound;
	public AudioSource hurtSound;

	private bool onPlatform;
	public float onPlatformSpeedModifier;


	void Start () {
		//assign the animator and rigidbody(movement/gravity) to the player
		myRigidbody = GetComponent<Rigidbody2D> ();
		myAnim = GetComponent<Animator> ();

		//the position that the player respawns is his first position when entering game
		respawnPosition = transform.position;

		//find the level manager, set the active movespeed
		theLevelManager = FindObjectOfType<LevelManager> ();
		activeMoveSpeed = moveSpeed;

		//allow player to move
		canMove = true;
	}
	

	void Update () {
		//check if the player is on the ground
		isGrounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, whatIsGround);

		if (knockbackCounter <= 0 && canMove) {
			//if the player is not knocked back, they can move

			if (onPlatform) {
				//if a player is on a platform, reduce their speed
				activeMoveSpeed = moveSpeed * onPlatformSpeedModifier;
			} else {
				//reset speed when leaving platform
				activeMoveSpeed = moveSpeed;
			}


			if (Input.GetAxisRaw ("Horizontal") > 0f) {
				//move the player right
				myRigidbody.velocity = new Vector3 (activeMoveSpeed, myRigidbody.velocity.y, 0f);
				//have the player sprite look right
				transform.localScale = new Vector3 (1f, 1f, 1f);

		
			} else if (Input.GetAxisRaw ("Horizontal") < 0f) {
				//move the player left
				myRigidbody.velocity = new Vector3 (-activeMoveSpeed, myRigidbody.velocity.y, 0f);
				//have the player sprite look left
				transform.localScale = new Vector3 (-1f, 1f, 1f);

			} else {
				//Have the player stop moving when no buttons are pressed
				myRigidbody.velocity = new Vector3 (0f, myRigidbody.velocity.y, 0f);
			}

			if (Input.GetButtonDown ("Jump") && isGrounded) {
				//have the player jump, only if he is grounded
				myRigidbody.velocity = new Vector3 (myRigidbody.velocity.x, jumpSpeed, 0f);
				//play jump sound
				jumpSound.Play ();
			}
				
		}

		if (knockbackCounter > 0) {
			//knockback the player for an amount of time
			knockbackCounter -= Time.deltaTime;

			if (transform.localScale.x > 0) {
				//knockback left
				myRigidbody.velocity = new Vector3 (-knockbackForce, knockbackForce, 0f);
			} else {
				//knockback right
				myRigidbody.velocity = new Vector3 (knockbackForce, knockbackForce, 0f);
			}
		}

		if (invincibilityCounter > 0) {
			//give the player invincibility for a small amount of time
			invincibilityCounter -= Time.deltaTime;
		}
		if (invincibilityCounter <= 0) {
			//remove invincibility when timer is out
			theLevelManager.invincible = false;
		}
		//set the speed and grounded variables for the animator conditions
		myAnim.SetFloat ("Speed", Mathf.Abs(myRigidbody.velocity.x));
		myAnim.SetBool ("Grounded", isGrounded);

		if (myRigidbody.velocity.y < 0) {
			//only activate the stompbox if the player is moving down
			stompBox.SetActive (true);
		} else {
			stompBox.SetActive (false);
		}

	}

	public void Knockback(){
		//knock back the player and give them short invincibility
		knockbackCounter = knockbackLength;
		invincibilityCounter = invincibilityLength;
		theLevelManager.invincible = true;
	}


	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "KillPlane") {
			//if the player touches a kill plane, respawn the player
			theLevelManager.Respawn ();
		}

		if (other.tag == "Checkpoint") {
			//if the player touches a checkpoint, set the respawn position to the checkpoint
			respawnPosition = other.transform.position;
		}
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "MovingPlatform") {
			//if the player is on a moving platform, move with it
			transform.parent = other.transform;
			onPlatform = true;
		}
	
	}

	void OnCollisionExit2D(Collision2D other){
		if (other.gameObject.tag == "MovingPlatform") {
			//if the player leaves a moving platform, do not move with it
			transform.parent = null;
			onPlatform = false;
		}

	}
}
