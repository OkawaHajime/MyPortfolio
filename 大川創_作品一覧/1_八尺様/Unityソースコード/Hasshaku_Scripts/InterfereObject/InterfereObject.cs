using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーが左クリックした時にアクションを実行する親クラス
/// アクションの内容は、このクラスを継承した子クラスで実装する
/// 子クラスのScriptを、プレイヤーが干渉できるオブジェクトに付ける
/// </summary>
public class InterfereObject : MonoBehaviour
{
	private void Awake()
	{
		Initialize();
	}

	/// <summary>
	/// 以下virtual関数
	/// 子クラスで実装する部分
	/// </summary>
	 
	//初期設定
	protected virtual void Initialize() { }
	
	//アクション内容
	public virtual void ObjectAction() { }
}
