using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

	public float waitToRespawn;
	public PlayerController thePlayer;

	public GameObject deathSplosion;

	public int coinCount;
	private int coinBonusLifeCount;
	public int bonusLifeThreshold;

	public AudioSource coinSound;

	public Text scoreText;

	public Image heart1;
	public Image heart2;
	public Image heart3;

	public Sprite heartFull;
	public Sprite heartHalf;
	public Sprite heartEmpty;

	public int maxHealth;
	public int healthCount;

	private bool respawning;

	public ResetOnRespawn[] objectsToReset;

	public bool invincible;

	public Text livesText;
	public int startingLives;
	public int currentLives;

	public GameObject gameOverScreen;

	public AudioSource levelMusic;
	public AudioSource gameOverMusic;

	public bool respawnCoActive;


	void Start () {
		//assign the player object
		thePlayer = FindObjectOfType<PlayerController> ();


		//set health to be full
		healthCount = maxHealth;

		//find which objects need to be resetted when player dies
		objectsToReset = FindObjectsOfType<ResetOnRespawn> ();

		if (PlayerPrefs.HasKey ("CoinCount")) {
			//if the player has a saved coin count, find it
			coinCount = PlayerPrefs.GetInt ("CoinCount");
		}


		//set the score UI to be the coin count
		scoreText.text = "Score: " + coinCount;

		if (PlayerPrefs.HasKey ("PlayerLives")) {
			//if the player has a saved lives, find it
			currentLives = PlayerPrefs.GetInt ("PlayerLives");
		} else {
			//otherwise, give them the specified lives
			currentLives = startingLives;
		}
		//update the lives UI
		livesText.text = "Lives x " + currentLives;
	}
	

	void Update () {
		if (healthCount <= 0 && !respawning) {
			//if health becomes 0 and the player isnt respawning, respawn the player
			Respawn ();
			respawning = true;
		}

		if (coinBonusLifeCount >= bonusLifeThreshold) {
			//if the score becomes greater than a threshold, set the score to 0 and add a life
			currentLives += 1;
			livesText.text = "Lives x " + currentLives;
			coinBonusLifeCount -= bonusLifeThreshold;
		}
	}

	public void Respawn(){
		//upon respawning, remove a life
		currentLives -= 1;
		livesText.text = "Lives x " + currentLives;

		if (currentLives > 0) {
			//start a respawn coroutine, so the game doesnt pause when the player dies
			StartCoroutine ("RespawnCo");
		} else {
			//when the lives hits 0, remove the player and go to game over screen
			thePlayer.gameObject.SetActive (false);
			gameOverScreen.SetActive (true);
			//stop the music and play the gameover music
			levelMusic.Stop ();
			gameOverMusic.Play ();

		}
	}

	public IEnumerator RespawnCo(){

		respawnCoActive = true;
		//upon dieing, turn off the player object
		thePlayer.gameObject.SetActive (false);

		//create a death particle effect
		Instantiate (deathSplosion, thePlayer.transform.position, thePlayer.transform.rotation);

		//wait an amount of seconds before respawning
		yield return new WaitForSeconds (waitToRespawn);

		respawnCoActive = false;

		healthCount = maxHealth;
		respawning = false;

		//reset the health
		UpdateHeartMeter ();

		//lose all coins on death :(
		coinCount = 0;
		scoreText.text = "Score: " + coinCount;
		coinBonusLifeCount = 0;

		//move the player to the respawn location
		thePlayer.transform.position = thePlayer.respawnPosition;

		//reactivate player
		thePlayer.gameObject.SetActive (true);

		//reactivate and reset other objects
		for (int i = 0; i < objectsToReset.Length; i++) {
			objectsToReset [i].gameObject.SetActive (true);
			objectsToReset [i].ResetObject ();
		}
	}


	public void AddCoins(int coinstoAdd){
		//add a coin and update the score
		coinCount += coinstoAdd;
		coinBonusLifeCount += coinstoAdd;
		scoreText.text = "Score: " + coinCount;

		//play the coin sound
		coinSound.Play ();
	}

	public void HurtPlayer(int damageToTake){
		//subtract the damage taken from the health and update the UI
		if (!invincible) {
			//if the player isn't invincible, they can take damage
			healthCount -= damageToTake;
			UpdateHeartMeter ();

			//knockback and play a sound to the damaged player
			thePlayer.Knockback ();
			thePlayer.hurtSound.Play ();
		}
	}

	public void GiveHealth(int healthToGive){
		//add health to the player
		healthCount += healthToGive;

		if (healthCount > maxHealth) {
			healthCount = maxHealth;
		}

		//play a soind for health pickup, update UI
		coinSound.Play ();
		UpdateHeartMeter ();
	}

	public void UpdateHeartMeter(){
		//Change the heart UI sprites based on health amount
		switch (healthCount) {
		case 6:
			heart1.sprite = heartFull;
			heart2.sprite = heartFull;
			heart3.sprite = heartFull;
			return;
		case 5:
			heart1.sprite = heartFull;
			heart2.sprite = heartFull;
			heart3.sprite = heartHalf;
			return;
		case 4:
			heart1.sprite = heartFull;
			heart2.sprite = heartFull;
			heart3.sprite = heartEmpty;
			return;
		case 3:
			heart1.sprite = heartFull;
			heart2.sprite = heartHalf;
			heart3.sprite = heartEmpty;
			return;
		case 2:
			heart1.sprite = heartFull;
			heart2.sprite = heartEmpty;
			heart3.sprite = heartEmpty;
			return;
		case 1:
			heart1.sprite = heartHalf;
			heart2.sprite = heartEmpty;
			heart3.sprite = heartEmpty;
			return;
		case 0:
			heart1.sprite = heartEmpty;
			heart2.sprite = heartEmpty;
			heart3.sprite = heartEmpty;
			return;
		default:
			heart1.sprite = heartEmpty;
			heart2.sprite = heartEmpty;
			heart3.sprite = heartEmpty;
			return;
		}
	}

	public void AddLives(int livesToAdd){
		//add lives to the player
		coinSound.Play ();
		currentLives += livesToAdd;
		livesText.text = "Lives x " + currentLives;
	}
}
