using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Yingyin : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler {

	[SerializeField]
	Image _target;
	[SerializeField]
	Sprite _yingyin;
	[SerializeField]
	Sprite _changeyingyin;
	void Start () {
		_target.sprite = _yingyin;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		_target.sprite = _changeyingyin;
		_target.SetNativeSize ();
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		_target.sprite = _yingyin;
		_target.SetNativeSize ();
	}
}
