using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ColorAttachを継承
/// どんぐりの着色時の処理
/// 正解時：Sprite切り替え
/// 不正解時：色に対応したSpriteに切り替え
/// </summary>
public class AcornAttach : ColorAttach
{
	[SerializeField]private Sprite _correct = null;
	[SerializeField]private Sprite _failure_green = null;
	[SerializeField]private Sprite _failure_yellow = null;
	[SerializeField]private Sprite _return_mono = null;
	[SerializeField]private GameObject _death_green = null;
	[SerializeField]private GameObject _death_yellow = null;
	private float _waitTime = 1.0f;

	protected override void Regain()
	{
		rend.sprite = _correct;
		base.Regain();
	}

	protected override void Failure()
	{
		//色に対応したSpriteに切り替え
		switch (_nowColor.sprite.name) {
			case "Green":
				rend.sprite = _failure_green;
				StartCoroutine(ReturnMonotone(_death_green));
				break;
			
			case "Yellow":
				rend.sprite = _failure_yellow;
				StartCoroutine(ReturnMonotone(_death_yellow));
				break;
		
		}
	}

	//指定時間待機後、Spriteを元に戻す
	private IEnumerator ReturnMonotone(GameObject death_color)
	{
		yield return new WaitForSeconds(_waitTime);
		_se.PlayBackSound(SoundEffectManager.SOUND_TYPE.SOUND_TYPE_FAILUR);
		box2D.enabled = true;
		rend.sprite = _return_mono;
		Instantiate(death_color, gameObject.transform.position, Quaternion.identity);
	}
}
