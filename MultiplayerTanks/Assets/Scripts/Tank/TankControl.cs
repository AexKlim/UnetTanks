using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TankControl : NetworkBehaviour {
	private Rigidbody rb;

	[SerializeField] private float speed;
	[SerializeField] private float rotSpeed;

	void Start () {
		rb = GetComponent<Rigidbody> ();
	}

	void Update () {
		if (!isLocalPlayer)
			return;
		Move ();
	}

	void Move(){
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");

		if (v != 0) {
			rb.MovePosition (rb.position + transform.forward * v * speed * Time.deltaTime);		
		}
		if (h != 0) {
			rb.MoveRotation (rb.rotation * Quaternion.Euler (0, h * rotSpeed * Time.deltaTime, 0));
		}
	}
}
