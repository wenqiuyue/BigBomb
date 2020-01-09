using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Money : MonoBehaviour {
	public Text _mymoney;
	void Start () {
		MyMoney ();
	
	}
	public void MyMoney()
	{
		KeepData saveData = SaveData._Sav.GetSaveData ();
		_mymoney.text = saveData._Gold.ToString ();
		SaveData._Sav.SaveGameData ();


	}


}
