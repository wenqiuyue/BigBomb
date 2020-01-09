using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fail : MonoBehaviour {
	public GameObject _fail;
	public void Show()
	{
		_fail.SetActive (true);
	}
	public void NotShow()
	{
		_fail.SetActive (false);
	}

}
