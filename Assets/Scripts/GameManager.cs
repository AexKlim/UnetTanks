using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour {
	public Text player1Text;
	public Text player2Text;
	public int player1Score;
	public int player2Score;

	void Start () {
		
	}

	void Update () {
		
	}
		
	[ClientRpc]
	public void RpcChangeScore(bool player1){
		if (player1) {
			player1Score++;
			player1Text.text = player1Score.ToString ();
		} else {
			player2Score++;
			player2Text.text = player2Score.ToString (); 
		}
	}
}
