using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour {

	public string levelSelect;
	public string mainMenu;

	private LevelManager theLevelManager;

	public GameObject thePauseScreen;

	private PlayerController thePlayer;

	void Start () {
		//find objects
		theLevelManager = FindObjectOfType<LevelManager> ();
		thePlayer = FindObjectOfType<PlayerController> ();
	}
	

	void Update () {
		if(Input.GetButtonDown("Pause")){
			if (Time.timeScale == 0f) {
				//if game is paused and pause button is pressed again, unpause
				ResumeGame ();
			} else {
				//if game isn't paused and pause button is pressed, pause
				PauseGame ();
			}
		}
	}

	public void PauseGame(){
		//stop the time, activate pause screen, stop player from moving, stop music
		Time.timeScale = 0;
		thePauseScreen.SetActive (true);
		thePlayer.canMove = false;
		theLevelManager.levelMusic.Pause ();
	}

	public void ResumeGame(){
		//remove pause screen, start time, allow player to move, play music
		thePauseScreen.SetActive (false);
		Time.timeScale = 1f;
		thePlayer.canMove = true;
		theLevelManager.levelMusic.Play ();
	}

	public void LevelSelect(){
		//Save the lives and coins
		PlayerPrefs.SetInt ("PlayerLives", theLevelManager.currentLives);
		PlayerPrefs.SetInt ("CoinCount", theLevelManager.coinCount);

		//start time, load level select
		Time.timeScale = 1f;
		SceneManager.LoadScene (levelSelect);
	}

	public void QuitToMainMenu(){
		//start time, load main menu
		Time.timeScale = 1f;
		SceneManager.LoadScene (mainMenu);
	}
}
