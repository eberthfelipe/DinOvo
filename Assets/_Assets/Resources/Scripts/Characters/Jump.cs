using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {
	private readonly string TAG_JUMP = "jump";
	private Rigidbody rigidBody;
	private AudioSource[] jumpSound;

	public int jump_force;
	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody>();	
		jumpSound = GetComponents<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == TAG_JUMP) {
			Debug.Log (TAG_JUMP);

			jumpSound[1].Play (); //retro-game-heal-sound

			//rigidBody.velocity = transform.TransformDirection()
			rigidBody.AddForce (new Vector3 (0,jump_force,0), ForceMode.VelocityChange);
		}
	}
}
