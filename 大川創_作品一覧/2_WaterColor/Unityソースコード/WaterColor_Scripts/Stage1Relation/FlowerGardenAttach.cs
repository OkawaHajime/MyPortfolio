using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerGardenAttach : ColorAttach
{
	[SerializeField]private Sprite _attach = null;
	[SerializeField]private GameObject _tutorial_Object;

	protected override void Regain()
	{
		rend.sprite = _attach;
		Destroy(_tutorial_Object);
		base.Regain();
	}
}
