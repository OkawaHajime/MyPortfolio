using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : SceneChangeEffect
{
	private Image _panel = null;
	private Color _new_panelColor = new Color();
	private float _alpha = 0.0f;

	protected override void Initialize()
	{
		_panel = gameObject.GetComponentInChildren<Image>();
		_new_panelColor = _panel.color;
	}

	public override void StartEffect(float delta_time)
	{
		_alpha = delta_time;
		_new_panelColor.a = _alpha;
		_panel.color = _new_panelColor;
	}

	public override void EndEffect(float delta_time)
	{
		_alpha = delta_time;
		_new_panelColor.a = _alpha;
		_panel.color = _new_panelColor;
	}
}
