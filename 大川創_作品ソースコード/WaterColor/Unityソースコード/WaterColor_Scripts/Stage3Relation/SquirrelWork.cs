using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquirrelWork : MonoBehaviour
{
    private enum SQUIRREL_ANIM
    {
        SQUIRREL_ANIM_WAIT,
        SQUIRREL_ANIM_RUN,
    };
    [SerializeField] private GameObject _squirrel_wait = null;
    [SerializeField] private GameObject _squirrel_run = null;
    [SerializeField] private List<ColorAbsorption> _lastSources = new List<ColorAbsorption>();
	private Rigidbody2D _squirrelRigid = null;
	private Vector2 _jump = new Vector2();
	private float _jumpPower = 30000.0f;
    private float speed = 300.0f;


    private void Awake()
    {
        _squirrelRigid = GetComponent<Rigidbody2D>();
		_jump.y = _jumpPower;
        SetSquirrelAnimation(SQUIRREL_ANIM.SQUIRREL_ANIM_RUN);
        
    }

    private void Update()
    {
        _squirrelRigid.velocity = new Vector2(speed, _squirrelRigid.velocity.y);
    }

    private void SetSquirrelAnimation(SQUIRREL_ANIM squirrel)
    {
        ResetAnimation();
        switch (squirrel) {

            case SQUIRREL_ANIM.SQUIRREL_ANIM_RUN:
                _squirrel_run.SetActive(true);
                enabled = true;
                break;

            case SQUIRREL_ANIM.SQUIRREL_ANIM_WAIT:
                _squirrel_wait.SetActive(true);
                enabled = false;
                break;
        }
    }

    private void ResetAnimation()
    {
        _squirrel_wait.SetActive(false);
        _squirrel_run.SetActive(false);
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Finish") {
			SetSquirrelAnimation(SQUIRREL_ANIM.SQUIRREL_ANIM_WAIT);
			StartCoroutine(SetLastSource());
		}
	}

	private IEnumerator SetLastSource()
	{
		yield return new WaitForSeconds(0.8f);
		_squirrelRigid.AddForce(_jump);
		foreach (ColorAbsorption lastsource in _lastSources) {
			lastsource.TurnBack();
		}
	}
}
