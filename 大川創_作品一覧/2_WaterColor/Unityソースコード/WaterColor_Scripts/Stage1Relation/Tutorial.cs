using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ColorAbsorptionを継承
/// チュートリアルの一部進行を行う
/// </summary>
public class Tutorial : ColorAbsorption
{
	[SerializeField] private Animator _tutorial;

	protected override void Initialise()
	{
		_tutorial.SetBool("change", false);
		base.Initialise();
	}

	public override void AbsorbStart()
	{
		_tutorial.SetBool("change", true);
		base.AbsorbStart();
	}
}
