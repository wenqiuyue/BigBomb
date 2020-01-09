using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Pic : MonoBehaviour {
	[SerializeField]
	Button _use;

	[SerializeField]
	int _id;
	[SerializeField]
	Text shuoming;
	public Image pic;
	public Text Shuoming{get{ return shuoming; } set{ shuoming = value; }}
	public Button _Use{get{ return _use; } set{ _use = value; }}
	public int _Id{get{ return _id; } set{ _id = value; }}
	public List<Button> _Skill;
	public List<Sprite> _pic;
	public List<string> _shuoming;
}
