using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Show1 : MonoBehaviour {
	public GameObject obj;
	public void Show()
	{
		obj.SetActive (true);

	}
	public void NotShow()
	{
		obj.SetActive (false);
	}

}
