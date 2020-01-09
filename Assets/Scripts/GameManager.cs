using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    BoardManager boardScript;
	private int leve = 1;
	// Use this for initialization
	void Awake () {
		//boardScript = GetComponent<BoardManager> ();
		//InitGame ();
	}
	void InitGame(){
		//boardScript.SetUpScene (leve);
	}
}
