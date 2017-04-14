using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelDoor : MonoBehaviour {

	public string levelToLoad;

	public bool unlocked;

	public Sprite doorBottomOpen;
	public Sprite doorTopOpen;
	public Sprite doorBottomClosed;
	public Sprite doorTopClosed;

	public SpriteRenderer doorTop;
	public SpriteRenderer doorBottom;


	void Start () {
		//open the first level at start of game
		PlayerPrefs.SetInt ("Level1", 1);

		if (PlayerPrefs.GetInt (levelToLoad) == 1) {
			//if the level is available, unlock it
			unlocked = true;
		} else {
			//if not, lock it
			unlocked = false;
		}

		if (unlocked) {
			//change the door sprite to unlocked
			doorTop.sprite = doorTopOpen;
			doorBottom.sprite = doorBottomOpen;
		} else {
			//change the door sprite to locked
			doorTop.sprite = doorTopClosed;
			doorBottom.sprite = doorBottomClosed;
		}
	}
	

	void Update () {
	
	}

	void OnTriggerStay2D(Collider2D other){
		if (other.tag == "Player") {
			if (Input.GetButtonDown ("Interact") && unlocked) {
				//if the player presses Interact on an unlocked door, load that level
				SceneManager.LoadScene (levelToLoad);
			}
		}
	}

}
