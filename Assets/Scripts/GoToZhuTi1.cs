using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToZhuTi1 : MonoBehaviour {
	public void EnterScene(int i)
	{   KeepData saveData = SaveData._Sav.GetSaveData ();
		if (saveData._GuanQiaData[i]) {
			SceneManager.LoadScene ("zhuti1");
		}
	}

}

