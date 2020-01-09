using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Sounds : MonoBehaviour {

	[SerializeField]
	Button music;
	[SerializeField]
	GameObject _music;
	bool isshow=false ;
	void Start () {
		music.onClick.AddListener(delegate ()
			{
				isshow=!isshow;

				if (isshow)
				{
					_music.GetComponent<AudioSource>().mute=true;
				}
				else {
					_music.GetComponent<AudioSource>().mute=false;
				}
			});
	}
	

}
