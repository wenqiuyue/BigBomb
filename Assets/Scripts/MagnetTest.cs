using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetTest : MonoBehaviour {
	//炸弹持续时间
	public float time=600f;
	//是否有磁铁功能
	private bool _isMagnet=false;
	//是否获取到磁铁
	private bool _isCiTie=false;
	// Update is called once per frame
	void Update () {
		if (_isCiTie) {
			time--;
			if (time <= 0) {
				_isMagnet = false;
				time = 0;
			}
		}
		if (_isMagnet) {
			Collider[] colliders = Physics.OverlapSphere(this.transform.position, 100);
				foreach (var item in colliders)
				{
					//如果是金币
				if (item.tag.Equals("jinbi")||
					item.tag.Equals("bombnum")||item.tag.Equals("shengming")
					||item.tag.Equals("jiasu")||item.tag.Equals("weili"))
					{
					//让金币的开始移动
					item.GetComponent<Jinbi>()._isCanMove = true;
					}
				}
		  }
	}
	/// <summary>
	/// 磁铁的持续时间
	/// </summary>

	public void OnTriggerEnter(Collider coll){
		if (coll.gameObject.tag == "Magnet") {
			_isCiTie = true;
			time = 600;
			_isMagnet = true;
			Destroy (coll.gameObject);
		}

	}
}
