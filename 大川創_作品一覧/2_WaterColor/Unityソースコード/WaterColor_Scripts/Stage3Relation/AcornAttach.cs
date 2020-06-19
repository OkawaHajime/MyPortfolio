using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	private IEnumerator ReturnMonotone(GameObject death_color)
	{
		yield return new WaitForSeconds(_waitTime);
		_se.PlayBackSound(SoundEffectManager.SOUND_TYPE.SOUND_TYPE_FAILUR);
		box2D.enabled = true;
		rend.sprite = _return_mono;
		Instantiate(death_color, gameObject.transform.position, Quaternion.identity);
	}
}
