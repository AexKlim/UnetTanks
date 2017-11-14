using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadingScale : MonoBehaviour {
	private float reloadingTime;

	void Start(){
		reloadingTime = transform.parent.parent.gameObject.GetComponent<TankShoot> ().reloadingTime;
	}

	void Update () {
		float reloadingTimer = transform.parent.parent.gameObject.GetComponent<TankShoot> ().reloadingTimer;
		gameObject.GetComponent<UnityEngine.UI.Image>().fillAmount = reloadingTimer / reloadingTime;
	}
}
