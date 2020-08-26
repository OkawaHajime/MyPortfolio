﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージ2の終盤のイベントを行う
/// </summary>
public class GladEvent : MonoBehaviour
{
	[SerializeField] private Player _player = null;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag == "Player") {
			_player.PlayerAnimationChange(Player.ANIM_TYPE.ANIM_TYPE_GLAD);
			Destroy(gameObject);
		}
	}
}
