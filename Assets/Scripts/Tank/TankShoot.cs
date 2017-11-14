using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class TankShoot : NetworkBehaviour{
	[SerializeField] private GameObject bulletPrefab;
	[SerializeField] private Slider fireSlider;
	[SerializeField] private Image reloadingImage;
	[SerializeField] private Transform firePos;
	public float maxfireForce;
	public float minfireForce;
	[SerializeField] private float timeMinToMaxFireForce;
	public float reloadingTime;
	public float reloadingTimer;
	public float fireForce;
	public bool isFiring;
	public bool reloaded;
	private float forceAddPerFrame;


	void Start () {
		forceAddPerFrame = (maxfireForce - minfireForce) / timeMinToMaxFireForce;
		reloadingTimer = reloadingTime;
		fireForce = minfireForce;
	}

	void Update () {
		if (!isLocalPlayer)
			return;

		if (reloadingTimer >= reloadingTime) {
			if (Input.GetKey (KeyCode.Space)) {
				if (fireForce < maxfireForce) {
					if (!fireSlider.gameObject.activeSelf)
						fireSlider.gameObject.SetActive (true);
					fireForce += forceAddPerFrame * Time.deltaTime;
				} else {
					Fire ();
				}
			}
			if (Input.GetKeyUp (KeyCode.Space)) {
				Fire ();
			}
		} else {
			reloadingTimer += Time.deltaTime;
		}
	}

	void Fire(){
		CmdSpawnBullet (fireForce, firePos.position, firePos.rotation);
		reloadingTimer = 0;
		fireForce = minfireForce;
		fireSlider.value = fireSlider.minValue;
		fireSlider.gameObject.SetActive (false);
	}

	[Command]
	void CmdSpawnBullet(float fireF, Vector3 pos, Quaternion rot){
		GameObject newBullet = Instantiate (bulletPrefab, pos, rot);
		newBullet.GetComponent<Rigidbody> ().AddForce (firePos.forward * fireF);
		NetworkServer.Spawn (newBullet);
	}
}