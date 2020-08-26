using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 障害物を順番に削除していく
/// </summary>
public class BarrierBreak : MonoBehaviour
{
	public void ChildKill()
	{
		Destroy(this.transform.GetChild(0).gameObject);
	}
}
