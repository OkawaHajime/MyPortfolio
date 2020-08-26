using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ColorAttachを継承
/// 花畑の着色時の処理
/// 正解時：Spriteの切り替え
/// 不正解時：無し
/// </summary>
public class FlowerGardenAttach : ColorAttach
{
	[SerializeField]private Sprite _attach = null;
	[SerializeField]private GameObject _tutorial_Object;

	protected override void Regain()
	{
		rend.sprite = _attach;
		Destroy(_tutorial_Object);
		base.Regain();
	}
}
