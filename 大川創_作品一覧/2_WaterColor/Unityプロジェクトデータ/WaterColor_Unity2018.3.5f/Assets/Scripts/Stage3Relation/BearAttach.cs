using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearAttach : ColorAttach {
	public enum BEAR {
		BEAR_NORMAL,
		BEAR_SAD,
		BEAR_GREEDY,
		BEAR_GLAD,
		BEAR_YELLOW,
		BEAR_RED,
		BEAR_GREEN,
	}

	[SerializeField] private List<GameObject> _animations = new List<GameObject>();
	[SerializeField] private GameObject _failure_yellow = null;
	[SerializeField] private GameObject _failure_red = null;
	[SerializeField] private GameObject _failure_green = null;
	private GameObject _clone = null;
	private GameObject _honey_Object = null;
	private HoneyAttach _honey = null;
	public bool Attach_complete = false;

	protected override void Setting()
	{
		_honey_Object = GameObject.Find("Honey");
		_honey = _honey_Object.GetComponent<HoneyAttach>();
		base.Setting();
		BearAnimationChange(BEAR.BEAR_NORMAL);
	}

	protected override void Regain()
	{
		if (_honey.Attach_complete) {
			BearAnimationChange(BEAR.BEAR_GLAD);
			base.Regain();
		}
		else {
			Attach_complete = true;
			BearAnimationChange(BEAR.BEAR_GREEDY);
		}
	}

	protected override void Failure()
	{
		switch (_nowColor.sprite.name) {
			case "Yellow":
				BearAnimationChange(BEAR.BEAR_YELLOW);
				break;

			case "Red":
				BearAnimationChange(BEAR.BEAR_RED);
				break;

			case "Green":
				BearAnimationChange(BEAR.BEAR_GREEN);
				break;
		}
	}

	public void BearAnimationChange(BEAR _changeAnim)
	{
		ResetAnimation();
		switch (_changeAnim) {
			case BEAR.BEAR_NORMAL:
				SetPoseAnimation((int)BEAR.BEAR_NORMAL);
				box2D.enabled = true;
				break;

			case BEAR.BEAR_SAD:
				SetPoseAnimation((int)BEAR.BEAR_SAD);
				box2D.enabled = true;
				break;

			case BEAR.BEAR_GREEDY:
				SetPoseAnimation((int)BEAR.BEAR_GREEDY);
				break;

			case BEAR.BEAR_GLAD:
				SetPoseAnimation((int)BEAR.BEAR_GLAD);
				break;

			case BEAR.BEAR_YELLOW:
				SetFailureAnimation(_failure_yellow);
				break;

			case BEAR.BEAR_RED:
				SetFailureAnimation(_failure_red);
				break;

			case BEAR.BEAR_GREEN:
				SetFailureAnimation(_failure_green);
				break;

		}

	}

	private void SetPoseAnimation(int number)
	{
		_animations[number].SetActive(true);
	}

	private void SetFailureAnimation(GameObject failure_color)
	{
		_clone = (GameObject)Instantiate(failure_color, gameObject.transform.position, Quaternion.identity);
		_clone.transform.parent = gameObject.transform;
	}

	private void ResetAnimation()
	{
		foreach (GameObject animation in _animations) {
			animation.SetActive(false);
		}
	}

}

