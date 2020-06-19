using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteKiller : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Untagged") {
			Destroy(collision.gameObject);
		}
	}
}
