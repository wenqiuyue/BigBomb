using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stopjiaoben : MonoBehaviour {
	public GameObject obj;
	public void Show()
	{
		obj.SetActive (true);
		GameObject.Find ("youling").GetComponent<EnemyAI> ().enabled = false;
		GameObject.Find ("player").GetComponent<Move2> ().enabled = false;
		GameObject.Find ("player").GetComponent<Player1> ().enabled = false;

	}
	public void NotShow()
	{
		obj.SetActive (false);
		GameObject.Find ("youling").GetComponent<EnemyAI> ().enabled = true;
		GameObject.Find ("player").GetComponent<Move2> ().enabled = true;
		GameObject.Find ("player").GetComponent<Player1> ().enabled = true;
	}

}
