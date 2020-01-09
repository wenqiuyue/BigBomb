using UnityEngine;
using System.Collections;
using UnityEngine.UI;//注意这个不能少
public class Show : MonoBehaviour {
	public GameObject btnObj ;//定义按钮
	public Sprite   expan;//定义待用的按钮图标
	public Sprite  back;
	Button btn;//声明按钮
	bool isshow=false ;//是否显示菜单
		// 初始化
	void Start () {
		
		btn = btnObj.GetComponent<Button>();
		btn.onClick.AddListener(delegate ()
			{
				isshow=!isshow;
	
				if (isshow)
				{
					//改变按钮图标
					btn.GetComponent<Image>().sprite=expan ;
				}
				else {
					btn.GetComponent<Image>().sprite=back;
				}
			});
	}

}