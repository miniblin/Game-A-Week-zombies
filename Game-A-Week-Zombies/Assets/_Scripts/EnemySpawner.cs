using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public GameObject zombie;

	public float spawnRate = 15f;

	private float nextTimeToSpawn=0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(Time.time>= nextTimeToSpawn){
			Instantiate(zombie,new Vector3(7.5f,7.5f,0)+(Vector3)Random.insideUnitCircle.normalized * 10, Quaternion.identity);
			nextTimeToSpawn = Time.time + 1/spawnRate;
		}
		
	}
}
