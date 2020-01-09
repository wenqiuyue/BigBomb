using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class MouseChange : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler{
	/// <summary>
	/// 目标按钮
	/// </summary>
	[SerializeField]
	Button _target;
	/// <summary>
	/// 按钮改变前的图片	/// </summary>
	[SerializeField]
	Sprite _original;
	/// <summary>
	/// 当鼠标移动上去改变后按钮图片	/// </summary>
	[SerializeField]
	Sprite _afterchange;
	void Start () {
		Oringinal ();

	}
	public void Oringinal()
	{
		_target.image.sprite = _original;
		_target.image.SetNativeSize ();
	}
	public void Afterchange()
	{
		_target.image.sprite = _afterchange;
		_target.image.SetNativeSize ();

	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		Afterchange ();
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		Oringinal ();
	}
}
