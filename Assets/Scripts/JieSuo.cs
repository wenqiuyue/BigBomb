using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JieSuo : MonoBehaviour {
	public GameObject _jiesuo;
	void Start()
	{
		
	}
	public void Show(int i)
	{  KeepData saveData = SaveData._Sav.GetSaveData ();
		if (saveData._IsUclockItem [i] == false) {
			_jiesuo.SetActive (true);
			GameObject.Find ("youling").GetComponent<EnemyAI> ().enabled = false;
			GameObject.Find ("player").GetComponent<Move2> ().enabled = false;
			GameObject.Find ("player").GetComponent<Player1> ().enabled = false;
		}
	}
	public void NotShow()
	{
		_jiesuo.SetActive (false);
		GameObject.Find ("youling").GetComponent<EnemyAI> ().enabled = true;
		GameObject.Find ("player").GetComponent<Move2> ().enabled = true;
		GameObject.Find ("player").GetComponent<Player1> ().enabled = true;
	}
	public void Show1(int i)
	{  KeepData saveData = SaveData._Sav.GetSaveData ();
		if(saveData._IsUclockItem[i]==false)
			_jiesuo.SetActive (true);
	}
	public void NotShow1()
	{
		_jiesuo.SetActive (false);
	
	}
		
}