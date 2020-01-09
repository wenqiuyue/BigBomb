using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UseSkill : MonoBehaviour {
	public Text _skillpoint;
	void Start()
	{   KeepData saveData = SaveData._Sav.GetSaveData ();
		_skillpoint.text = saveData._Skill.ToString ();
		SaveData._Sav.SaveGameData ();
	}
}
