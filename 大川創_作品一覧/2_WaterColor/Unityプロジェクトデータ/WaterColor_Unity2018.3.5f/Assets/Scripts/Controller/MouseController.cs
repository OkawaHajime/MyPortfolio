using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アクション可能なオブジェクトにタッチした時の処理を実行する
/// マウス版
/// </summary>
public class MouseController : MonoBehaviour
{
	[SerializeField]private AttachEffectGenerator _generator = null;
	private Vector2 _mousePosition = new Vector2();
	private Vector2 _worldPosition = new Vector2();
	private RaycastHit2D hit = new RaycastHit2D();

	//タッチ処理
	private void Update()
	{
		if (Input.GetMouseButtonDown(0)) {
			_mousePosition = Input.mousePosition;
			_worldPosition = Camera.main.ScreenToWorldPoint(_mousePosition);
			hit = Physics2D.Raycast(_worldPosition, Vector2.zero);

			//Rayを飛ばしてヒットしたオブジェクトによって処理を分岐
			if (hit) {
				switch (hit.collider.gameObject.tag) {
					case "Source":
						hit.collider.GetComponent<ColorAbsorption>().AbsorbStart();
						enabled = false;
						break;

					case "Regainer":
						hit.collider.GetComponent<ColorAttach>().AttachStart();
						break;

				}
			}
		}
	}
}
