using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPoint : MonoBehaviour {
	public Button _skillbuy;
	public Text _skillpoint;
	public Text _mymoney;
	public Text _skillmoney;
    void Start()
	{   
		KeepData saveData = SaveData._Sav.GetSaveData ();
		int _skillmoney1 = int.Parse (_skillmoney.text);

			_skillbuy.onClick.AddListener (delegate {
			if (saveData._Gold >= _skillmoney1) {
				saveData._Gold = saveData._Gold - int.Parse (_skillmoney.text);
				saveData._Skill++;
				SaveData._Sav.SaveGameData ();
				_skillpoint.text = saveData._Skill.ToString ();
				_mymoney.text = saveData._Gold.ToString ();
			}
			});
		_mymoney.text = saveData._Gold.ToString ();
		_skillpoint.text = saveData._Skill.ToString ();

		
	}

}
