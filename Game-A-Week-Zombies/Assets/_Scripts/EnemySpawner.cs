using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public GameObject zombie;

	public float spawnRate = 15f;

	private float nextTimeToSpawn=0f;

	private GameObject player;
	// Use this for initialization
	void Start () {
		player =GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {

		if(Time.time>= nextTimeToSpawn){
			Instantiate(zombie,new Vector3(player.transform.position.x,player.transform.position.y,0)+(Vector3)Random.insideUnitCircle.normalized * 10, Quaternion.identity);
			nextTimeToSpawn = Time.time + 1/spawnRate;
		}
		
	}
}
