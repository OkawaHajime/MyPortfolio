using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ColorAttachを継承
/// きのこの着色時の処理
/// 正解時：きのこと背景のSpriteを切り替える
/// 不正解時：全ての色を付けるまで白黒の状態を維持
/// </summary>
public class MushroomAttach : ColorAttach
{
	private enum PHASE { 
		PHASE_0,
		PHASE_1,
	};

	[SerializeField] private Image _flash = null;
	[SerializeField] private Sprite _rainbowMushroom = null;
	[SerializeField] private Sprite _wood = null;
	[SerializeField] private Sprite _ground = null;
	[SerializeField] private Sprite _background = null;
	[SerializeField] private SpriteRenderer _wood_mono = null;
	[SerializeField] private SpriteRenderer _ground_mono = null;
	[SerializeField] private SpriteRenderer _background_mono = null;
	private PHASE _phase = PHASE.PHASE_0;
	private float _alpha = 0.0f;
	private float _startFlash = 0.1f;
	private float _endFlash = 0.01f;
	private bool _red_complete = false;
	private bool _blue_complete = false;
	private bool _yellow_complete = false;
	private bool _green_complete = false;
	private bool _pink_complete = false;
	private bool _purple_complete = false;
	private bool _brown_complete = false;

	protected override void Initialize()
	{
		enabled = false;
		base.Initialize();
	}

	protected override void Failure()
	{
		//全てのboolがtrueになるまで失敗扱いにする
		switch (_nowColor.sprite.name) {
			case "Red":
				_red_complete = true;
				_colorSource.PossessionKill();
				break;

			case "Blue":
				_blue_complete = true;
				_colorSource.PossessionKill();
				break;

			case "Yellow":
				_yellow_complete = true;
				_colorSource.PossessionKill();
				break;

			case "Green":
				_green_complete = true;
				_colorSource.PossessionKill();
				break;
			
			case "Pink":
				_pink_complete = true;
				_colorSource.PossessionKill();
				break;

			case "Purple":
				_purple_complete = true;
				_colorSource.PossessionKill();
				break;

			case "Brown":
				_brown_complete = true;
				_colorSource.PossessionKill();
				break;
		}

		//全てのboolがtrueになったら着色イベントを開始
		if(_red_complete && _blue_complete && _yellow_complete && _green_complete && _pink_complete && _purple_complete && _brown_complete) {
			box2D.enabled = false;
			enabled = true;
		}
		else {
			box2D.enabled = true;
		}

	}

	private void Update()
	{
		//着色イベント
		switch (_phase) {
			case PHASE.PHASE_0:
				actOnPhase0();
				break;

			case PHASE.PHASE_1:
				actOnPhase1();
				break;
		}
	}

	//フェードイン（フラッシュ）
	private void actOnPhase0()
	{
		//全体が白くなったらSpriteを切り替える
		_alpha += _startFlash;
		setAlpha(_alpha, _flash);
		if(_alpha >= 5.0f) {
			_phase = PHASE.PHASE_1;
			_alpha = 0.0f;
			rend.sprite = _rainbowMushroom;
			_wood_mono.sprite = _wood;
			_ground_mono.sprite = _ground;
			_background_mono.sprite = _background;
		}
	}

	//フェードアウト（フラッシュ開け）
	private void actOnPhase1()
	{
		//フェードアウトが終わったらプレイヤーにリアクションをさせる
		_alpha += _endFlash;
		setAlpha(1 - _alpha, _flash);
		if(_alpha >= 1.0f) {
			enabled = false;
			_barrier.ChildKill();
			_player.PlayerAnimationChange(Player.ANIM_TYPE.ANIM_TYPE_GLAD);
		}
	}

	//透明度変更
	private void setAlpha(float alpha, Image flash)
	{
		Color color = flash.color;
		flash.color = new Color(color.r, color.g, color.b, alpha);
	}

}
