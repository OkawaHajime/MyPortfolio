using UnityEngine;

/// <summary>
/// プレイヤーのリアクションエフェクトの生成を行う
/// </summary>
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

	//初期設定
	private void Start()
	{
		ResetReaction();
	}

	//特定のリアクションエフェクトの再生
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

	//全てのリアクションエフェクトを非表示
	private void ResetReaction()
	{
		_gladReaction.gameObject.SetActive(false);
		_questionReaction.gameObject.SetActive(false);
		_surpriseReaction.gameObject.SetActive(false);
	}
}
