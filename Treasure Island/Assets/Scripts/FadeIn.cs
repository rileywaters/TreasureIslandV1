using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour {

	public float fadeTime;
	private Image blackScreen;


	void Start () {
		//find the blackscreen
		blackScreen = GetComponent<Image> ();
	}
	

	void Update () {
		//fade the blackscreen the set amount of time
		blackScreen.CrossFadeAlpha (0f, fadeTime, false);

		if (blackScreen.color.a == 0) {
			//when the blackscreen has faded, deactivated it
			gameObject.SetActive (false);
		}
	}
}
