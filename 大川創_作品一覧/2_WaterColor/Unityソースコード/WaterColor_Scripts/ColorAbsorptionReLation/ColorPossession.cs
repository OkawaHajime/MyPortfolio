using UnityEngine;

public class ColorPossession : MonoBehaviour
{
	private GameObject _possessionColor = null;
	private ColorAbsorption _source = null;

	public void GetColor(GameObject possessionSource)
	{
		possessionSource.gameObject.SetActive(false);
		if (_possessionColor) {
			_source = _possessionColor.GetComponent<ColorAbsorption>();
			_source.TurnBack();
		}
		_possessionColor = possessionSource;
	}

	public void PossessionKill()
	{
		Destroy(_possessionColor.gameObject);
	}
}
