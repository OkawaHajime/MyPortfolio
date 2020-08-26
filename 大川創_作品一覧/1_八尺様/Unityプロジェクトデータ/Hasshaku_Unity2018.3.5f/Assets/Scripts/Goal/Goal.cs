using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// InterfereObjectを継承
/// キーアイテムを収集数で動作を変える
/// </summary>
public class Goal : InterfereObject
{
	[SerializeField] private GameObject _stoperText = null;
	[SerializeField] private int _clearCondtions = 0;

	public override void ObjectAction()
	{
		//キーアイテムを全て集めていたらオブジェクトを削除
		if (KeyItem.NowPossess == _clearCondtions) {
			Destroy(gameObject);
		} 
		//集めていない場合はテキストを表示
		else {
			_stoperText.SetActive(true);
			StartCoroutine(BunishText());
		}
	}

	//指定時間待機後、テキストを非表示にする
	private IEnumerator BunishText()
	{
		yield return new WaitForSeconds(2.0f);
		_stoperText.SetActive(false);
	}
}
