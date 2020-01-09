using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stop : MonoBehaviour {
	public GameObject _zanting;
	public void Show()
	{
		_zanting.SetActive (true);
	}
	public void NotShow()
	{
		_zanting.SetActive (false);
	}

}