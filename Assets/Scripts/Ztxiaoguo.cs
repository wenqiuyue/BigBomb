using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Ztxiaoguo : MonoBehaviour {

	public Button _stop;
	public Button _returngame;
	public Sprite stop;
	public Sprite returngame;
	public Button _returngame2;
	void Start () {


		_stop.onClick.AddListener(delegate ()
			{
				_stop.GetComponent<Image>().sprite=stop ;
			});

		_returngame.onClick.AddListener(delegate ()
			{
				_stop.GetComponent<Image>().sprite=returngame ;
			});
		_returngame2.onClick.AddListener(delegate ()
			{
				_stop.GetComponent<Image>().sprite=returngame ;
			});
	}
}
