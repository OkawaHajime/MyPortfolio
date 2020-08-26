using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージ1のオープニングイベントを行う
/// </summary>
public class MonotoneSourceWark : MonoBehaviour
{
	[SerializeField]private Player _player = null;
	[SerializeField]private GameObject _burst_yellow = null;
	[SerializeField]private SpriteRenderer _flowerGarden = null;
	[SerializeField]private Sprite _flowerGarden_mono = null;
	[SerializeField]private float _speed = 600.0f;

	//花畑を白黒にし、プレイヤーのアニメーションを切り替える
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Finish") {
			Instantiate(_burst_yellow, gameObject.transform.position, Quaternion.identity);
			_flowerGarden.sprite = _flowerGarden_mono;
			_player.PlayerAnimationChange(Player.ANIM_TYPE.ANIM_TYPE_SURPRISE);
			StartCoroutine(EndEvent());
		}
	}

	//指定時間待った後、プレイヤーを動かし自身を削除
	private IEnumerator EndEvent()
	{
		yield return new WaitForSeconds(1.01f);
		_player.PlayerAnimationChange(Player.ANIM_TYPE.ANIM_TYPE_WALK);
		Destroy(gameObject);
	}
}
