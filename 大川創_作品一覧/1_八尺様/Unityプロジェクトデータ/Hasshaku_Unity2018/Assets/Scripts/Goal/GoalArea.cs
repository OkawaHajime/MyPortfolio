using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalArea : MonoBehaviour
{
	[SerializeField]private SceneChangeEffectTrigger _sceneChange = null;

	private void OnTriggerEnter(Collider other)
	{
		//Playerタグのオブジェクトが入ってきたらタグを変更、ゲームリザルトへ遷移
		if (other.gameObject.tag == "Player") {
            Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			other.gameObject.tag = "Finish";
            _sceneChange.ChangeScene();
		}
	}
}
