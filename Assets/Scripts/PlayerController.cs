using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	//Time
	public TimeManager timeManager;

	//GAME OVER UI scence
	public GameObject gameOverUI;

	// Settings
	public int startingHealth;
	public float startSpeed;
	private float gravity;


	//Components
	private Rigidbody rb;
	private CapsuleCollider playerCollider;
	private Animator anim;
	private Text healthText;


	//Moving Variables
	private Vector3 moveVector;
	private float verticalVelocity;
	private float currentSpeed;
	private float increaseAmount;

	//State
	private bool grounded;
	private float lastZ;
	private bool hurting;
	private bool sliding;
	private int currentHealth;
	private int hurtAmount;
	private float hurtInterval;
	private float timeToHurt;
	public static bool gameOver;

	//Animator
	public PlayerAnimator playerAnimator;

	//display texts
	public Text countText;
	private float score;

	//audio 
	public AudioSource coinSound;


	// Use this for initialization
	void Start () {
		//add score counter
		score = 0;
		countScore ();

		rb = GetComponent<Rigidbody> ();
		anim = GetComponent<Animator> ();
		playerCollider = GetComponent<CapsuleCollider> ();
		grounded = true;
		lastZ = 1000;
		currentSpeed = startSpeed;
		setIncreaseAmount();
		hurting = false;
		healthText = GameObject.Find ("Canvas/Health").GetComponent<Text> ();
		hurtAmount = 0;
		currentHealth = startingHealth;
		gameOver = false;

		//add sound effects for coins
		coinSound = (AudioSource) gameObject.AddComponent <AudioSource>();
		AudioClip coinClip;
		coinClip = (AudioClip)Resources.Load ("coin-drop-1");
		coinSound.clip = coinClip;
		coinSound.loop = false;
	}

	// Update is called once per frame
	void FixedUpdate(){
		
		if ((lastZ == rb.position.z) || hurting) {
			currentSpeed -= currentSpeed / 2;
		}
			
		lastZ = rb.position.z;

	}

	void Update () {

		if (transform.position.y < -10f) {
			gameOver = true;
		}

		if (gameOver) {
			// GAME OVER ACTIONS // 
			GameObject camera = GameObject.Find("Player/Main Camera");
			//camera.transform.position = new Vector3 (camera.transform.position.x, 2, camera.transform.position.z - .02f);
			//camera.transform.rotation = new Quaternion (10f, 180f, 0f, 0f);
			rb.velocity = Vector3.zero;

			anim.Play ("LOSE00");

			//when game end go to scence "GameOver"
			//SceneManager.LoadScene ("GameOver");
			this.enabled = false;
			this.timeManager.enabled = false;
			this.playerAnimator.enabled = false;
			//active the gameover ui
			this.gameOverUI.SetActive(true);

			// GAME OVER ACTIONS OVER //
		} else {

			if (hurting) {
				hurtPlayer ();
			}

			setHealthText ();
			setIncreaseAmount ();

			if (Input.GetKey ("space")) {
				gravity = .075f;
			} else {
				gravity = .4f;
			}

			verticalVelocity = rb.velocity.y;

			if (sliding && !(anim.GetCurrentAnimatorStateInfo (0).IsName ("SLIDE00"))) {
				sliding = false;
				setNormalCollider ();
			}

			if (grounded) {
				//Jump
				if (Input.GetKeyDown ("space")) {
					rb.AddForce (new Vector3 (0, 65f, 0), ForceMode.Impulse);
					grounded = false;
				}

				// Slide
				if (Input.GetKeyDown (KeyCode.DownArrow) || Input.GetKeyDown ("s")) {
					Vector3 temp = new Vector3 (0, .4f, 0);
					setSlidingCollider ();
					playerCollider.center = temp;
					playerCollider.direction = 2;
					sliding = true;
				}

			} else {
				verticalVelocity -= gravity;
			}
				
			currentSpeed += increaseAmount;

			// X - Left Right
			moveVector.x = Input.GetAxisRaw ("Horizontal") * 12;
			// Y - Up Down
			moveVector.y = verticalVelocity;
			// Z - Forward Backward
			moveVector.z = currentSpeed;

			rb.velocity = moveVector;
		}
	}

	void OnCollisionExit(Collision other) {
		if (other.collider.gameObject.tag == "Obstacle") { 
			hurting = false;
		}
	}

	void OnCollisionEnter(Collision other) {
		if (other.collider.gameObject.tag == "Obstacle") { 
			grounded = true;
			hurting = true;
			hurtAmount = other.collider.gameObject.GetComponent<ObstacleController> ().hurtAmount;
			hurtInterval = other.collider.gameObject.GetComponent<ObstacleController> ().hurtInterval;
			timeToHurt = Time.time;
		}
		if (other.collider.gameObject.tag == "Ground") {
			grounded = true;
		}
	}

	private void setIncreaseAmount(){
		if (currentSpeed < 10.0f) {
			increaseAmount = .2f;
		} else if (currentSpeed < 15.0f) {
			increaseAmount = .10f;
		} else if (currentSpeed < 30.0f) {
			increaseAmount = .05f;
		} else {
			increaseAmount = .01f;
		}
	}

	private void setHealthText(){
		healthText.text = "Health:";
		for(int i = 0; i < currentHealth; i++)
		{
			healthText.text += "♥";
		}
	}

	private void setSlidingCollider(){
		Vector3 temp = new Vector3 (0, .4f, 0);
		playerCollider.center = temp;
		playerCollider.direction = 2;
	}

	private void setNormalCollider(){
		Vector3 temp = new Vector3 (0, 1f, 0);
		playerCollider.center = temp;
		playerCollider.direction = 1;
	}

	public void hurtPlayer(){
		if (Time.time >= timeToHurt) {
			currentHealth -= hurtAmount;
			timeToHurt += hurtInterval;
			if (currentHealth <= 0) {
				gameOver = true;
			}
		}

	}

	//When hit coin score plus one
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Coin") {
			other.gameObject.SetActive (false);
			score += 1;
			countScore ();
			//play pickup coin sound
			coinSound.Play();
		} else {

		}
	}

	//count the score for player
	void countScore() {
		countText.text = "Score: " + score.ToString();

	}

}
