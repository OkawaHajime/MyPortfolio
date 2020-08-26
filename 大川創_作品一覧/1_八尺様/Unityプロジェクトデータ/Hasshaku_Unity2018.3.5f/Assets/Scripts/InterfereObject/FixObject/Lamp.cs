using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// マッチが使用されたときに点灯させる
/// </summary>
public class Lamp : MonoBehaviour
{
	[SerializeField] private int _burnTime = 120;
	[SerializeField] private GameObject _lightObject = null;
    [SerializeField] private AudioClip burnOutFire = null;

	private Animator _burningAnim = null;
    private AudioSource audioSource = null;

	private void Awake()
	{
		_burningAnim = _lightObject.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
	}

	//ランプを点灯
	public void Ignition()
	{
		gameObject.tag = "Lamp_Burning";
		_burningAnim.SetBool("Burning", true);
		StartCoroutine(Burning());
	}

	//指定時間待機後、消灯
	private IEnumerator Burning()
	{
		yield return new WaitForSeconds(_burnTime);
		gameObject.tag = "Lamp";
		_burningAnim.SetBool("Burning", false);
        audioSource.PlayOneShot(burnOutFire);
	}
}
