using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerMantisAttach : ColorAttach
{
	public enum FLOWERMANTIS {
		FLOWERMANTIS_INTIMIDATION,
		FLOWERMANTIS_SMILE,
		FLOWERMANTIS_ATTACK,
	};

	[SerializeField] private List<GameObject> _animations = new List<GameObject>();
	private GameObject _clone = null;
	private string loopEnd = null;

	protected override void Setting()
	{
		loopEnd = _animations[(int)FLOWERMANTIS.FLOWERMANTIS_SMILE].name;
		base.Setting();
	}

	protected override void Regain()
	{
		SetAnimation(FLOWERMANTIS.FLOWERMANTIS_SMILE);
		base.Regain();
	}

	protected override void Failure()
	{
		SetAnimation(FLOWERMANTIS.FLOWERMANTIS_ATTACK);
		_player.PlayerAnimationChange(Player.ANIM_TYPE.ANIM_TYPE_SURPRISE);
	}

	public void SetAnimation(FLOWERMANTIS _changeAnim)
	{
		ResetAnimation();

		switch (_changeAnim) {
			case FLOWERMANTIS.FLOWERMANTIS_INTIMIDATION:
				IntimidationAnim();
				break;

			case FLOWERMANTIS.FLOWERMANTIS_SMILE:
				SmileAnim();
				break;

			case FLOWERMANTIS.FLOWERMANTIS_ATTACK:
				AttackAnim();
				break;
		}
	}

	private void IntimidationAnim()
	{
		_animations[(int)FLOWERMANTIS.FLOWERMANTIS_INTIMIDATION].SetActive(true);
		box2D.enabled = true;
	}

	private void SmileAnim()
	{
		_animations[(int)FLOWERMANTIS.FLOWERMANTIS_SMILE].SetActive(true);
	}

	private void AttackAnim()
	{
		_clone = (GameObject)Instantiate(_animations[(int)FLOWERMANTIS.FLOWERMANTIS_ATTACK], gameObject.transform.position, Quaternion.identity);
		_clone.transform.parent = gameObject.transform;
	}

	private void ResetAnimation()
	{
		foreach (GameObject animation in _animations) {
			if (animation.name == loopEnd) {
				break;
			}
			animation.SetActive(false);
		}
	}
}
