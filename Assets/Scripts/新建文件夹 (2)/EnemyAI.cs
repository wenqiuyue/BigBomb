using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI: MonoBehaviour, INowPutBombNumMinuse {
	/// <summary>
	/// 开启列表
	/// </summary>
	List<GridDate> _openList=new List<GridDate>();

	/// <summary>
	/// 关闭列表
	/// </summary>
	List<GridDate> _closeList=new List<GridDate>();

	//将格子路径反向存入列表
	List<GridDate> path = new List<GridDate>();

	//危险区域列表
	List<GridDate> dangerousList=new List<GridDate>();

	//安全区域列表
	List<GridDate> safeList=new List<GridDate>();

	/// <summary>
	/// 记录危险区域地图
	/// </summary>
	int[,] _dangerousMap;

	//将敌人周围3个格子内的障碍物放入列表
	List<GridDate> obstacleList=new List<GridDate>();
	/// <summary>
	///当前地图信息 
	/// </summary>
	Map _map;
	Player1 _player;
	float _spd = 200f;
	//敌人初始位置
	//Grid _direnGrid=new Grid(_map.Rows,_map.Columns);

	/// <summary>
	/// 是否找到路径
	/// </summary>
	bool _isFindPath = false;
	/// <summary>
	/// 现在角色在路径的序号下标
	/// </summary>
	short _pathIndex = 0;
	/// <summary>
	/// 附近是否有炸弹
	/// </summary>
	bool _isBombnearby = false;
	/// <summary>
	/// 是否在炸弹伤害范围内
	/// </summary>
	bool _isHurt = false;
	/// <summary>
	/// 是否能安放炸弹
	/// </summary>
	bool _isputbomb = false;

	//炸弹预制
	public Bomb _bombPrefab;
	//玩家安放炸弹的初始威力
	public int _weiLi=2;
	//可以安放炸弹数量
	public  int bombnum=1; 
	/// <summary>
	/// 当前放置炸弹数
	/// </summary>
	public int _NowPutBombNum{ get{ return chushibombnum; } 
		set{ 
			chushibombnum = value; 
			if (chushibombnum < 0)
				chushibombnum = 0;
			//Debug.LogFormat ("bomb:{0} {1}", chushibombnum, bombnum);
		} }

	//比较
	public  int chushibombnum=0;

	/// <summary>
	/// 是否可以运行AIAction函数
	/// </summary>
	bool _isRunAIAcition = false;

	/// <summary>
	/// 是否寻找路径时检测炸弹
	/// </summary>
	bool _isFindPathCheckBomb = false;

	void Start () {
		//获取map脚本   
		_map = GameObject.FindObjectOfType<Map> ();
		_player = GameObject.FindObjectOfType<Player1> ();
		//this.transform.position = new Vector2 (90f, 390f);
		transform.position = _map.GetGridPos(5, 1);

		//path = FindingPath (new Grid(5, 1), new Grid(4, 6));
		//_isRunAIAcition = true;
		//_isFindPath = true;
		//PrintPath (path);
		//Debug.Log ( _map.GetGrid(transform.position));

		InvokeRepeating("IsRunAIAction", 0.2f, 0.2f);
	}

	/// <summary>
	/// 是否可以运行AIAction函数
	/// </summary>
	/// <returns><c>true</c> if this instance is run AI action; otherwise, <c>false</c>.</returns>
	void IsRunAIAction(){
		_isRunAIAcition = true;
		//Debug.LogFormat ("_isFindPath:{0}", _isFindPath);
		if (!_isFindPath)
			AiAction ();
	}

	void Update () {
		
		Move ();
		/*for(int mpIndex=0;mpIndex<path.Count-1;mpIndex++){

			Vector2 movepos = _map.GetGridPos (path[mpIndex]);
			//向上移

			if (path [mpIndex + 1].Rows - path [mpIndex].Rows < 0 
				&& path [mpIndex + 1].Columns-path [mpIndex].Columns==0) {
				//movepos.y += _spd * Time.deltaTime;
				Move(movepos.x,movepos.y+=_spd * Time.deltaTime);
				Debug.Log ("1");
			}

			//向下移
			if (path [mpIndex + 1].Rows - path [mpIndex].Rows > 0 
				&& path [mpIndex + 1].Columns-path [mpIndex].Columns==0) {
				//movepos.y -= _spd * Time.deltaTime;
				Move(movepos.x,movepos.y-=(_spd * Time.deltaTime));
				Debug.Log ("2");
			}
			//向左移
			if (path [mpIndex + 1].Rows - path [mpIndex].Rows == 0 
				&& path [mpIndex + 1].Columns-path [mpIndex].Columns<0) {
				//movepos.x -= _spd * Time.deltaTime;
				Move(movepos.x-(_spd * Time.deltaTime),movepos.y);
				Debug.Log ("3");
			}
			//向右移
			if (path [mpIndex + 1].Rows - path [mpIndex].Rows == 0 
				&& path [mpIndex + 1].Columns-path [mpIndex].Columns<0) {
				//movepos.x += _spd * Time.deltaTime;
				Move(movepos.x+=_spd * Time.deltaTime,movepos.y);
				Debug.Log ("4");
			}


			transform.position = movepos;
		}



	*/
	}
	/// <summary>
	///AI移动 
	/// </summary>
	public void Move(){
		if (!_isFindPath || path == null || path.Count == 0) 
			return;

		//Debug.LogFormat ("movePath:{0} {1}", _pathIndex, path.Count);
		// 如果移动到最后一个格子，清空路径
		if (_pathIndex >= path.Count) {
			_pathIndex = 0;
			_isFindPath = false;
			path.Clear ();

			if (_isRunAIAcition)
				AiAction ();
			Debug.Log ("清空路径");
			return;
		}

		// 获取当前目标格子
		GridDate target = path[_pathIndex];
		// 获取当前角色所在格子
		Grid now = _map.GetGrid(transform.position);
		if(_pathIndex > 0)
			now = path[_pathIndex - 1];

		// 获取目标格子坐标
		Vector2 targetPos = _map.GetGridPos(target);
		//Debug.LogFormat ("{0}->{1}", now, target);
		//Debug.LogFormat ("{0}->{1}", transform.position, targetPos);

		// 如果是初始移动，把玩家移到格子中间
		if (_pathIndex == 0) {
			// 如果就在当前格子，把角色移动到当前格子
			if (path.Count == 1) {
				// 如果y值相等，x方向移动
				if (Mathf.Approximately (targetPos.y, transform.position.y)) {
					// 如果x方向也相等，退出
					if (Mathf.Approximately (targetPos.x, transform.position.x)) {
						_pathIndex++;

						if (_isRunAIAcition)
							AiAction ();
						return;
					}
					// x不相等，往x方向移动
					if (targetPos.x > transform.position.x) {
					// 目标点在当前点右边，往右移动
						transform.Translate (_spd * Time.deltaTime, 0, 0);
						//Debug.LogFormat ("you {0} {1}", transform.position.x, targetPos.x);
						if (transform.position.x >= targetPos.x) {
							transform.position = new Vector2 (targetPos.x, transform.position.y);
							_pathIndex++;

							if (_isRunAIAcition)
								AiAction ();
						}
					} else {
					// 目标点在当前点左边，往左移动	
						transform.Translate (-_spd * Time.deltaTime, 0, 0);
						//Debug.LogFormat ("zuo {0} {1}", transform.position.x, targetPos.x);
						if (transform.position.x <= targetPos.x) {
							transform.position = new Vector2 (targetPos.x, transform.position.y);
							_pathIndex++;

							if (_isRunAIAcition)
								AiAction ();
						}
					}
				}
				if (transform.position.y > targetPos.y) {
					// 向xia移动
					transform.Translate (0, -_spd * Time.deltaTime, 0);
					if (transform.position.y <= targetPos.y) {
						transform.position = new Vector2 (transform.position.x, targetPos.y);
					}
				} else {
					// 向shang移动
					transform.Translate (0, _spd * Time.deltaTime, 0);
					if (transform.position.y >= targetPos.y) {
						transform.position = new Vector2 (transform.position.x, targetPos.y);
					}
				}
				// 如果y值不相等，y方向移动
				return;
			}
			GridDate next = path[_pathIndex+1];
			// 如果即将横向移动，竖向移到格子中间
			if (now.Rows == next.Rows) {
				if (transform.position.y > targetPos.y) {
					// 向xia移动
					transform.Translate (0, -_spd * Time.deltaTime, 0);
					//Debug.LogFormat ("xia {0} {1}", transform.position.y, targetPos.y);
					if (transform.position.y <= targetPos.y) {
						transform.position = new Vector2 (transform.position.x, targetPos.y);
						// 如果移动到目的地，准备移到下个格子
						_pathIndex++;
						//Debug.Log ("移动到目的地");

						if (_isRunAIAcition)
							AiAction ();
					}
				} else {
					// 向shang移动
					transform.Translate (0, _spd * Time.deltaTime, 0);
					//Debug.LogFormat ("shang {0} {1}", transform.position.y, targetPos.y);
					if (transform.position.y >= targetPos.y) {
						transform.position = new Vector2 (transform.position.x, targetPos.y);
						// 如果移动到目的地，准备移到下个格子
						_pathIndex++;

						if (_isRunAIAcition)
							AiAction ();
					}
				}
			} else {
			// 如果即将竖向移动，横向移到格子中间
				if (transform.position.x < targetPos.x) {
					// 向右移动
					transform.Translate (_spd * Time.deltaTime, 0, 0);
					//Debug.LogFormat ("you {0} {1}", transform.position.x, targetPos.x);
					if (transform.position.x >= targetPos.x) {
						transform.position = new Vector2 (targetPos.x, transform.position.y);
						// 如果移动到目的地，准备移到下个格子
						_pathIndex++;

						if (_isRunAIAcition)
							AiAction ();
					}
				} else {
					// 向左移动
					transform.Translate (-_spd * Time.deltaTime, 0, 0);
					//Debug.LogFormat ("zuo {0} {1}", transform.position.x, targetPos.x);
					if (transform.position.x <= targetPos.x) {
						transform.position = new Vector2 (targetPos.x, transform.position.y);
						// 如果移动到目的地，准备移到下个格子
						_pathIndex++;

						if (_isRunAIAcition)
							AiAction ();
					}
				}
			}
			return;
		}

		// 朝目标格子移动
		if (now.Rows == target.Rows) {
			// 行数相等，水平方向移动
			if (transform.position.x < targetPos.x) {
				// 向右移动
				transform.Translate (_spd * Time.deltaTime, 0, 0);
				//Debug.LogFormat ("you {0} {1}", transform.position.x, targetPos.x);
				if (transform.position.x >= targetPos.x) {
					transform.position = new Vector2 (targetPos.x, transform.position.y);
					// 如果移动到目的地，准备移到下个格子
					_pathIndex++;

					if (_isRunAIAcition)
						AiAction ();
				}
			} else {
				// 向左移动
				transform.Translate (-_spd * Time.deltaTime, 0, 0);
				//Debug.LogFormat ("zuo {0} {1}", transform.position.x, targetPos.x);
				if (transform.position.x <= targetPos.x) {
					transform.position = new Vector2 (targetPos.x, transform.position.y);
					// 如果移动到目的地，准备移到下个格子
					_pathIndex++;

					if (_isRunAIAcition)
						AiAction ();
				}
			}

		} else {
			// 列数相等，竖直方向移动
			if (transform.position.y > targetPos.y) {
				// 向xia移动
				transform.Translate (0, -_spd * Time.deltaTime, 0);
				//Debug.LogFormat ("xia {0} {1}", transform.position.y, targetPos.y);
				if (transform.position.y <= targetPos.y) {
					transform.position = new Vector2 (transform.position.x, targetPos.y);
					// 如果移动到目的地，准备移到下个格子
					_pathIndex++;
					//Debug.Log ("移动到目的地");

					if (_isRunAIAcition)
						AiAction ();
				}
			} else {
				// 向shang移动
				transform.Translate (0, _spd * Time.deltaTime, 0);
				//Debug.LogFormat ("shang {0} {1}", transform.position.y, targetPos.y);
				if (transform.position.y >= targetPos.y) {
					transform.position = new Vector2 (transform.position.x, targetPos.y);
					// 如果移动到目的地，准备移到下个格子
					_pathIndex++;

					if (_isRunAIAcition)
						AiAction ();
				}
			}
		}

		return;

	}

	public List<GridDate> FindingPath(Grid now, Grid target){
		_openList.Clear ();
		_closeList.Clear ();
		_isFindPath = false;
		path.Clear ();

		GridDate nowData = new GridDate (now.Rows, now.Columns);

		/*_map.Rows = 2;
		_map.Columns = 11;
		*/

		//敌人初始位置
		//GridDate _direnGrid=new GridDate(_map.Rows,_map.Columns);

		_openList.Add (nowData);//将初始位置加入到开启列表
		//获取初始格子
		//GridDate _mingrid=_openList[0];
		//获取玩家坐标
		//Vector2 pos2=GameObject.Find("Player").transform.position;
		bool isFindTarget = false;
		do{

			GridDate _mingrid=_openList[0];
			_mingrid._gCost = 0;

			//gridDate._hCost = (pos2.x - _mingrid.Rows) + (pos2.y - _mingrid.Columns);
			//遍历开启列表，获取权值最小的格子,如果列表中有玩家位置，则跳出
			//Debug.Log(_mingrid);
			for(int i=0;i<_openList.Count;i++){
				if(_openList[i]._fCost<_mingrid._fCost){
					_mingrid=_openList[i];

				}
				if(_openList[i].Rows==target.Rows && _openList[i].Columns==target.Columns){
					isFindTarget=true;

					//List<GridDate> path = new List<GridDate>();

					GridDate nowGrid = _openList[i];
					path.Add(nowGrid);
					do{
						nowGrid = nowGrid._writeGrid;
						if(nowGrid == null)
							break;
						path.Insert(0, nowGrid);
					}while(true);



					string strPath = string.Empty;
					for(int pathIndex = 0; pathIndex < path.Count; pathIndex++){
						strPath += string.Format("{0}\n", path[pathIndex]);
					}
					//Debug.Log(strPath);


					break;

				}

			}

			//在开启列表中删除此格子，并将格子加入关闭列表
			_openList.Remove(_mingrid);
			_closeList.Add(_mingrid);

			if(isFindTarget){
				break;
			}
			//如果获取的最小权值的格子上一格可以移动并且没有在关闭列表中,添加到开启列表
			if(IsAIPassGrid (_mingrid.Rows+1,_mingrid.Columns) 
				&& !GridDate.IsList(_closeList,_mingrid.Rows+1,_mingrid.Columns)){
				GridDate newUpGrid = new GridDate(_mingrid.Rows+1,_mingrid.Columns);
				_openList.Add(newUpGrid);
				//记录前一个格子
				newUpGrid._writeGrid=_mingrid;
				//敌人移动步数+1
				newUpGrid._gCost = _mingrid._gCost + 1;
				//加入到open列表的格子距离玩家步数
				newUpGrid._hCost = newUpGrid.CalcMoveCount(target);

			}

			//Debug.Log(_openList.Count);

			//如果获取的最小权值的格子下一格可以移动并且没有在关闭列表中,添加到开启列表
			if(IsAIPassGrid (_mingrid.Rows-1,_mingrid.Columns)   
				&& !GridDate.IsList(_closeList,_mingrid.Rows-1,_mingrid.Columns)){
				GridDate newDownGrid=new GridDate(_mingrid.Rows-1,_mingrid.Columns);
				_openList.Add(newDownGrid);
				//记录前一个格子
				newDownGrid._writeGrid=_mingrid;
				//敌人移动步数+1
				newDownGrid._gCost = _mingrid._gCost + 1;
				//加入到open列表的格子距离玩家步数
				newDownGrid._hCost = newDownGrid.CalcMoveCount(target);
			}
			//如果获取的最小权值的格子左一格可以移动并且没有在关闭列表中,添加到开启列表
			if(IsAIPassGrid (_mingrid.Rows,_mingrid.Columns-1) 
				&& !GridDate.IsList(_closeList,_mingrid.Rows,_mingrid.Columns-1)){
				GridDate newLeftGrid=new GridDate(_mingrid.Rows,_mingrid.Columns-1);

				_openList.Add(newLeftGrid);
				//记录前一个格子
				newLeftGrid._writeGrid=_mingrid;
				//敌人移动步数+1
				newLeftGrid._gCost = _mingrid._gCost + 1;
				//加入到open列表的格子距离玩家步数
				newLeftGrid._hCost = newLeftGrid.CalcMoveCount(target);
			}
			//如果获取的最小权值的格子右一格可以移动并且没有在关闭列表中,添加到开启列表
			if(IsAIPassGrid (_mingrid.Rows,_mingrid.Columns+1) 
				&& !GridDate.IsList(_closeList,_mingrid.Rows,_mingrid.Columns+1)){
				GridDate newRightGrid=new GridDate(_mingrid.Rows,_mingrid.Columns+1);
				_openList.Add(newRightGrid);
				//记录前一个格子
				newRightGrid._writeGrid=_mingrid;
				//敌人移动步数+1
				newRightGrid._gCost = _mingrid._gCost + 1;
				//加入到open列表的格子距离玩家步数
				newRightGrid._hCost = newRightGrid.CalcMoveCount(target);
			}

			//Debug.LogFormat("_openList.Count:{0} _closeList.Count:{1}", 
			//	_openList.Count, _closeList.Count);

		}while(_openList.Count > 0);
		return path;
	}


	/// <summary>
	///判断是否在危险区域
	/// </summary>
	public bool IsDangerousArea(Grid nowGrig){
		// 清空危险区域，安全区域
		dangerousList.Clear();
		safeList.Clear ();

		Bomb bomb;
		//获取所有炸弹脚本
		Bomb [] _bombnum = GameObject.FindObjectsOfType<Bomb> ();

		// 获取和原地图一致的地图记录危险区域
		_dangerousMap = _map.GetCopyMap();

		bool isCreateUpBomb, isCreateDownBomb, isCreateLeftBomb, isCreateRightBomb;

		//获取所有炸弹的危险格子加入危险列表
		for (int bombIndex = 0; bombIndex < _bombnum.Length; bombIndex++) {
			bomb = _bombnum [bombIndex]; 
			//地图更新
			//_map.GetGrid(bomb.transform.position);

			// 获取炸弹所在格子
			Grid bombGrid = _map.GetGrid (bomb.transform.position);
			// 炸弹格子上下左右可以设置为危险区域
			isCreateUpBomb = isCreateDownBomb = isCreateLeftBomb = isCreateRightBomb = true;
			//遍历威力，找出危险格子，并存入危险列表
			dangerousList.Add(new GridDate(bombGrid.Rows, bombGrid.Columns));
			for (int dangerIndex = 0; dangerIndex < bomb.j; dangerIndex++) {
				//炸弹上方的危险区域
				if(isCreateUpBomb)
					AddGridToDangerousList (new GridDate (bombGrid.Rows + dangerIndex, bombGrid.Columns), 
						ref dangerousList, ref isCreateUpBomb);

				//炸弹下方的危险区域
				if(isCreateDownBomb)
					AddGridToDangerousList (new GridDate(bombGrid.Rows - dangerIndex, bombGrid.Columns), 
						ref dangerousList, ref isCreateDownBomb);

				//炸弹右方的危险区域
				if(isCreateRightBomb)
					AddGridToDangerousList (new GridDate (bombGrid.Rows, bombGrid.Columns + dangerIndex), 
						ref dangerousList, ref isCreateRightBomb);

				//炸弹左方的危险区域
				if(isCreateLeftBomb)
					AddGridToDangerousList (new GridDate (bombGrid.Rows, bombGrid.Columns - dangerIndex), 
						ref dangerousList, ref isCreateLeftBomb);
			}

		}

		if (_bombnum.Length == 0)
			return false;

		// 在地图上把危险区域标出来
		for(int i = 0; i < dangerousList.Count; i++){
			_dangerousMap [dangerousList[i].Rows, dangerousList[i].Columns] = 99;
		}

		//  遍历整张地图，把安全区域加入列表
		for(int i = 0; i < _dangerousMap.GetLength(0); i++){
			for(int j = 0; j < _dangerousMap.GetLength(1); j++){
				if (_dangerousMap [i, j] == 1)
					safeList.Add (new GridDate(i, j));
			}
		}
		/*
		//将所有路的格子为危险格子的删掉
		for (int pathListIndex = safeList.Count - 1; pathListIndex >= 0; pathListIndex--) {
			for (int dangerousListIndex = 0; dangerousListIndex < dangerousList.Count; dangerousListIndex++) {
				if (safeList [pathListIndex] == dangerousList [dangerousListIndex]) {
					safeList.Remove (safeList [pathListIndex]);
				}
			}
		}
		*/
		//判断敌人是否在安全区域
		return !GridDate.IsList(safeList, nowGrig.Rows, nowGrig.Columns);

		/*for (int direnSafe = 0; direnSafe < safeList.Count; direnSafe++) {
			//如果敌人在安全区域，返回为真否则进行躲避危险行为
			if (nowGrig.Rows == safeList [direnSafe].Rows && nowGrig.Columns == safeList [direnSafe].Columns) {
				return true;
			}	

		}

		return false;*/
	}

	/// <summary>
	/// 把格子加入危险区域
	/// </summary>
	/// <param name="grid">判定格子</param>
	/// <param name="isCreateBombInDir">是否在该方向创建炸弹</param>
	void AddGridToDangerousList(GridDate grid, ref List<GridDate> dangerousGridList, ref bool isCreateBombInDir){
		// 如果该格子不能移动，不在该方向继续创建炸弹并退出
		if(!_map.IsPassGrid(grid)){
			isCreateBombInDir = false;
			return;
		}
		// 把该格子加入危险区域列表
		dangerousGridList.Add (grid);
	}

	//移动到最近安全位置
	public bool AearestSafe(){
		// 获取自身所在格子
		Grid nowGrid = _map.GetGrid(transform.position);

		// 如果无安全区域，返回不往安全区域移动
		if (safeList.Count == 0) {
			//Debug.Log ("无安全格子");
			return false;
		}
			
		//IsDangerousArea (nowGrid);

		// 计算自身移动到所有安全区域理论花费步数
		//string str = "安全格子:";
		for(int i = 0; i < safeList.Count; i++){
			safeList[i]._theory = safeList[i].CalcMoveCount(nowGrid);

			/*if (i != 0)
				str += " ";
			str += string.Format ("[{0},{1}]", safeList[i].Rows, safeList[i].Columns);*/
		}
		//Debug.Log (str);

		// 安全区域根据理论移动步数进行升序排列
		safeList.Sort (new SortGridDataByTheoryAsc());

		// 计算获得实际移动步数最小的安全格子
		GridDate nowCalcGrid;
		// 声明当前移动列表，最小移动列表
		List<GridDate> nowMoveList = null, minMoveList = null;
		for(int i = 0; i < safeList.Count; i++){
			// 获取当前计算步数格子
			nowCalcGrid = safeList[i];

			// 如果最小移动列表不为空，且当前计算格子理论移动步数大于等于最小实际移动步数，退出
			if (minMoveList != null && nowCalcGrid._theory >= minMoveList.Count - 1)
				break;

			// 计算移动到当前格子路径
			nowMoveList = FindingPath(nowGrid, nowCalcGrid);
			//PrintPath (nowMoveList);

			// 如果无最小移动列表或者当前移动步数比最小移动步数小，重新赋值
			if (nowMoveList.Count != 0 && (minMoveList == null || nowMoveList.Count < minMoveList.Count)) {
				minMoveList = nowMoveList;
				break;
			}
		}

		if (minMoveList == null || minMoveList.Count == 0)
			return false;

		// 移动路径等于最小移动路径
		SetPathVarDefault();
		path = minMoveList;

		//Debug.Log ("Safe");
		// 打印移动路径
		PrintPath(path);

		//CancelInvoke ();

		return true;

		//安全列表里的第一个格子离敌人的实际步数和理论步数
		/*GridDate _safeMinGrid=safeList[0];
		path = FindingPath(nowGrid,_safeMinGrid);  
		int actual = path.Count;
		_safeMinGrid._theory=(_safeMinGrid.Rows - nowGrid.Rows) + (_safeMinGrid.Columns - nowGrid.Columns);
		//排序安全列表
		for (int NearestIndex=0;NearestIndex<safeList.Count;NearestIndex++){
			//if (((safeList [NearestIndex].Rows - nowGrid.Rows) + (safeList [NearestIndex].Columns - nowGrid.Columns)) <
			//	  ((_safeMinGrid.Rows - nowGrid.Rows) + (_safeMinGrid.Columns - nowGrid.Columns))) {
			//		
			//}
			//遍历安全列表，算出当前格子离敌人距离的理论步数
			int theory = (safeList [NearestIndex].Rows - nowGrid.Rows) +
				(safeList [NearestIndex].Columns - nowGrid.Columns);
			//如果理论步数小于  实际步数，则实际步数等于当前格子与敌人的步数
			if (theory < actual) {
				path =  FindingPath  (nowGrid,safeList[NearestIndex]);
				actual = path.Count;
			} 
		} */
			
	}

	/// <summary>
	/// 设置路径相关参数为默认值
	/// </summary>
	void SetPathVarDefault(){
		_isFindPath = true;
		_pathIndex = 0;
	}

	void PrintPath(List<GridDate> printPath){
		return;
		if (printPath == null || printPath.Count == 0)
			return;

		string strPath = string.Empty;
		for(int pathIndex = 0; pathIndex < printPath.Count; pathIndex++){
			strPath += string.Format("{0}\n", printPath[pathIndex]);
		}
		Debug.Log (strPath);
	}

	/// <summary>
	/// 判断与玩家的距离格子是否小于5
	/// </summary>
	bool LessGrid(){
		if (GameObject.FindObjectsOfType<Destory1> ().Length == 0)
			return true;

		//获取玩家格子
		Grid playerGrid =_map.GetGrid( _player.transform.position);
		//获取敌人当前格子
		Grid nowGrid = _map.GetGrid (transform.position);
		//将玩家与敌人之间的格子存入列表
		List<GridDate> moveGrid=new List<GridDate>();
		moveGrid = FindingPath (nowGrid,playerGrid);
		//如果格子小于5，返回为真，否则返回为假
		if (moveGrid.Count <= 200) {
			return true;
		}
		return false;
	}

	/// <summary>
	/// 当距离格子大于5时，将障碍物加入列表,并移动到距离最近的格子
	/// </summary>
	void MoveObstacle(){
		//获取敌人当前格子
		Grid nowGrid = _map.GetGrid (transform.position);  

		obstacleList.Clear ();

		// 获取和原地图一致的地图记录障碍
		int[,] obstacleMap = _map.GetCopyMap();
		//将所有障碍物加入列表
		for (int obstacleRows = 0; obstacleRows < obstacleMap.GetLength (0); obstacleRows++) {

			for (int obstacleColumns = 0; obstacleColumns < obstacleMap.GetLength (1); obstacleColumns++) {

				if (obstacleMap [obstacleRows, obstacleColumns] == 3) {
					
					obstacleList.Add (new GridDate (obstacleRows, obstacleColumns));

				}
			}
		}

		// 如果障碍物列表为空，退出
		if (obstacleList.Count == 0)
			return;

		//找出距离最近的格子
		GridDate latelyGrid = obstacleList [0];
		//算出列表第一个障碍的理论步数
		int theoreticalStep=nowGrid.CalcMoveCount(latelyGrid);
		//遍历列表中所有障碍，算出最小理论步数的格子，并获取此格子
		for(int obstacleListIndex=1;obstacleListIndex<obstacleList.Count;obstacleListIndex++){

			if (nowGrid.CalcMoveCount (obstacleList [obstacleListIndex]) < theoreticalStep) {
				theoreticalStep = nowGrid.CalcMoveCount (obstacleList [obstacleListIndex]);

				latelyGrid = obstacleList [obstacleListIndex];
			}
			
			/*	//将列表中不是敌人上下左右四个方向上的障碍删除
		for(int i=obstacleList.Count;i>=0;i--){
			if (nowGrid.CalcMoveCount (obstacleList[i]) > 3){
				obstacleList.Remove (obstacleList[i]);
			}

		}*/
		
		}
		//声明最近的障碍路径，并存入列表
		List <GridDate> latelyGridList=new List<GridDate>();

		// 把障碍物暂时设为通路，方便寻路
		_map._Map [latelyGrid.Rows, latelyGrid.Columns] = 1;
		latelyGridList = FindingPath (nowGrid,latelyGrid);
		// 重新设为障碍物
		_map._Map [latelyGrid.Rows, latelyGrid.Columns] = 3;

		//  没有找到路径，退出
		if (latelyGridList.Count < 2)
			return;

		//遍历列表，删除障碍所在的格子
		latelyGridList.RemoveAt(latelyGridList.Count-1);
		// 移动路径等于最小移动路径
		SetPathVarDefault();
		path = latelyGridList;
		//获取目的地位置
		Grid destination = latelyGridList [latelyGridList.Count-1];
		//如果敌人到达目的位置，则开始放炸弹
		IsCanPutDownBomb (destination);

		// 打印移动路径
		//Debug.Log ("Obstacle");
		PrintPath(path);
     }
		
	/// <summary>
	/// 满足追击玩家条件时，追击玩家
	/// </summary>
	bool Pursuit(){
		//获取玩家格子
		Grid playerGrid =_map.GetGrid( _player.transform.position);
		//获取敌人当前格子
		Grid nowGrid = _map.GetGrid (transform.position);  
		//将与玩家的路径存入列表
		List <GridDate> wanJiaList=new List<GridDate>();
		wanJiaList = FindingPath (nowGrid,playerGrid);

		// 如果没找到移到玩家路径，退出
		if (wanJiaList == null || wanJiaList.Count == 0) 
			return false;

		// 如果找到玩家，但是不需要移动便可以放炸弹
		if (wanJiaList.Count <= 1) {
			IsCanPutDownBomb (nowGrid);
			_isFindPath = false;
			return true;
		}

		//遍历列表，删除玩家所在的格子
		wanJiaList.RemoveAt(wanJiaList.Count - 1);
		// 移动路径等于距离玩家的路径
		path = wanJiaList;

		//获取目的地位置
		Grid destination = wanJiaList [wanJiaList.Count-1];
		//如果敌人到达目的位置，则开始放炸弹
		IsCanPutDownBomb(destination);
		SetPathVarDefault ();
		// 打印移动路径
		//Debug.Log ("Pursuit");
		PrintPath(path);

		return true;
	}

	/// <summary>
	/// AI是否能通过该格子
	/// </summary>
	bool IsAIPassGrid(int row, int column){
		// 不检测炸弹，直接返回原始通过信息
		if(!_isFindPathCheckBomb)
			return _map.IsPassGrid (row, column);

		// 超出数组范围，返回不能通过
		if (row < 0 || row >= _dangerousMap.GetLength(0) ||
			column < 0 || column  >= _dangerousMap.GetLength(1))
			return false;

		// 如果该格子是炸弹，退出
		if (_dangerousMap [row, column] == 99)
			return false;

		// 返回原始是否能通过信息
		return _map.IsPassGrid (row, column);
	}

	/// <summary>
	/// 如果该格子能放置炸弹则放置；否则直接退出
	/// </summary>
	bool IsCanPutDownBomb(Grid destinationGrid){
		if (IsPutDownBomb (destinationGrid)) {
			//Debug.LogFormat ("put:{0} {1}", chushibombnum, bombnum);
			if(chushibombnum < bombnum){
				PutBomb ();
				chushibombnum++;
				return true;
			}
		}
		return false;
	}

	/// <summary>
	/// 判断放炸弹的条件
	/// </summary>
	/// <returns><c>true</c> <c>false</c>.</returns>
	bool IsPutDownBomb(Grid destinationGrid){
		//获取敌人当前格子
		Grid nowGrid = _map.GetGrid (transform.position); 
		//如果敌人的位置等于目的地位置，返回为true，否则返回为false
		if(nowGrid.Rows==destinationGrid.Rows && nowGrid.Columns==destinationGrid.Columns){
			return true;
		}
		return false;

	}

	/// <summary>
	/// 放炸弹
	/// </summary>
	void PutBomb(){
		if (_bombPrefab) {
			Bomb bomb = Instantiate (_bombPrefab);
			//让炸弹定位在玩家最近的格子里
			bomb.transform.position = 
				_map.GetNearGridPos (transform.position);
			bomb.InitData (_weiLi, this);
			//Debug.Log ("Boom!!!!!"+bomb.transform.position);


		}
	}

	/// <summary>
	/// 敌人AI
	/// </summary>
	void AiAction(){
		if (!_isRunAIAcition)
			return;
		//Debug.Log ("AiAction()");
		//获取敌人当前格子
		Grid nowGrid = _map.GetGrid (transform.position); 

		//移动到最近障碍物的地方    
		//MoveObstacle ();
		//return;

		//如果敌人在危险区域时
		_isFindPathCheckBomb = false;
		if (IsDangerousArea (nowGrid)) {
			//开始躲避炸弹，移到安全区域
			AearestSafe();

		} 
		//如果敌人在安全区域,并满足追击条件
		else if (LessGrid()) {
			//追击玩家
			_isFindPathCheckBomb = true;
			Pursuit ();

		}
		//如果敌人在安全区域,但不满足追击条件
		else{
			//移动到最近障碍物的地方 
			_isFindPathCheckBomb = true;
			MoveObstacle ();

		}

		_isRunAIAcition = false;
	}




}
