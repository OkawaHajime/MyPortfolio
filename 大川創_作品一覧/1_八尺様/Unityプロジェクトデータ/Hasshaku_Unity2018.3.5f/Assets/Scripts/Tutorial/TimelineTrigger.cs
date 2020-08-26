using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// Timelineを起動する
/// </summary>
public class TimelineTrigger : MonoBehaviour
{
	[SerializeField]private PlayableDirector _matchTutorial = null;

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player") {
			_matchTutorial.Play();
			Destroy(gameObject);
		}
	}
}
