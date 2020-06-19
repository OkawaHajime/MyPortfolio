using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierBreak : MonoBehaviour
{
	public void ChildKill()
	{
		Destroy(this.transform.GetChild(0).gameObject);
	}
}
