using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Loadingシーンを読み込む
/// 遷移が可能なシーン全てに配置する
/// </summary>
public class SceneChangeEffectTrigger : MonoBehaviour
{
	[SerializeField] private SceneChangeData _data = null;
	[SerializeField] private string _now_scene = "現在のシーンを入力";
	[SerializeField] private string _after_scene = "切り替えたいシーンを入力";
	[SerializeField] private SceneChanger.FADE_TYPE _fade_type = SceneChanger.FADE_TYPE.FADE_TYPE_BLACK;
	[SerializeField] private float _idle_time = 1.0f;

	private List<string> _close_scene = new List<string>();

	private void Awake()
	{
		_close_scene.Add(_now_scene);
	}

	//Serializeで入力した内容を保存後、Loadingシーンを読み込む
	public void changeScene()
	{
		_data.setSceneChangeData(_close_scene, _after_scene, _fade_type, _idle_time);
		SceneManager.LoadScene("Loading", LoadSceneMode.Additive);
	}

	public void addCloseScene(string scene)
	{
		_close_scene.Add(scene);
	}

	public void setAfterScene(string scene)
	{
		_after_scene = scene;
	}
}
