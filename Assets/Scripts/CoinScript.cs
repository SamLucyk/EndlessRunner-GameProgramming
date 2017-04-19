using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour {
	
	void Update () {
		transform.Rotate(0, 140 * Time.deltaTime, 0);
	}

	void OnCollisionEnter(Collision collision){
		Destroy(this.gameObject);
	}
}
