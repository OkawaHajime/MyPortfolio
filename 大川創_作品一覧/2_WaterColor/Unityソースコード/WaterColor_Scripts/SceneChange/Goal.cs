using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
	[SerializeField]protected SceneChangeEffectTrigger _scene_changer = null;
	[SerializeField]protected Player _player = null;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player") {
			GoalEvent();
		}
	}

	protected virtual void GoalEvent(){ }
}
