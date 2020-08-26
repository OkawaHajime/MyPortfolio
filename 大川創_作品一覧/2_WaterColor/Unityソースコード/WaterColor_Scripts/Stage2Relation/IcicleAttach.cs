using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ColorAttachを継承
/// つららの着色時の処理
/// 正解時：Spriteの切り替え、Rigidbodyを使って落下させる
/// 不正解時：色に対応したSpriteに切り替え
/// </summary>
public class IcicleAttach : ColorAttach
{
	[SerializeField]private Sprite _attach = null;
	[SerializeField]private Sprite _failure_purple = null;
	[SerializeField]private Sprite _return_mono = null;
	[SerializeField]private GameObject _death_purple = null;
	private Rigidbody2D rb = null;
	private PolygonCollider2D _polygon2D = null;
	private float _waitTime = 1.0f;

	protected override void Initialize()
	{
		_polygon2D = GetComponent<PolygonCollider2D>();
		_polygon2D.enabled = false;
		base.Initialize();
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

	//指定時間待った後、白黒状態に戻す
	private IEnumerator ReturnMonotone(GameObject death_color)
	{
		yield return new WaitForSeconds(_waitTime);
		box2D.enabled = true;
		rend.sprite = _return_mono;
		Instantiate(death_color, gameObject.transform.position, Quaternion.identity);
	}

	//重力の影響を受けるようにする
	private void AddGravity()
	{
		rb = gameObject.AddComponent<Rigidbody2D>();
		rb.gravityScale = 100;
		rb.mass = 100;
	}

	//Collider関連
	private void OnCollisionEnter2D(Collision2D collision)
	{
		//落ちたあと、プレイヤーにリアクションをさせる
		if (collision.gameObject.tag == "Finish") {
			_player.PlayerAnimationChange(Player.ANIM_TYPE.ANIM_TYPE_SURPRISE);
			StartCoroutine(CallBack());
		}
	}

	//指定時間待った後、プレイヤーを動かす
	private IEnumerator CallBack()
	{
		yield return new WaitForSeconds(1.01f);
		_player.PlayerAnimationChange(Player.ANIM_TYPE.ANIM_TYPE_WALK);
	}
}
