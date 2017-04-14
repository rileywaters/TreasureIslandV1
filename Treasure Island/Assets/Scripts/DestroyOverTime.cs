using UnityEngine;
using System.Collections;

public class DestroyOverTime : MonoBehaviour {

	public float lifeTime;


	void Start () {
	
	}

	void Update () {
		//give a gameobject a lifetime that decreases as time goes on
		lifeTime = lifeTime - Time.deltaTime;

		if (lifeTime <= 0f) {
			//destroy the object when its lifetime hits 0
			Destroy (gameObject);
		}
	}
}
