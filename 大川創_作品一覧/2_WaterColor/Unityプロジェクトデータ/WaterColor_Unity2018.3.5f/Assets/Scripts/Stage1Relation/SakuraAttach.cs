using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ColorAttachを継承
/// 桜の木の着色時の処理
/// 正解時：3つのイラストを移り変える
/// 不正解時：無し
/// </summary>
public class SakuraAttach : ColorAttach
{
	private enum PHASE_GROWTH {
		PHASE_0,
		PHASE_1,
	};

    [SerializeField] private GameObject _sakuraEffect = null;
    [SerializeField] private Transform _effectGenerate = null;
	[SerializeField] private Sprite _attach = null;
	[SerializeField] private List<SpriteRenderer> Sakura_growth = new List<SpriteRenderer>();
	private PHASE_GROWTH _phase = PHASE_GROWTH.PHASE_0;
	private Vector3 _rotation = new Vector3(-90, 0, 0);
	private float _time = 0.0f;

	private void Start()
	{
		enabled = false;
	}

	protected override void Regain()
	{
		enabled = true;
		rend.sprite = _attach;
        _colorSource.PossessionKill();
	}

	private void Update()
	{
		//イラスト切り替え
		switch (_phase) {
			case PHASE_GROWTH.PHASE_0:
				actOnPhase0();
				break;
			case PHASE_GROWTH.PHASE_1:
				actOnPhase1();
				break;
		}
	}

	//イラスト1をフェードイン、イラスト2をフェードアウトさせる
	private void actOnPhase0()
	{
		_time += getDeltaTime();
		setAlpha(1 - _time, Sakura_growth[(int)_phase]);
		setAlpha(_time, Sakura_growth[(int)_phase + 1]);
		if (_time >= 1.5f) {
			_phase = PHASE_GROWTH.PHASE_1;
			_time = 0.0f;
		}
	}

	//イラスト2をフェードイン、イラスト3をフェードアウトさせ、プレイヤーにリアクションをさせる
	private void actOnPhase1()
	{
		_time += getDeltaTime();
		setAlpha(1 - _time, Sakura_growth[(int)_phase]);
		setAlpha(_time, Sakura_growth[(int)_phase + 1]);
		if (_time >= 1.5f) {
			enabled = false;
			_player.PlayerAnimationChange(Player.ANIM_TYPE.ANIM_TYPE_GLAD);
			Instantiate(_sakuraEffect, _effectGenerate.position, Quaternion.identity);
			_barrier.ChildKill();
		}
	}

	//時間測定
	private float getDeltaTime()
	{
		return Time.unscaledDeltaTime;
	}

	//透明度変更
	private void setAlpha(float alpha, SpriteRenderer rend)
	{
		Color color = rend.color;
		rend.color = new Color(color.r, color.g, color.b, alpha);
	}

}
