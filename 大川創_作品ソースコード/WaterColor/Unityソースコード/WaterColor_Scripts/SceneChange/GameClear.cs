using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClear : SceneChangeEffect
{
	private Image _image;
	private Text _text;

	protected override void initialize()
	{
		_image = GetComponentInChildren<Image>();
		_text = GetComponentInChildren<Text>();
	}

	public override void startEffect(float delta_time)
	{
		setAlpha(delta_time);
		setTextAlpha(delta_time);
	}

	public override void endEffect(float delta_time)
	{
		setAlpha(1 - delta_time);
		setTextAlpha(1 - delta_time);
	}

	private void setAlpha(float alpha)
	{
		Color color = _image.color;
		_image.color = new Color(color.r, color.g, color.b, alpha);
	}

	private void setTextAlpha(float alpha)
	{
		Color textColor = _text.color;
		_text.color = new Color(textColor.r, textColor.g, textColor.b, alpha);
	}

}
