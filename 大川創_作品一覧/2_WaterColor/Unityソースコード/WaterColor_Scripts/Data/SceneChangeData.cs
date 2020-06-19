using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DataAsset/SceneChangeData")]
public class SceneChangeData : ScriptableObject {

	private List<string> _close_scene;
	private string _after_scene;
	private float _idle_time;
	private SceneChanger.FADE_TYPE _fade_type;

	public void setSceneChangeData(List<string> close_scene, string after_scene, SceneChanger.FADE_TYPE fade_type, float idle_time)
	{
		_close_scene = close_scene;
		_after_scene = after_scene;
		_idle_time = idle_time;
		_fade_type = fade_type;
	}

	public List<string> getCloseScene { get { return _close_scene; } }
	public string getAfterScene { get { return _after_scene; } }
	public float getIdleTime { get { return _idle_time; } }
	public SceneChanger.FADE_TYPE getFadeType { get { return _fade_type; } }
}

