using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 現在シーンから別シーンへの遷移を行う
/// </summary>
public class SceneChanger : MonoBehaviour
{
	public enum FADE_TYPE {
		FADE_TYPE_BLACK,
		FADE_TYPE_WHITE,
		FADE_TYPE_GOAL,
	};

	private enum PHASE {
		PHASE_0,
		PHASE_1,
		PHASE_2,
		PHASE_3,
	};

	[SerializeField] private SceneChangeData _data = null;
	[SerializeField] private ActiveSceneNameKeeper _active_scene_keeper = null;

	[SerializeField] private List<string> _close_scene = new List<string>();
	[SerializeField] private string _after_scene = "";
	[SerializeField] private FADE_TYPE _fade_type = FADE_TYPE.FADE_TYPE_BLACK;
	[SerializeField] private float _idle_time = 1.0f;
	[SerializeField] private List<SceneChangeEffect> _effects = new List<SceneChangeEffect>();

	private PHASE _phase = PHASE.PHASE_0;
	private float _time = 0.0f;

	private void Awake()
	{
		//ScripttableObjectからのデータ読み込み
		_close_scene = _data.getCloseScene;
		_after_scene = _data.getAfterScene;
		_fade_type = _data.getFadeType;
		_idle_time = _data.getIdleTime;

		//アクティブシーン名の保存
		_active_scene_keeper.scene_name = _after_scene;

		//初期フェーズに移行
		_phase = PHASE.PHASE_0;
		_time = 0.0f;

		//アクティブ化
		_effects[(int)_fade_type].gameObject.SetActive(true);

		//デリケートの登録
		SceneManager.sceneLoaded += loadSceneDetected;
	}

	private void Update()
	{
		switch (_phase) {
			case PHASE.PHASE_0:
				actOnPhase0();
				break;
			case PHASE.PHASE_1:
				actOnPhase1();
				break;
			case PHASE.PHASE_2:
				actOnPhase2();
				break;
			case PHASE.PHASE_3:
				actOnPhase3();
				break;
		}
	}

	//開始指示
	private void actOnPhase0()
	{
		_phase = PHASE.PHASE_1;
	}

	//フェードアウト
	private void actOnPhase1()
	{
		_time += getDeltaTime();
		_effects[(int)_fade_type].startEffect(_time);

		//フェードアウトが終わったらシーンをアンロード
		if (_time >= 1.0f) {
			foreach (string scene in _close_scene) {
				SceneManager.UnloadSceneAsync(scene);
			}
			_phase = PHASE.PHASE_2;
			_time = 0.0f;
		}
	}

	//待機
	private void actOnPhase2()
	{
		//指定時間待ったらシーンをロード
		_time += getDeltaTime();
		Debug.Log("シーン切り替え待機時間：" + _time);
		if (_time >= _idle_time) {
			SceneManager.LoadScene(_after_scene, LoadSceneMode.Additive);
			_time = -0.7f;
		}
	}

	//フェードイン
	private void actOnPhase3()
	{
		_time += getDeltaTime();
		_effects[(int)_fade_type].endEffect(_time);
		
		//フェードインが終わったら自分をアンロード
		if (1 - _time <= 0.0f) {
			SceneManager.UnloadSceneAsync("Loading");
		}
	}

	private float getDeltaTime()
	{
		return Time.unscaledDeltaTime;
	}

	private void loadSceneDetected(Scene scene, LoadSceneMode mode)
	{
		if (_phase == PHASE.PHASE_2) {
			_phase = PHASE.PHASE_3;
		}
	}
}
