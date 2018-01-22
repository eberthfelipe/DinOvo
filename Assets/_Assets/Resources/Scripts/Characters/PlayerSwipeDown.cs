using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// based on: https://unity3d.com/pt/learn/tutorials/projects/2d-roguelike-tutorial/adding-mobile-controls
public class PlayerSwipeDown : MonoBehaviour {

	public int moveValue;
	public int maxTouches;
	public float recoveryTime;

	private Material[] materials = new Material[5];
	private Renderer renderer;
	private Rigidbody eggRigidBody;
	private Vector3 touchOrigin = -Vector3.one;
	private AudioSource touchSound;
	private int countTouch;
	private GameScore gameScore;

	#if UNITY_STANDALONE || UNITY_EDITOR
	private bool isClicking = false;
	#endif

	// Use this for initialization
	void Start () {
		getMaterials ();
		countTouch = 0;
		renderer = GetComponent<Renderer> ();
		eggRigidBody = GetComponent<Rigidbody> ();
		touchSound = GetComponent<AudioSource> (); //huechcrunch
		gameScore = new GameScore();
		//every time defined in var "recoveryTime", the touch count is going to updated
		InvokeRepeating("subtractionTouch", recoveryTime, recoveryTime);
	}
	
	// Update is called once per frame
	void Update () {
		checkTouch ();
		CheckMovement ();
		updateUI ();
	}

	// function to check and validate touch inputs
	void CheckMovement(){
		int horizontal = 0;
		int vertical = 0;

		#if UNITY_STANDALONE || UNITY_EDITOR
		// input for PC

		if(Input.GetMouseButton(0) && !isClicking){
			touchOrigin = Input.mousePosition;
			isClicking = true;
		} else if(Input.GetMouseButtonUp(0)){
			Debug.Log ("click");
			Vector2 touchEnd = Input.mousePosition;
			// difference between two axis to check the direction of moviment
			float x = touchEnd.x - touchOrigin.x;
			float y = touchEnd.y - touchOrigin.y;
			touchOrigin.x = -1; // to revalidate the condition to enter

			if (Mathf.Abs (x) > Mathf.Abs (y)) {
				horizontal = x > 0 ? moveValue : -moveValue;
			} else {
				vertical = y > 0 ? moveValue : -moveValue;
			}
			isClicking = false;
		}

		#elif UNITY_ANDROID
			
		if(Input.touchCount > 0){
			Debug.Log ("get swipe");
			Touch myTouch = Input.touches[0];

			if (myTouch.phase == TouchPhase.Began) {
				touchOrigin = myTouch.position;
			} else if (myTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0) {
				Vector2 touchEnd = myTouch.position;
				// difference between two axis to check the direction of moviment
				float x = touchEnd.x - touchOrigin.x;
				float y = touchEnd.y - touchOrigin.y;
				touchOrigin.x = -1; // to revalidate the condition to enter

				if (Mathf.Abs (x) > Mathf.Abs (y)) {
					horizontal = x > 0 ? moveValue : -moveValue;
				} else {
					vertical = y > 0 ? moveValue : -moveValue;
				}
					
			}
		} 
		#endif

		if (horizontal != 0 || vertical != 0) {
			MovePlayer (horizontal, vertical);
		}

	}

	void MovePlayer(int horizontal, int vertical){
		touchSound.Play ();
		eggRigidBody.AddForce (new Vector3 (horizontal,vertical,0), ForceMode.Impulse);
		countTouch++;
//		gameObject.transform.position = 
//			new Vector3 (gameObject.transform.position.x
//				+ horizontal, gameObject.transform.position.y
//				+ vertical, transform.position.z);
		Debug.Log ("countTouch:"+countTouch);
	}

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == GameScore.TAG_SCORE) {
			Debug.Log (GameScore.TAG_SCORE);
			Destroy (col.gameObject);
			scorePoint ();
		}
	}

	void scorePoint(){
		gameScore.setScore ();
		Debug.Log ("Score:"+gameScore.getScore());
		GameController.setScore (gameScore.getScore ().ToString());
	}

	//if countTouch > maxTouches the game is going to restart
	void checkTouch(){
		if (countTouch > maxTouches) {
			GameController.GameControllerRestart ();
		}
	}

	void subtractionTouch(){
		if (countTouch > 0 && countTouch <= maxTouches) {
			countTouch--;
		}
		Debug.Log ("subtractionTouch:"+countTouch);
	}

	void updateUI(){
		switch (countTouch) {
		case 0:
			renderer.sharedMaterial = materials[0];
			break;
		case 1:
			renderer.sharedMaterial =  materials[1];
			break;
		case 2:
			renderer.sharedMaterial =  materials[2];
			break;
		case 3:
			renderer.sharedMaterial =  materials[3];
			break;
		case 4:
			renderer.sharedMaterial =  materials[4];
			break;
		default:
			renderer.sharedMaterial =  materials[4];
			break;
		}
	}

	void getMaterials(){
		for (int i = 0; i < 5; i++) {
			switch (i) {
			case 0:
				materials [i] = (Material) Resources.Load ("Materials/New Material", typeof(Material));
				break;
			case 1:
				materials [i] = (Material) Resources.Load ("Materials/material_color_gray", typeof(Material));
				break;
			case 2:
				materials [i] = (Material) Resources.Load ("Materials/material_color_yellow", typeof(Material));
				break;
			case 3:
				materials [i] = (Material) Resources.Load ("Materials/material_color_red", typeof(Material));
				break;
			case 4:
				materials [i] = (Material) Resources.Load ("Materials/material_color_black", typeof(Material));
				break;
			}
		}
	}
}
