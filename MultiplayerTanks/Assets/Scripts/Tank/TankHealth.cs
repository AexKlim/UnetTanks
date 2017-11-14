using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TankHealth: NetworkBehaviour {
	public GameManager gameManager;
	public GameObject tankExplosion;
	public GameObject bustedTank;
	public GameObject UIBar;
	public float maxHP;

	private Transform[] spawnPoints;

	[SyncVar] public float currentHP;

	void Start () {
		gameManager = GameObject.Find ("NetworkManager").GetComponent<GameManager> ();
		NetworkStartPosition[] startPositions = FindObjectsOfType<NetworkStartPosition>();
		spawnPoints = new Transform[startPositions.Length];
		for (int i = 0; i < startPositions.Length; i++) {
			spawnPoints[i] = startPositions[i].transform;
		}
		currentHP = maxHP;
	}

	void Update () {
		CalculateUI ();
	}
		
	public void TakeDmg(float dmg){
		currentHP -= dmg;
		if (currentHP <= 0)
			OnDeath ();
	}
		
	void OnDeath(){
		if (isServer) {
			Explosion ();
			GameObject newBustedTank = Instantiate (bustedTank, transform.position, Quaternion.Euler (0, Random.Range (0, 360f), 0));
			NetworkServer.Spawn (newBustedTank);
			transform.position = spawnPoints [Random.Range (0, spawnPoints.Length)].position;
			transform.rotation = spawnPoints [Random.Range (0, spawnPoints.Length)].rotation;
			currentHP = maxHP;
			if (isLocalPlayer)
				gameManager.RpcChangeScore (true);
			else
				gameManager.RpcChangeScore (false);
		}
	}

	public void CalculateUI(){
		UIBar.GetComponent<UnityEngine.UI.Image>().fillAmount = currentHP / maxHP;
	}

	void Explosion(){
		GameObject newTankExplosion = Instantiate (tankExplosion, transform.position, Quaternion.Euler(new Vector3(-90f, 0, 0)));
		Destroy (newTankExplosion, newTankExplosion.GetComponent<ParticleSystem>().main.duration);
		NetworkServer.Spawn (newTankExplosion);
	}
}
