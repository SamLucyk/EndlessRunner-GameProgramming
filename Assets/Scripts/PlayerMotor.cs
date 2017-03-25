using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour {

	private CharacterController controller;
	public float speed;
	private Vector3 moveVector;
	private float verticalVelocity;
	private float gravity = .6f;

	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () {

		moveVector = Vector3.zero;

		if (Input.GetKey ("space")) {
			gravity = .175f;
		} else {
			gravity = .4f;
		}

		if (controller.isGrounded) {
			if (Input.GetKeyDown("space")) {
				verticalVelocity = 7.0f;
			}
		} else {
			verticalVelocity -= gravity;
		}

		// X - Left Right
		moveVector.x = Input.GetAxisRaw("Horizontal") * 2;
		// Y - Up Down
		moveVector.y = verticalVelocity;
		// Z - Forward Backward
		moveVector.z = speed;

		controller.Move (moveVector * speed * Time.deltaTime);
	}
}
