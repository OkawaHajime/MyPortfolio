using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfereObject : MonoBehaviour
{
	private void Awake()
	{
		Initialize();
	}

	protected virtual void Initialize() { }
	public virtual void ObjectAction() { }
}
