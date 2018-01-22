using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	private static readonly string TAG_RESTART = "restart";
	private static int currentScene;
	private static HUDController hudController;

	public Text HudObject;

	// Use this for initialization
	void Start () {
		currentScene = SceneManager.GetActiveScene ().buildIndex;
		hudController = new HUDController(HudObject);
	}
	
	// Update is called once per frame
	void Update () {
	}

	public static void setScore(string score){
		hudController.setScore (score);
	}

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == TAG_RESTART) {
			GameControllerRestart ();
		}
	}

	public static void GameControllerRestart(){
		Debug.Log (TAG_RESTART);
		SceneManager.LoadScene(currentScene);
	}
}
