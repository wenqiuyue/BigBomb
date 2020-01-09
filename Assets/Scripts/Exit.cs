using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//点击退出游戏按钮退出程序
public class Exit : MonoBehaviour {

	public void Quit()
	{   
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif

	}
}
