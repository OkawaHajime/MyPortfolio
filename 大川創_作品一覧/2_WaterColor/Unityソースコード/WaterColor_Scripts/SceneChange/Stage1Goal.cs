using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Goal : Goal
{
    [SerializeField] private Rigidbody2D _playerRigid = null;
    [SerializeField]private float _flySpeed = 0.0f;

    private void Awake()
    {
        enabled = false;
    }

    private void Update()
    {
        _playerRigid.velocity = new Vector2(_playerRigid.velocity.x, _flySpeed);
    }

    protected override void GoalEvent()
    {
        enabled = true;
        _player.PlayerAnimationChange(Player.ANIM_TYPE.ANIM_TYPE_SPECIAL);
        StartCoroutine(StartChangeScene());
    }

    private IEnumerator StartChangeScene()
    {
        yield return new WaitForSeconds(0.5f);
        _scene_changer.changeScene();
    }
}
