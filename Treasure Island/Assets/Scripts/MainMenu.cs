using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public string firstLevel;
	public string LevelSelect;

	public string[] levelNames;

	public int startingLives;


	void Start () {
	
	}
	

	void Update () {
	
	}

	public void NewGame(){
		//Load the first level on new game
		SceneManager.LoadScene (firstLevel);

		for (int i = 0; i < levelNames.Length; i++) {
			//lock the levels
			PlayerPrefs.SetInt (levelNames [i], 0);
		}

		//reset coins and lives
		PlayerPrefs.SetInt ("CoinCount", 0);
		PlayerPrefs.SetInt ("PlayerLives", startingLives);
	}
	public void Continue(){
		//load the level select
		SceneManager.LoadScene (LevelSelect);
	}
	public void QuitGame(){
		//quit the game
		Application.Quit ();
	}
}
