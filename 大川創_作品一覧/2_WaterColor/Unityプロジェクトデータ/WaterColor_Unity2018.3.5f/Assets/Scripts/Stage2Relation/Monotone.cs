using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージ2のオープニングイベントを行う
/// </summary>
public class Monotone : MonoBehaviour
{
	private Rigidbody2D _monotone = null;
	[SerializeField]private float _speed = -800.0f;

	private void Awake()
	{
		_monotone = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		//左に直進
		_monotone.velocity = new Vector2(_speed, _monotone.velocity.y);
	}
}
