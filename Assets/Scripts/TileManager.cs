using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TileManager : MonoBehaviour {

	public GameObject[] tilePrefabs;

	private Transform playerTransform;
	private float spawnZ = -20.0f;
	private float tileLength = 20;
	private int tilesOnScreen = 9;
	private List<GameObject> activeTiles;
	private float safeZone = 60;

	// Use this for initialization
	void Start () {
		activeTiles = new List<GameObject>();
		playerTransform = GameObject.FindGameObjectWithTag ("Player").transform;
		for (int i = 0; i < tilesOnScreen; i++) {
			SpawnTile ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (playerTransform.position.z - safeZone > (spawnZ - tilesOnScreen * tileLength)) {
			SpawnTile ();
			DeleteTile ();
		}
	}

	private void SpawnTile(int prefabIndex = -1){
		int index = Random.Range(0, tilePrefabs.Length);
		GameObject go;
		go = Instantiate (tilePrefabs [index]) as GameObject;
		go.transform.position = Vector3.forward * spawnZ;
		spawnZ += tileLength;
		activeTiles.Add (go);
	}

	private void DeleteTile(int prefabIndex = -1){
		Destroy (activeTiles [0]);
		activeTiles.RemoveAt (0);
	}
		
}
