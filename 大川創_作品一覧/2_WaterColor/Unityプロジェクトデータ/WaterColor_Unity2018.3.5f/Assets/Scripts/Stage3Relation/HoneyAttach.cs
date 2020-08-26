using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ColorAttachを継承
/// 蜂の巣の着色時の処理
/// 
/// </summary>
public class HoneyAttach : ColorAttach
{
	[SerializeField]private Sprite _correct = null;
	[SerializeField]private Sprite _failure_brown = null;
	[SerializeField]private Sprite _failure_red = null;
	[SerializeField]private Sprite _failure_green = null;
	[SerializeField]private Sprite _return_mono = null;
	[SerializeField]private GameObject _death_brown = null;
	[SerializeField]private GameObject _death_red = null;
	[SerializeField]private GameObject _death_green = null;
	private GameObject _bear_Object = null;
	private BearAttach _bear = null;
	private float _waitTime = 1.0f;
	public bool Attach_complete = false;

	protected override void Initialize()
	{
		_bear_Object = GameObject.Find("Bear");
		_bear = _bear_Object.GetComponent<BearAttach>();
		base.Initialize();
	}

	protected override void Regain()
	{
		//熊の進行状況によって分岐
		if (_bear.Attach_complete) {
			_bear.BearAnimationChange(BearAttach.BEAR.BEAR_GLAD);
			rend.sprite = _correct;
			base.Regain();
		}
		else {
			Attach_complete = true;
			rend.sprite = _correct;
			_bear.BearAnimationChange(BearAttach.BEAR.BEAR_SAD);
		}

	}

	protected override void Failure()
	{
		//色に応じたSpriteに切り替え
		switch (_nowColor.sprite.name) {
			case "Brown":
				rend.sprite = _failure_brown;
				StartCoroutine(ReturnMonotone(_death_brown));
				break;

			case "Red":
				rend.sprite = _failure_red;
				StartCoroutine(ReturnMonotone(_death_red));
				break;

			case "Green":
				rend.sprite = _failure_green;
				StartCoroutine(ReturnMonotone(_death_green));
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
