using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingScript : MonoBehaviour {

	float angle = 0;

	void Update () {
		//float rot = Mathf.Sin ( Time.deltaTime);
		angle = 180 + 90 * Mathf.Sin (Time.time * 1.5f);
		transform.rotation = Quaternion.Euler( 0, 0, angle);
		//transform.eulerAngles = Vector3(0, 0, 90 * Mathf.Sin(Time.deltaTime));
	}
}
	


