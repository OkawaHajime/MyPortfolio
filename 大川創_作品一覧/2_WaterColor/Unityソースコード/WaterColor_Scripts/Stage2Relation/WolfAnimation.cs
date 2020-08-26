using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 狼のアニメーションを再生する
/// </summary>
public class WolfAnimation : MonoBehaviour
{
	private enum WOLF_ANIM {
		WOLF_ANIM_WAIT,
		WOLF_ANIM_RUN,
	};

	[SerializeField]private GameObject _waitAnimation = null;
	[SerializeField]private GameObject _runAnimation = null;
	[SerializeField]private Player _player = null;
	[SerializeField]private ChildWolfAnimation _childWolf = null;
	[SerializeField]private float _speed = 350.0f;
	private Rigidbody2D _wolfRigid = null;
	private Vector3 _flip = new Vector3();
	private Vector3 _childWolf_flip = new Vector3();
	private Vector3 _positionPlus = new Vector3(0.0f, 30.0f, -1.0f);

	private void Awake()
	{
		//情報取得
		_wolfRigid = GetComponent<Rigidbody2D>();

		//初期設定
		enabled = false;
		_flip = gameObject.transform.localScale;
		WolfAnimationChange(WOLF_ANIM.WOLF_ANIM_WAIT);
	}

	private void Update()
	{
		//移動
		_wolfRigid.velocity = new Vector2(_speed, _wolfRigid.velocity.y);
		_player.gameObject.transform.localPosition = gameObject.transform.localPosition + _positionPlus;
		_player.gameObject.transform.localRotation = gameObject.transform.localRotation;
	}

	//指定されたアニメーションを再生
	private void WolfAnimationChange(WOLF_ANIM _changeAnim)
	{
		ResetAnimation();

		switch (_changeAnim) {
			case WOLF_ANIM.WOLF_ANIM_WAIT:
				_waitAnimation.SetActive(true);
				break;

			case WOLF_ANIM.WOLF_ANIM_RUN:
				SetRunAnimation();
				break;
		}
	}

	//走りアニメーション
	private void SetRunAnimation()
	{
		//狼の向きを右に変える
		enabled = true;
		_runAnimation.SetActive(true);
		_flip.x *= -1;
		gameObject.transform.localScale = _flip;
		
		//子狼の向きを右に変え、走りアニメーションを再生
		_childWolf_flip = _childWolf.gameObject.transform.localScale;
		_childWolf_flip.x *= -1;
		_childWolf.gameObject.transform.localScale = _childWolf_flip;
		_childWolf.ChildWolfAnimationChange(ChildWolfAnimation.WOLF_CHILD_ANIM.WOLF_CHILD_RUN);
		
		//プレイヤーの特殊アニメーションを再生
		_player.PlayerAnimationChange(Player.ANIM_TYPE.ANIM_TYPE_SPECIAL);
	}

	//全てのアニメーションを一度止める
	private void ResetAnimation()
	{
		_waitAnimation.SetActive(false);
		_runAnimation.SetActive(false);
	}

	//Collider関連
	private void OnCollisionEnter2D(Collision2D collision)
	{
		//プレイヤーが触れたら走る
		if(collision.gameObject.tag == "Player") {
			WolfAnimationChange(WOLF_ANIM.WOLF_ANIM_RUN);
		}
	}
}
