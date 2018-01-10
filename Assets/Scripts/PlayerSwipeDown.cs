using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// based on: https://unity3d.com/pt/learn/tutorials/projects/2d-roguelike-tutorial/adding-mobile-controls
public class PlayerSwipeDown : MonoBehaviour {

	public int moveValue;

	private Rigidbody eggRigidBody;
	private Vector3 touchOrigin = -Vector3.one;

	#if UNITY_STANDALONE || UNITY_EDITOR
	private bool isClicking = false;
	#endif

	// Use this for initialization
	void Start () {
		eggRigidBody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		CheckMovement ();

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
		eggRigidBody.AddForce (new Vector3 (horizontal,vertical,0), ForceMode.Impulse);

//		gameObject.transform.position = 
//			new Vector3 (gameObject.transform.position.x
//				+ horizontal, gameObject.transform.position.y
//				+ vertical, transform.position.z);
		Debug.Log ("x:"+transform.position.x);
		Debug.Log ("y:"+transform.position.y);
//		Debug.Log ("z:"+transform.position.z);
	}


}
