using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// InterfereObjectを継承
/// ドアの開閉のアニメーションを再生する
/// </summary>
public class Door : InterfereObject
{
    [SerializeField] private AudioClip doorOpen = null;
    [SerializeField] private AudioClip lockerClose = null;

	private Animator _doorAnim = null;
    private AudioSource audioSource = null;
	private bool _open = false;

	protected override void Initialize()
	{
		_doorAnim = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();
	}

	public override void ObjectAction()
	{
		//ドアを開ける
		if (!_open) {
			_open = true;
			_doorAnim.SetBool("Open", true);
			audioSource.PlayOneShot(doorOpen);
		} 
		//ドアを閉じる
		else {
			_open = false;
			_doorAnim.SetBool("Open", false);
			audioSource.PlayOneShot(lockerClose);
		}
	}

	//敵がドアに当たったら、ドアを開閉する
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Enemy") {
			ObjectAction();
		}
	}
}
