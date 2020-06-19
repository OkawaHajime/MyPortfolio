using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleAttach : ColorAttach
{
	[SerializeField]private Sprite _attach = null;
	[SerializeField]private Sprite _failure_purple = null;
	[SerializeField]private Sprite _return_mono = null;
	[SerializeField]private GameObject _death_purple = null;
	private Rigidbody2D rb = null;
	private PolygonCollider2D _polygon2D = null;
	private float _waitTime = 1.0f;

	protected override void Setting()
	{
		_polygon2D = GetComponent<PolygonCollider2D>();
		_polygon2D.enabled = false;
		base.Setting();
	}

	protected override void Regain()
	{
		_polygon2D.enabled = true;
		rend.sprite = _attach;
        gameObject.tag = "EditorOnly";
		_barrier.ChildKill();
		_player.PlayerAnimationChange(Player.ANIM_TYPE.ANIM_TYPE_WALK);
		AddGravity();
	}

	protected override void Failure()
	{
		_se.PlayBackSound(SoundEffectManager.SOUND_TYPE.SOUND_TYPE_FAILUR);
		rend.sprite = _failure_purple;
		StartCoroutine(ReturnMonotone(_death_purple));
	}

	private IEnumerator ReturnMonotone(GameObject death_color)
	{
		yield return new WaitForSeconds(_waitTime);
		box2D.enabled = true;
		rend.sprite = _return_mono;
		Instantiate(death_color, gameObject.transform.position, Quaternion.identity);
	}

	private void AddGravity()
	{
		rb = gameObject.AddComponent<Rigidbody2D>();
		rb.gravityScale = 100;
		rb.mass = 100;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Finish") {
			_player.PlayerAnimationChange(Player.ANIM_TYPE.ANIM_TYPE_SURPRISE);
			StartCoroutine(CallBack());
		}
	}

	private IEnumerator CallBack()
	{
		yield return new WaitForSeconds(1.01f);
		_player.PlayerAnimationChange(Player.ANIM_TYPE.ANIM_TYPE_WALK);
	}
}
