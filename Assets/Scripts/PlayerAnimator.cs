using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {

	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown (KeyCode.Space)){

			anim.Play ("JUMP00B",-1, 0f);
			print ("SPACE PRESSED!");

		}
		if (Input.GetKeyDown (KeyCode.LeftArrow) || Input.GetKeyDown ("a")) {
			anim.Play ("RUN00_L", -1, 0f);
			print ("pressed left");

		}

		if (Input.GetKeyDown (KeyCode.RightArrow) || Input.GetKeyDown ("d")){
			anim.Play ("RUN00_R", -1, 0f);
		} 

		if (Input.GetKeyDown (KeyCode.DownArrow) || Input.GetKeyDown ("s")) {
			anim.Play("SLIDE00", -1, 0f);
		}
	}
}
