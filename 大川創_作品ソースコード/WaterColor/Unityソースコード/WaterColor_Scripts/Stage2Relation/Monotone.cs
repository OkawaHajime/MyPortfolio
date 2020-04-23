using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
		_monotone.velocity = new Vector2(_speed, _monotone.velocity.y);
	}
}
