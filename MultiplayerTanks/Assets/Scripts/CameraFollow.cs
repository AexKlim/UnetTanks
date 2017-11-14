using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	public Vector3 offset; 
	public float maxX, maxZ;
	public float damping;
	void Start () {
		
	}

	void Update () {
		if (target != null) {
			transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * damping);
			if (transform.position.x > maxX)
				transform.position = new Vector3 (maxX, transform.position.y, transform.position.z);
			if(transform.position.x < -maxX)
				transform.position = new Vector3 (-maxX, transform.position.y, transform.position.z);
			if(transform.position.z > maxZ)
				transform.position = new Vector3 (transform.position.x, transform.position.y, maxZ);
			if(transform.position.z < -maxZ)
				transform.position = new Vector3 (transform.position.x, transform.position.y, -maxZ);
		}
	}
}
