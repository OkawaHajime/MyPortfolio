using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゴール到着時のイベントを行う親クラス
/// イベントの中身を、このクラスを継承した子クラスで実装する
/// </summary>
public class Goal : MonoBehaviour
{
	[SerializeField]protected SceneChangeEffectTrigger _scene_changer = null;
	[SerializeField]protected Player _player = null;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player") {
			GoalEvent();
		}
	}

	/// <summary>
	/// 以下virtual関数
	/// 子クラスで実装する部分
	/// </summary>
	 
	//イベントの中身
	protected virtual void GoalEvent(){ }
}
