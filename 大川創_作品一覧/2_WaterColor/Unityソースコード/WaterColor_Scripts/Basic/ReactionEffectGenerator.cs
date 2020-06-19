using UnityEngine;

public class ReactionEffectGenerator : MonoBehaviour
{
	public enum REACTION_TYPE {
		REACTION_TYPE_GLAD,
		REACTION_TYPE_QUESTION,
		REACTION_TYPE_SURPRISE,
	};

	[SerializeField] private ParticleSystem _gladReaction = null;
	[SerializeField] private ParticleSystem _questionReaction = null;
	[SerializeField] private ParticleSystem _surpriseReaction = null;

	private void Start()
	{
		ResetReaction();
	}

	public void ReactionGenerate(REACTION_TYPE _reaction)
	{
		ResetReaction();

		switch (_reaction) {
			case REACTION_TYPE.REACTION_TYPE_GLAD:
				_gladReaction.gameObject.SetActive(true);
				_gladReaction.Play();
				break;

			case REACTION_TYPE.REACTION_TYPE_QUESTION:
				_questionReaction.gameObject.SetActive(true);
				_questionReaction.Play();
				break;

			case REACTION_TYPE.REACTION_TYPE_SURPRISE:
				_surpriseReaction.gameObject.SetActive(true);
				_surpriseReaction.Play();
				break;

		}
	}

	private void ResetReaction()
	{
		_gladReaction.gameObject.SetActive(false);
		_questionReaction.gameObject.SetActive(false);
		_surpriseReaction.gameObject.SetActive(false);
	}
}
