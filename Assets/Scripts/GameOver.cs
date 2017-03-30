using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {



	//end the game
	public void gameOver() {
		SceneManager.LoadScene ("GameOver");

	}
}
