using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeEffect : MonoBehaviour
{
	private void Awake()
	{
		Initialize();
	}

	protected virtual void Initialize() { }
	public virtual void StartEffect(float delta_time) { }
	public virtual void EndEffect(float delta_time) { }
}
