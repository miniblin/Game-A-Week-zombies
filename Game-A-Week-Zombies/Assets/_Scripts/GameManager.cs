using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public LevelCreator levelCreatorScript;
	private int level=15;

	// Use this for initialization
	void Awake () {
		levelCreatorScript = GetComponent<LevelCreator>();
		InitGame();
	}
	
	// Update is called once per frame
	void InitGame () {
		levelCreatorScript.setupScene(level);
	}


}
