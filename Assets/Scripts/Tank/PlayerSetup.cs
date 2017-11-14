using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {
	public GameObject gameCamera;
	public Behaviour[] componentsToDisable;
	public Color clientColor;

	public List<MeshRenderer> rends;

	void Start () {
		if (!isLocalPlayer) {
			for (int i = 0; i < componentsToDisable.Length; i++) {
				componentsToDisable [i].enabled = false;
			}
		} else {
			if (!isServer) {
				CmdSetColor (clientColor);
			}
			gameCamera = Camera.main.gameObject;
			if(gameCamera.GetComponent<CameraFollow>()!=null)
			gameCamera.GetComponent<CameraFollow> ().target = transform;
		}
	}

	[ClientRpc]
	void RpcSetColor(Color color){
		foreach (MeshRenderer renderer in rends) {
			renderer.material.color = color;
		}
	}
		
	[Command]
	void CmdSetColor(Color color){
		RpcSetColor (color);
	}
}
