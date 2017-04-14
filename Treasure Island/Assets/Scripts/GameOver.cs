using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

	public string levelSelect;
	public string mainMenu;

	private LevelManager theLevelManager;

	void Start () {
		//find the level manager
		theLevelManager = FindObjectOfType<LevelManager> ();
	}

	void Update () {
	
	}

	public void Restart(){
		//When restarting, reset the coins and lives
		PlayerPrefs.SetInt ("CoinCount", 0);
		PlayerPrefs.SetInt ("PlayerLives", theLevelManager.startingLives);

		//reload the active scene
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}

	public void LevelSelect(){
		//when going to level select, reset the coins and lives
		PlayerPrefs.SetInt ("CoinCount", 0);
		PlayerPrefs.SetInt ("PlayerLives", theLevelManager.startingLives);

		//load level select
		SceneManager.LoadScene (levelSelect);
	}

	public void QuitToMainMenu(){
		//load the main menu
		SceneManager.LoadScene (mainMenu);
	}
}
