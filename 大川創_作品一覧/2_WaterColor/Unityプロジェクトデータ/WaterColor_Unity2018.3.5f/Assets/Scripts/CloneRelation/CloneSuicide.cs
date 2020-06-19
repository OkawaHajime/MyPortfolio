using System.Collections;
using UnityEngine;

public class CloneSuicide : MonoBehaviour
{
	[SerializeField] private float grace = 0.0f;

	private void Awake()
	{
		Initialize();
	}

	protected virtual void Initialize() { }

	private void Start()
	{
		StartCoroutine(Suicide(grace));
	}

	private IEnumerator Suicide(float _grace)
	{
		yield return new WaitForSeconds(_grace);
		Testament();
	}

	protected virtual void Testament()
	{
		Destroy(gameObject);
	}
}
