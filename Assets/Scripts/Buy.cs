using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buy : MonoBehaviour {
	public GameObject _jiesuo;
	public Text _mymoney;
	public Text _jsMoney;
	void Start()
	{  
	}
	public void Show(int i)
	{   KeepData saveData = SaveData._Sav.GetSaveData ();
	
		int _jsmoney1 = int.Parse (_jsMoney.text);
		if ( _jsmoney1>saveData._Gold) {
			_jiesuo.SetActive (false);
		}
		if (saveData._IsUclockItem [i] == false) {
			if (_jsmoney1 <=saveData._Gold) {
				_jiesuo.SetActive (true);
				saveData._Gold= saveData._Gold - _jsmoney1;
				SaveData._Sav.SaveGameData ();
				_mymoney.text =saveData._Gold.ToString ();
			}
		}
		_mymoney.text =saveData._Gold.ToString ();
		SaveData._Sav.SaveGameData ();

	}


}
