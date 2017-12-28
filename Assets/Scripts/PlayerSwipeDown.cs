using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// based on: https://unity3d.com/pt/learn/tutorials/projects/2d-roguelike-tutorial/adding-mobile-controls
public class PlayerSwipeDown : MonoBehaviour {
	private Vector2 touchOrigin = -Vector2.one;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		CheckSwipe ();

	}

	// function to check and validate touch inputs
	void CheckSwipe(){
		//#if UNITY_STANDALONE
		// input for PC

		//#else

		int horizontal = 0;
		int vertical = 0;
		if(Input.touchCount > 0){
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
					horizontal = x > 0 ? 1 : -1;
				} else {
					vertical = y > 0 ? 1 : -1;
				}
					
			}
		}

		//#endif
		if (horizontal != 0 || vertical != 0) {
			
		}

	}
}
