using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	private readonly string TAG_RESTART = "restart";
	private int currentScene;

	// Use this for initialization
	void Start () {
		currentScene = SceneManager.GetActiveScene ().buildIndex;
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == TAG_RESTART) {
			Debug.Log (TAG_RESTART);
			SceneManager.LoadScene(currentScene);
		}
	}
}
