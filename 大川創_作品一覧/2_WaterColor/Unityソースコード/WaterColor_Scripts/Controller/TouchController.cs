using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アクション可能なオブジェクトにタッチした時の処理を実行する
/// 実機（Ipad）版
/// </summary>
public class TouchController : MonoBehaviour
{
	private Touch _touch = new Touch();
	private RaycastHit2D hit = new RaycastHit2D();
	private Vector2 world_point = new Vector2();
	private Bounds rect = new Bounds();
	private bool ColorRetention = false;

	//タッチ処理
	private void Update()
	{
		if (Input.touchCount > 0) {
			_touch = Input.GetTouch(0);
			world_point = Camera.main.ScreenToWorldPoint(_touch.position);

			if (_touch.phase == TouchPhase.Began) {
				hit = Physics2D.Raycast(world_point, Vector2.zero);

				//Rayを飛ばしてヒットしたオブジェクトによって処理を分岐
				if (hit) {
					switch (hit.collider.gameObject.tag) {
						case "Source":
							if (!ColorRetention) {
								hit.collider.GetComponent<ColorAbsorption>().AbsorbStart();
								ColorRetention = true;
							}
							break;

						case "Regainer":
							hit.collider.GetComponent<ColorAttach>().AttachStart();
							ColorRetention = false;
							break;
					}
				}
			}
		}
	}
}
