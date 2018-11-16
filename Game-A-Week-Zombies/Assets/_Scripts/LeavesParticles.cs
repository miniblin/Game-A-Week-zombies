using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeavesParticles : MonoBehaviour {

	// Use this for initialization

	public GameObject leaves;
	private GameObject leavesInstance;
	void Start () {
		leavesInstance = Instantiate(leaves,transform.position, Quaternion.identity);
		leavesInstance.transform.SetParent(this.transform);
		leavesInstance.SetActive(false);
	}
	
	/// <summary>
	/// Sent when another object enters a trigger collider attached to this
	/// object (2D physics only).
	/// </summary>
	/// <param name="other">The other Collider2D involved in this collision.</param>
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Wall" && leavesInstance.activeSelf ==false){
			leavesInstance.SetActive(true);
		}
	}

	/// <summary>
	/// Sent when another object leaves a trigger collider attached to
	/// this object (2D physics only).
	/// </summary>
	/// <param name="other">The other Collider2D involved in this collision.</param>
	void OnTriggerExit2D(Collider2D other)
	{
		if(other.tag=="Wall"){
			leavesInstance.SetActive(false);
			//deactivate leaves
		}
	}
	// Update is called once per frame
	void Update () {
		
	}
}
