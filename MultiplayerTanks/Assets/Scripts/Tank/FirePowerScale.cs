using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePowerScale : MonoBehaviour {

	void Start () {
		GetComponent<UnityEngine.UI.Slider>().minValue = transform.parent.parent.gameObject.GetComponent<TankShoot> ().minfireForce;
		GetComponent<UnityEngine.UI.Slider>().maxValue = transform.parent.parent.gameObject.GetComponent<TankShoot> ().maxfireForce;
	}

	void Update () {
		float fireForce = transform.parent.parent.gameObject.GetComponent<TankShoot> ().fireForce;
		gameObject.GetComponent<UnityEngine.UI.Slider>().value = fireForce;
	}
}
