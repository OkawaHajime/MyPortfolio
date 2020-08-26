using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 白黒の色の精を削除する
/// </summary>
public class WhiteKiller : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Untagged") {
			Destroy(collision.gameObject);
		}
	}
}
