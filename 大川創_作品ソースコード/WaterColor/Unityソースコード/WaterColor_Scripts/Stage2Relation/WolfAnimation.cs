using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
		_wolfRigid = GetComponent<Rigidbody2D>();

		enabled = false;
		_flip = gameObject.transform.localScale;
		WolfAnimationChange(WOLF_ANIM.WOLF_ANIM_WAIT);
	}

	private void Update()
	{
		_wolfRigid.velocity = new Vector2(_speed, _wolfRigid.velocity.y);
		_player.gameObject.transform.localPosition = gameObject.transform.localPosition + _positionPlus;
		_player.gameObject.transform.localRotation = gameObject.transform.localRotation;
	}

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

	private void SetRunAnimation()
	{
		enabled = true;
		_runAnimation.SetActive(true);
		_childWolf_flip = _childWolf.gameObject.transform.localScale;
		_flip.x *= -1;
		_childWolf_flip.x *= -1;
		gameObject.transform.localScale = _flip;
		_childWolf.gameObject.transform.localScale = _childWolf_flip;
		_player.PlayerAnimationChange(Player.ANIM_TYPE.ANIM_TYPE_SPECIAL);
		_childWolf.ChildWolfAnimationChange(ChildWolfAnimation.WOLF_CHILD_ANIM.WOLF_CHILD_RUN);

	}

	private void ResetAnimation()
	{
		_waitAnimation.SetActive(false);
		_runAnimation.SetActive(false);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "Player") {
			WolfAnimationChange(WOLF_ANIM.WOLF_ANIM_RUN);
		}
	}
}
