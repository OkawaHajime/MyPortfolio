using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : InterfereObject
{
	[SerializeField] private GameObject _stoperText = null;
	[SerializeField] private int _clearCondtions = 0;

	public override void ObjectAction()
	{
		if (KeyItem.NowPossess == _clearCondtions) {
			Destroy(gameObject);
		} else {
			_stoperText.SetActive(true);
			StartCoroutine(BunishText());
		}
	}

	private IEnumerator BunishText()
	{
		yield return new WaitForSeconds(2.0f);
		_stoperText.SetActive(false);
	}
}
