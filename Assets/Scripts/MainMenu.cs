using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
	public string levelToLoad = "MainScene";


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//play the game
	public void Play() {
		SceneManager.LoadScene (levelToLoad);

	}

	//quit the game
	public void Quit ()
	{
		Debug.Log ("Quit the game!");
		Application.Quit ();
	}
}
