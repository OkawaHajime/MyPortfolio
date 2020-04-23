using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeEffectTrigger : MonoBehaviour
{
	[SerializeField] private SceneChangeData _data = null;
	[SerializeField] private string _now_scene = "現在のシーンを入力";
	[SerializeField] private string _after_scene = "切り替えたいシーンを入力";
	[SerializeField] private SceneChanger.FADE_TYPE _fade_type = SceneChanger.FADE_TYPE.FADE_TYPE_BLACK;
	[SerializeField] private float _idle_time = 1.0f;
    [SerializeField] private AudioClip selectUI = null;

    private List<string> _close_scene = new List<string>();
    private AudioSource audioSource;

	private void Awake()
	{
		_close_scene.Add(_now_scene);

        audioSource = GetComponent<AudioSource>();
    }

	public void changeScene(string after_scene)
	{
		_data.setSceneChangeData(_close_scene, after_scene, _fade_type, _idle_time);
		SceneManager.LoadScene("Loading", LoadSceneMode.Additive);
    }

	public void ChangeScene()
	{
		_data.setSceneChangeData(_close_scene, _after_scene, _fade_type, _idle_time);
		SceneManager.LoadScene("Loading", LoadSceneMode.Additive);
        audioSource.PlayOneShot(selectUI);
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
