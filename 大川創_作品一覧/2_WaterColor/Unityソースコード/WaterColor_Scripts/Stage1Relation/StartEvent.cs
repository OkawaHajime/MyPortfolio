using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージ1のオープニングイベントのために、プレイヤーの動きを一時的に止める
/// </summary>
public class StartEvent : MonoBehaviour
{
	[SerializeField] private Player _player = null;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag == "Player") {
			_player.PlayerAnimationChange(Player.ANIM_TYPE.ANIM_TYPE_WAIT);
			Destroy(gameObject);
		}
	}
}
