using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bullet : NetworkBehaviour {
	public GameObject bulletExplosion;
	public GameObject crater;
	bool isLive = true;
	public float explosionRadius;
	public float minDmg;
	public float maxDmg;
	float timer;
	[SerializeField] float lifeTime = 5f;
	Rigidbody rb;

	void Start(){
		rb = GetComponent<Rigidbody> ();
	}
		
	[ServerCallback]
	void Update () {
		transform.rotation = Quaternion.LookRotation (rb.velocity);
		timer += Time.deltaTime;
		if (timer >= lifeTime)
			NetworkServer.Destroy (gameObject);
	}
		
	[ServerCallback]
	void OnTriggerEnter (Collider other){
		if (!isLive)
			return;
		isLive = false;

		if (other.gameObject.tag == "Ground") {
			SpawnCrater (new Vector3(transform.position.x,0,transform.position.z));
		}
		Explosion();
		Collider[] colls = Physics.OverlapSphere (transform.position, explosionRadius);
		foreach (Collider coll in colls) {
			if (coll.gameObject.tag == "Player") {
				Debug.Log (coll.gameObject.name);
				float dmg = CalculateDmg (coll.gameObject.transform.position);
				coll.gameObject.GetComponent<TankHealth> ().TakeDmg (dmg);
			}
		}
	}

	void Explosion(){
		GameObject newBulletExplosion = Instantiate (bulletExplosion, transform.position, Quaternion.Euler(new Vector3(-90f,0,0)));
		Destroy (newBulletExplosion, newBulletExplosion.GetComponent<ParticleSystem>().main.duration);
		NetworkServer.Spawn (newBulletExplosion);
		Destroy (gameObject);
	}

	void SpawnCrater(Vector3 pos){
		GameObject newCrater = Instantiate (crater, pos, Quaternion.Euler(0,Random.Range(0,360f),0));
		NetworkServer.Spawn(newCrater);
	}

	float CalculateDmg(Vector3 target){
		float dist = Vector3.Distance (target, transform.position);
		return ((explosionRadius - dist) / explosionRadius) * (maxDmg - minDmg) + minDmg;
	}
}
