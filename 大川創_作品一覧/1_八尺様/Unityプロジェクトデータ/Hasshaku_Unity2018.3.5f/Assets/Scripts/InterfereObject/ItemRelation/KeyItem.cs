using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Itemを継承
/// 取得時に合計数をテキストで表示する
/// </summary>
public class KeyItem : Item
{
	[SerializeField]private int _clearConditions = 0;
	private GameObject _textObject = null;
	private Text _keyItemPossess = null;
	private static int _nowPossess = 0;

    public static int NowPossess {
        get {
            return _nowPossess;
        }
    }

	protected override void Initialize()
	{
		_textObject = GameObject.Find("KeyItemPossess");
		_keyItemPossess = _textObject.GetComponent<Text>();
	}

	private void Start()
	{
		_nowPossess = 0;
		_keyItemPossess.text = _nowPossess.ToString() + " / " + _clearConditions.ToString();
	}

	//取得時に、現在のキーアイテム所持数を更新
	public override void ObjectAction()
	{
		_nowPossess++;
		_keyItemPossess.text = _nowPossess.ToString() + " / " + _clearConditions.ToString();
        Destroy(gameObject);
	}
}
