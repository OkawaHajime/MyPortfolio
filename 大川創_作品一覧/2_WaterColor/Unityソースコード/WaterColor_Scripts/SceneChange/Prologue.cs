using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プロローグイラストの切り替えを行う
/// </summary>
public class Prologue : MonoBehaviour
{
	private enum PROLOGUE_PHASE {
		PROLOGUE_PHASE_0,
		PROLOGUE_PHASE_1,
		PROLOGUE_PHASE_2,
		PROLOGUE_PHASE_3,
	};


	[SerializeField]private SceneChangeEffectTrigger _changer = null;
	[SerializeField]private List<SpriteRenderer> _image_prologues = new List<SpriteRenderer>();
	private Color _memory = new Color();
	private PROLOGUE_PHASE _phase = PROLOGUE_PHASE.PROLOGUE_PHASE_0;
	private float _alpha = 0.0f;
	private float _alpha_plus = 0.03f;
	private bool fade_out = false;

	private void Awake()
	{
		//初期設定
		foreach(SpriteRenderer image_prologue in _image_prologues) {
			_memory = image_prologue.color;
			image_prologue.color = new Color(_memory.r, _memory.g, _memory.b, _alpha);
		}
	}

	private void Update()
	{
		//絵の切り替え
		switch (_phase) {
			case PROLOGUE_PHASE.PROLOGUE_PHASE_0:
				actOnPhase(PROLOGUE_PHASE.PROLOGUE_PHASE_1);
				break;

			case PROLOGUE_PHASE.PROLOGUE_PHASE_1:
				actOnPhase(PROLOGUE_PHASE.PROLOGUE_PHASE_2);
				break;

			case PROLOGUE_PHASE.PROLOGUE_PHASE_2:
				actOnPhase(PROLOGUE_PHASE.PROLOGUE_PHASE_3);
				break;

			case PROLOGUE_PHASE.PROLOGUE_PHASE_3:
				actOnLastPhase();
				break;

		}
	}

	//各イラストのフェードイン、フェードアウト
	private void actOnPhase(PROLOGUE_PHASE changePhase)
	{
		_alpha += _alpha_plus;
		if (!fade_out) { 
			setAlpha(_alpha, _image_prologues[(int)_phase]);
			if(_alpha >= 1.8f) {
				_alpha = 0.0f;
				fade_out = true;
			}
		}
		else {
			setAlpha(1 - _alpha, _image_prologues[(int)_phase]);
			if(_alpha >= 1.8f) {
				_alpha = 0.0f;
				fade_out = false;
				_phase = changePhase;
			}
		}
	}

	//最後のイラストを表示しきったらシーン遷移
	private void actOnLastPhase()
	{
		_alpha += _alpha_plus;
		setAlpha(_alpha, _image_prologues[(int)_phase]);
		if(_alpha >= 2.0f) {
			enabled = false;
			_changer.changeScene();
		}
	}

	//透明度の変更
	private void setAlpha(float alpha, SpriteRenderer rend)
	{
		_memory = rend.color;
		rend.color = new Color(_memory.r, _memory.g, _memory.b, alpha);
	}

}
