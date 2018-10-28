using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

	private Transform target;
	public float cameraSpeed = 50f;
	
	// Use this for initialization
	void Start ()
	{
		target = GameObject.FindWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update ()
	{

		float xJoyStixDirection = Input.GetAxisRaw("Horizontal");
		float yJoyStixDirection = Input.GetAxisRaw("Vertical");
		
		Vector3 cameraOffset = new Vector3(xJoyStixDirection/10f, yJoyStixDirection/10f, -10);
		
		Vector3 newpos = this.target.position + cameraOffset;
		
		newpos = Vector3.Lerp(this.transform.position, newpos, Time.deltaTime*this.cameraSpeed);

		this.transform.position = new Vector3(newpos.x, newpos.y, -10);
	}
	
	
	
}