using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildWolfAnimation : MonoBehaviour
{
	public enum WOLF_CHILD_ANIM {
		WOLF_CHILD_SAD,
		WOLF_CHILD_GLAD,
		WOLF_CHILD_RUN,
	};

	[SerializeField]private float _speed = 0.0f;
	[SerializeField] private List<GameObject> _animations = new List<GameObject>();
	private Rigidbody2D _rigid = null;
	private Vector3 _flip = new Vector3();

	private void Awake()
	{
		_rigid = GetComponent<Rigidbody2D>();
		_flip = gameObject.transform.localScale;
		ChildWolfAnimationChange(WOLF_CHILD_ANIM.WOLF_CHILD_SAD);
	}

	private void Update()
	{
		_rigid.velocity = new Vector2(_speed, _rigid.velocity.y);
	}

	public void ChildWolfAnimationChange(WOLF_CHILD_ANIM _changeAnim)
	{
		ResetAnimation();

		switch (_changeAnim) {
			case WOLF_CHILD_ANIM.WOLF_CHILD_SAD:
				SetSadAnimation();
				break;

			case WOLF_CHILD_ANIM.WOLF_CHILD_GLAD:
				SetGladAnimation();
				break;

			case WOLF_CHILD_ANIM.WOLF_CHILD_RUN:
				SetRunAnimation();
				break;
		}
	}

	private void SetSadAnimation()
	{
		_animations[(int)WOLF_CHILD_ANIM.WOLF_CHILD_SAD].SetActive(true);
	}

	private void SetGladAnimation()
	{
		_animations[(int)WOLF_CHILD_ANIM.WOLF_CHILD_GLAD].SetActive(true);
	}

	private void SetRunAnimation()
	{
		enabled = true;
		_animations[(int)WOLF_CHILD_ANIM.WOLF_CHILD_RUN].SetActive(true);
	}

	private void ResetAnimation()
	{
		enabled = false;
		foreach (GameObject anim in _animations) {
			anim.SetActive(false);
		}
	}

	//Collider
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Finish") {
			_flip.x *= -1;
			gameObject.transform.localScale = _flip;
			ChildWolfAnimationChange(WOLF_CHILD_ANIM.WOLF_CHILD_GLAD);
			Destroy(collision.gameObject);
		}
	}
}
