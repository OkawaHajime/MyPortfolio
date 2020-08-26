using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// シーン遷移のアニメーション
/// フェードイン、フェードアウトを行う
/// </summary>
public class Fade : SceneChangeEffect {
	private Image _image;

	protected override void initialize()
	{
		_image = GetComponentInChildren<Image>();
	}

	public override void startEffect(float delta_time)
	{
		setAlpha(delta_time);
	}

	public override void endEffect(float delta_time)
	{
		setAlpha(1 - delta_time);
	}

	private void setAlpha(float alpha)
	{
		Color color = _image.color;
		_image.color = new Color(color.r, color.g, color.b, alpha);
	}
}
