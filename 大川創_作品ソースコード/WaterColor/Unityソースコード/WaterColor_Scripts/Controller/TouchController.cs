using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
	private Touch _touch = new Touch();
	private RaycastHit2D hit = new RaycastHit2D();
	private Vector2 world_point = new Vector2();
	private Bounds rect = new Bounds();
	private bool ColorRetention = false;

	private void Update()
	{
		if (Input.touchCount > 0) {
			_touch = Input.GetTouch(0);
			world_point = Camera.main.ScreenToWorldPoint(_touch.position);

			if (_touch.phase == TouchPhase.Began) {
				hit = Physics2D.Raycast(world_point, Vector2.zero);

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
