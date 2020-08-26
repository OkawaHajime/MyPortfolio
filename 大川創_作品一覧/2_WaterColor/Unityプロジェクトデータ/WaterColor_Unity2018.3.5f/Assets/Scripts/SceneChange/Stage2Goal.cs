using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージ2のゴールイベントを行う
/// </summary>
public class Stage2Goal : Goal
{
	protected override void GoalEvent()
	{
		_scene_changer.changeScene();
	}
}
