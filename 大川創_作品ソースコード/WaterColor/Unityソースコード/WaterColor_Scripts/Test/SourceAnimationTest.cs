using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SourceAnimationTest : MonoBehaviour
{
	[SerializeField]private List<Animator> _sourceAnimators = new List<Animator>();
	private Animator _possession_color = null;

	private void Awake()
	{
		SetSourceAnimation();
	}

	private void SetSourceAnimation()
	{
		foreach(Animator sourceAnimator in _sourceAnimators) {
			sourceAnimator.SetBool(sourceAnimator.gameObject.name, true);
		}
	}

	public void ColorRetention(Animator sourceAnimator)
	{
		sourceAnimator.gameObject.SetActive(false);
		if (_possession_color) {
			_possession_color.gameObject.SetActive(true);
			_possession_color.enabled = true;
			_possession_color.SetBool(_possession_color.gameObject.name, true);
		}
		_possession_color = sourceAnimator;
	}

}
