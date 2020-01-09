using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager1 : MonoBehaviour {

	public DataSave1 _save;
	public KeepData _keep;
	static GameManager1 Instance;
	void Awake()  
	{  
		if (Instance == null)  
		{  
			DontDestroyOnLoad(gameObject);  
			Instance = this;  
		}  
		else if (Instance != this)  
		{  
			Destroy(gameObject);  
		}    
	
	} 
	void Update()
	{  
		if(Input.GetKey(KeyCode.Escape))
			{
			#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
			#else
			Application.Quit();
			#endif
			}
		//MoneyUpdate ();
	}

	/*void MoneyUpdate()
	{   
		SaveData save = GameObject.FindObjectOfType<SaveData> ();
		_keep = save.GetSaveData();
	}
    */

}
