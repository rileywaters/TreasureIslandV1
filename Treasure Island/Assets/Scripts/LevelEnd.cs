using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class LevelEnd : MonoBehaviour {

	public string levelToLoad;
	public string levelToUnlock;

	private PlayerController thePlayer;
	private CameraController theCamera;
	private LevelManager theLevelManager;

	public float waitToMove;
	public float waitToLoad;

	private bool movePlayer;

	public Sprite flagOpen;

	private SpriteRenderer theSpriteRenderer;

	void Start () {
		//find the objects
		thePlayer = FindObjectOfType<PlayerController> ();
		theCamera = FindObjectOfType<CameraController> ();
		theLevelManager = FindObjectOfType<LevelManager> ();

		theSpriteRenderer = GetComponent<SpriteRenderer> ();
	}
	

	void Update () {
		if (movePlayer) {
			//if the player should move, move them right
			thePlayer.myRigidbody.velocity = new Vector3 (thePlayer.moveSpeed, thePlayer.myRigidbody.velocity.y, 0f);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			//if the player hits the level end, open the flag and start end level coroutine

			theSpriteRenderer.sprite = flagOpen;

			StartCoroutine("LevelEndCo");
		}
	}

	public IEnumerator LevelEndCo(){
		//stop the player from moving, stop camera from following, make player invincible
		thePlayer.canMove = false;
		theCamera.followTarget = false;
		theLevelManager.invincible = true;

		//stop the music and start the end level music
		theLevelManager.levelMusic.Stop ();
		theLevelManager.gameOverMusic.Play ();

		//stop the player
		thePlayer.myRigidbody.velocity = Vector3.zero;

		//Save the players coins and lives
		PlayerPrefs.SetInt ("CoinCount", theLevelManager.coinCount);
		PlayerPrefs.SetInt ("PlayerLives", theLevelManager.currentLives);

		//unlock the specified level
		PlayerPrefs.SetInt (levelToUnlock, 1);

		//wait some seconds then move the player right
		yield return new WaitForSeconds (waitToMove);
		movePlayer = true;
		//wait some seconds then load the next scene
		yield return new WaitForSeconds (waitToLoad);
		SceneManager.LoadScene (levelToLoad);
	}


}
