using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeEffectTrigger : MonoBehaviour
{
	[SerializeField] private SceneChangeData _data = null;
	[SerializeField] private string _scene_now = "現在のシーンを入力";
	[SerializeField] private string _scene_after = "切り替えたいシーンを入力";
	[SerializeField] private SceneChanger.FADE_TYPE _fade_type = SceneChanger.FADE_TYPE.FADE_TYPE_BLACK;
	[SerializeField] private float _idle_time = 1.0f;
    [SerializeField] private AudioClip _sound_change = null;

    private List<string> _scene_close = new List<string>();
    private AudioSource _audioSource = null;

	private void Awake()
	{
		_scene_close.Add(_scene_now);

        _audioSource = GetComponent<AudioSource>();
    }

	public void changeScene(string after_scene)
	{
		_data.setSceneChangeData(_scene_close, after_scene, _fade_type, _idle_time);
		SceneManager.LoadScene("Loading", LoadSceneMode.Additive);
    }

	public void ChangeScene()
	{
		_data.setSceneChangeData(_scene_close, _scene_after, _fade_type, _idle_time);
		if(_sound_change != null) {
			_audioSource.PlayOneShot(_sound_change);
		}
		SceneManager.LoadScene("Loading", LoadSceneMode.Additive);
    }

	public void addCloseScene(string scene)
	{
		_scene_close.Add(scene);
	}

	public void setAfterScene(string scene)
	{
		_scene_after = scene;
	}
}
