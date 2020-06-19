using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowmanAttach : ColorAttach
{
	private enum SNOWMAN {
		SNOWMAN_NORMAL,
		SNOWMAN_BLUE,
		SNOWMAN_GREEN,
		SNOWMAN_PURPLE,
		SNOWMAN_GLAD,
	};

	[SerializeField]private GameObject _correct = null;
	[SerializeField]private GameObject _monotone = null;
	[SerializeField]private GameObject _failure_blue = null;
	[SerializeField]private GameObject _failure_green = null;
	[SerializeField]private GameObject _failure_purple = null;
	[SerializeField]private GameObject _burst_blue = null;
	[SerializeField]private GameObject _burst_green = null;
	[SerializeField]private GameObject _burst_purple = null;
	[SerializeField]private ChildWolfAnimation _childWolf = null;
	private float _waitTime = 1.0f;

	protected override void Setting()
	{
		SnowmanAnimationChange(SNOWMAN.SNOWMAN_NORMAL);
		base.Setting();
	}

	protected override void Regain()
	{
		_barrier.ChildKill();
		SnowmanAnimationChange(SNOWMAN.SNOWMAN_GLAD);
		_childWolf.ChildWolfAnimationChange(ChildWolfAnimation.WOLF_CHILD_ANIM.WOLF_CHILD_GLAD);
		_player.PlayerAnimationChange(Player.ANIM_TYPE.ANIM_TYPE_GLAD);
		StartCoroutine(RunningChild());
	}

	private IEnumerator RunningChild()
	{
		yield return new WaitForSeconds(_waitTime);
		_childWolf.ChildWolfAnimationChange(ChildWolfAnimation.WOLF_CHILD_ANIM.WOLF_CHILD_RUN);
	}

	protected override void Failure()
	{
		switch (_nowColor.sprite.name) {
			case "Blue":
				SnowmanAnimationChange(SNOWMAN.SNOWMAN_BLUE);
				break;

			case "Green":
				SnowmanAnimationChange(SNOWMAN.SNOWMAN_GREEN);
				break;

			case "Purple":
				SnowmanAnimationChange(SNOWMAN.SNOWMAN_PURPLE);
				break;
		}
	}

	private void SnowmanAnimationChange(SNOWMAN changeAnim)
	{
		ResetAnimation();
		switch (changeAnim) {
			case SNOWMAN.SNOWMAN_NORMAL:
				_monotone.SetActive(true);
				break;

			case SNOWMAN.SNOWMAN_BLUE:
				_failure_blue.SetActive(true);
				StartCoroutine(ReturnMonotone(_burst_blue));
				break;

			case SNOWMAN.SNOWMAN_GREEN:
				_failure_green.SetActive(true);
				StartCoroutine(ReturnMonotone(_burst_green));
				break;

			case SNOWMAN.SNOWMAN_PURPLE:
				_failure_purple.SetActive(true);
				StartCoroutine(ReturnMonotone(_burst_purple));
				break;

			case SNOWMAN.SNOWMAN_GLAD:
				_correct.SetActive(true);
				break;
		}

	}

	private IEnumerator ReturnMonotone(GameObject death_color)
	{
		yield return new WaitForSeconds(_waitTime);
		_se.PlayBackSound(SoundEffectManager.SOUND_TYPE.SOUND_TYPE_FAILUR);
		box2D.enabled = true;
		SnowmanAnimationChange(SNOWMAN.SNOWMAN_NORMAL);
		Instantiate(death_color, gameObject.transform.position, Quaternion.identity);
	}

	private void ResetAnimation()
	{
		_monotone.SetActive(false);
		_failure_blue.SetActive(false);
		_failure_green.SetActive(false);
		_failure_purple.SetActive(false);
		_correct.SetActive(false);
	}
}
