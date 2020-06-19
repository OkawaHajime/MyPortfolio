using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeEffect : MonoBehaviour
{
	private void Awake()
	{
		initialize();
	}

	protected virtual void initialize() { }
	public virtual void startEffect(float delta_time) { }
	public virtual void endEffect(float delta_time) { }
}
