using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BackToMenu : MonoBehaviour {

	//back to the mainmenu scence
	public void backToMenu() {
		SceneManager.LoadScene ("MainMenu");

	}
}
