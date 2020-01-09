using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Returned: MonoBehaviour {
	public void EnterScene()
	{
		SceneManager.LoadScene ("start");
	}

}
