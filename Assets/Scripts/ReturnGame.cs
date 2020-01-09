using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnGame : MonoBehaviour {
	public void EnterScene()
	{
		SceneManager.LoadScene ("game");
	}

}

