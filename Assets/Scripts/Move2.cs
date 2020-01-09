using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//玩家移动的方向
public enum Direction
{
	Left,
	Right,
	Up,
	Down
};
	
public class Move2: MonoBehaviour {
	public float speed = 100f;
	//玩家的移速
	[HideInInspector]
	public int timer;
	//道具持续时间，由Item控制
	[HideInInspector]
	public bool inStatus;
	[HideInInspector]
	public bool canSetTrap = true;
	[HideInInspector]
	public bool isMoving = false;
	//玩家是否能移动
	private Vector3 startPosition;
	//玩家准备移动时的起始点
	private Vector3 endPosition;
	//玩家要移动到的位置点
	private float t;
	private float factor;
	[HideInInspector]
	public Vector2 input;
	//每次移动的距离
	public float gridSize = 600f;
	private float x = 0;
	private float y = 0;
	public Sprite up;
	public Sprite down;
	public Sprite left;
	public Sprite right;


	/// <summary>
	/// 玩家的初始位置
	/// </summary>
	void Start(){
		this.transform.position = new Vector2 (90f, 390f);

	}
	/// <summary>
	/// 判断玩家能否移动
	/// </summary>
	public bool CanMove(Direction dir)
	{
		Vector3 direction = Vector3.zero;

		switch (dir) 
		{
		case Direction.Left:
			direction = Vector3.left;
			break;

		case Direction.Right:
			direction = Vector3.right;
			break;

		case Direction.Up:
			direction = Vector3.up;
			break;

		case Direction.Down:
			direction = Vector3.down;
			break;
		}
		//创建射线
		RaycastHit hit;
		//射线的初始位置，方向，存在hit，检测距离
		if (!Physics.Raycast (transform.position, direction, out hit, 60f))
        //如果射线没有碰撞，移动返回为真
			return true;
		//如果射线检测到为障碍物，移动返回为false
		if (hit.collider.tag == "Zhangaiwu"||hit.collider.tag=="baoxiang"
			||hit.collider.tag=="Bomb")
			
			return false;
		     return true;

	}
	/// <summary>
	/// 真正控制玩家移动的脚本
	/// </summary>
	public IEnumerator move(Transform transform)
	{
		isMoving = true;
		startPosition = transform.position;
		t = 0;
		//初始位置为新出入的位置值
		endPosition = new Vector3 (startPosition.x + System.Math.Sign (input.x) * gridSize,
			startPosition.y + System.Math.Sign (input.y) * gridSize,
			startPosition.z);
		factor = 2f;
		while (t < 1f) {
			t += Time.deltaTime * (speed / gridSize) * factor;
			transform.position = Vector3.Lerp(startPosition, endPosition, t);
			yield return null;
		}

		isMoving = false;
		yield return 0;
	}
	/// <summary>
	/// 判断玩家在上下左右四个方向是否移动
	/// </summary>
	public void FixedUpdate()
	{
		if (!isMoving) 
		{
			x = 0;
			y = 0;

			if (Input.GetKey(KeyCode.LeftArrow)) {
				this.GetComponent<SpriteRenderer> ().sprite = left;
				if (CanMove (Direction.Left))
					x = -60f;
			}
			else if (Input.GetKey(KeyCode.RightArrow)) {
				this.GetComponent<SpriteRenderer> ().sprite = right;
				if (CanMove (Direction.Right))
					x = 60f;
			}
			else if (Input.GetKey(KeyCode.UpArrow)) {
				this.GetComponent<SpriteRenderer> ().sprite = up;
				if (CanMove (Direction.Up))
					y = 60f;
			}
			else if (Input.GetKey(KeyCode.DownArrow)) {
				this.GetComponent<SpriteRenderer> ().sprite = down;
				if (CanMove (Direction.Down))
					y = -60f;
			}

			input = new Vector2 ((float)x, (float)y);
			if (input != Vector2.zero && speed != 0) {
				StartCoroutine (move (transform));
			}
		}
	}
	/// <summary>
	/// 碰撞到加速道具速度加1
	/// </summary>
	public void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "jiasu") {
			speed += 25f;
		}
		if (other.gameObject.tag == "jiansu") {
			speed -= 25f;
		}
	}
}
