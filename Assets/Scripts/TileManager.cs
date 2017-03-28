using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TileManager : MonoBehaviour {

	//Options
	public GameObject[] tilePrefabs;
	public bool zeroY = true;
	public bool randomZ = false;
	public bool randomX = false;
	public bool randomYLevel = false;
	public bool timeDetermined = false;
	public int tilesOnScreen = 9;
	public float spawnZ = -20.0f;
	private int tileLength = 20;
	private float safeZone = 60;

	private Transform playerTransform;

	private List<GameObject> activeTiles;
	private List<float> levelOptions;

	private float countTime;

	// Use this for initialization
	void Start () {
		levelOptions = new List<float> ();
		levelOptions.Add (.5f);
		levelOptions.Add (5f);
		activeTiles = new List<GameObject>();
		playerTransform = GameObject.FindGameObjectWithTag ("Player").transform;
		for (int i = 0; i < tilesOnScreen; i++) {
			SpawnTile ();
		}
		countTime = GameObject.Find ("TimeManager").GetComponent<TimeManager> ().getTime ();

	}
	
	// Update is called once per frame
	void Update () {
		//Spawn the next
		if (playerTransform.position.z - safeZone > (spawnZ - tilesOnScreen * tileLength)) {
			SpawnTile ();
			DeleteTile ();
		}
	}

	private void SpawnTile(int prefabIndex = -1){
		// Get random gameobject
		int index;
		GameObject go;
		if (timeDetermined) {
			index = getTimeDeterminedTileIndex ();
		} else {
			index = Random.Range (0, tilePrefabs.Length);
		}


		//Create object
		go = Instantiate (tilePrefabs [index]) as GameObject;

		// Non-random defaults
		Vector3 addZ = Vector3.zero;
		float xScale = 1;
		float xPos = 0;

		// Random Z Position
		if (randomZ) {
			addZ.z = Random.Range (0f, tileLength);
		} 
		// Random X Position
		if (randomX) {
			xPos = Random.Range (-12f, 13f);
		}
			
		//Set position
		Vector3 temp = Vector3.forward * spawnZ + addZ;
		if (!zeroY) {
			temp.y = go.transform.position.y;
		}

		// Random Y Level
		if (randomYLevel) {
			int levelIndex = Random.Range (0,2);
			print (levelIndex);
			temp.y = levelOptions [levelIndex];
		}

		temp.x += xPos;
		go.transform.position = temp;

		//Set Scale
		temp = go.transform.localScale;
		temp.x *= xScale;
		go.transform.localScale = temp;

		//Adjust for next spawn
		spawnZ += tileLength;
		activeTiles.Add (go);
	}

	private void DeleteTile(int prefabIndex = -1){
		//Destroy the last tile
		Destroy (activeTiles [0]);
		activeTiles.RemoveAt (0);
	}

	private int getTimeDeterminedTileIndex(){
		countTime = GameObject.Find ("TimeManager").GetComponent<TimeManager> ().getTime ();
		print ("Getting Tile" + countTime);
		float changeTime = 15.00f;
		if (countTime < changeTime) {
			return 0;
		} else if (countTime < changeTime * 2) {
			return 1;
		} else if (countTime < changeTime * 3) {
			return 2;
		} else if (countTime < changeTime * 4) {
			return 3;
		} else if (countTime < changeTime * 5) {
			return 4;
		} else {
			return 5;
		}
	}
		
}
