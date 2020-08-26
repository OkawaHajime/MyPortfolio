using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージ3のゴールイベントを行う
/// </summary>
public class Stage3Goal : Goal
{
    protected override void GoalEvent()
    {
        _player.PlayerAnimationChange(Player.ANIM_TYPE.ANIM_TYPE_GLAD);
        _scene_changer.changeScene();
    }
}
