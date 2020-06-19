using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttachEffectGenerator : MonoBehaviour
{
	private enum ATTACH_TYPE {
		ATTACH_TYPE_RED,
		ATTACH_TYPE_BLUE,
		ATTACH_TYPE_GREEN,
		ATTACH_TYPE_YELLOW,
		ATTACH_TYPE_PINK,
		ATTACH_TYPE_BROWN,
        ATTACH_TYPE_PURPLE,
	}

	[SerializeField] private Image _nowColor = null;
	[SerializeField] private List<GameObject> _attachEffects = new List<GameObject>();
	[SerializeField] private List<GameObject> _regainers = new List<GameObject>();
	private GameObject _selectEffect = null;
	private Vector3 positionPlus = new Vector3(0.0f, 0.0f, -20.0f);

	public void EffectGenerate(string regainerName)
	{
		foreach (GameObject regainer in _regainers) {
			if (regainer.name == regainerName) {
				SelectEffect();
				Instantiate(_selectEffect, regainer.transform.position + positionPlus, Quaternion.identity);
			}
		}
	}

	private void SelectEffect()
	{
		switch (_nowColor.sprite.name) {
			case "Red":
				_selectEffect = _attachEffects[(int)ATTACH_TYPE.ATTACH_TYPE_RED];
				break;

			case "Blue":
				_selectEffect = _attachEffects[(int)ATTACH_TYPE.ATTACH_TYPE_BLUE];
				break;

			case "Green":
				_selectEffect = _attachEffects[(int)ATTACH_TYPE.ATTACH_TYPE_GREEN];
				break;

			case "Yellow":
				_selectEffect = _attachEffects[(int)ATTACH_TYPE.ATTACH_TYPE_YELLOW];
				break;

			case "Pink":
				_selectEffect = _attachEffects[(int)ATTACH_TYPE.ATTACH_TYPE_PINK];
				break;

			case "Brown":
				_selectEffect = _attachEffects[(int)ATTACH_TYPE.ATTACH_TYPE_BROWN];
				break;

            case "Purple":
                _selectEffect = _attachEffects[(int)ATTACH_TYPE.ATTACH_TYPE_PURPLE];
                break;
        }
	}
}
