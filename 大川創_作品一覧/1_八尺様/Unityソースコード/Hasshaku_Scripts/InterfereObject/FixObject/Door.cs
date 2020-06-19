using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
		if (!_open) {
			_open = true;
			_doorAnim.SetBool("Open", true);
			audioSource.PlayOneShot(doorOpen);
		} 
		else {
			_open = false;
			_doorAnim.SetBool("Open", false);
			audioSource.PlayOneShot(lockerClose);
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Enemy") {
			ObjectAction();
		}
	}
}
