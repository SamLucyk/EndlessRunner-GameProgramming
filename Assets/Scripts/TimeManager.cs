using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class TimeManager : MonoBehaviour {
	//count the time
	private float countTime = 0.00f;

	//countTime text
	public Text countTimeText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		countTime += Time.deltaTime;
		countTime = Mathf.Clamp (countTime, 0f, Mathf.Infinity);
		countTimeText.text = "Time: " +string.Format("{0:00.00}", countTime) + "s";
		 
	}

	public float getTime(){
		return countTime;
	}
}
