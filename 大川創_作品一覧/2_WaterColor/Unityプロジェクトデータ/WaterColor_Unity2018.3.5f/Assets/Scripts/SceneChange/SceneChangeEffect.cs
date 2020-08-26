using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// シーン遷移時のアニメーションを行う親クラス
/// アニメーションの内容は、このクラスを継承した子クラスで実装する
/// 子クラスのScriptを、Canvasに付ける
/// </summary>
public class SceneChangeEffect : MonoBehaviour
{
	private void Awake()
	{
		initialize();
	}

	/// <summary>
	/// 以下virtual関数
	/// 子クラスで実装する部分
	/// </summary>
	 
	//情報取得
	protected virtual void initialize() { }

	//シーン遷移前のアニメーション
	public virtual void startEffect(float delta_time) { }
	
	//シーン遷移後のアニメーション
	public virtual void endEffect(float delta_time) { }
}
