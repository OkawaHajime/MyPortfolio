using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class ItemManager : MonoBehaviour
{
    [SerializeField]private Text _itemUI = null;
	[SerializeField]private Text _itemNumberUI = null;
	[SerializeField]private List<Item> _possessionItems = new List<Item>();
    [SerializeField]private AudioClip _takeItem = null;
    [SerializeField]private AudioClip _takeClearItem = null;
    [SerializeField]private AudioClip _useFlashlight = null;
    [SerializeField]private AudioClip _useMatchstick = null;
    [SerializeField]private AudioClip _usePill = null;

	private GameObject _initialEquipment = null;
    private GameObject _possessionItemUI = null;
	private Animator _riseItem = null;
	private Item _item = null;
	private Subject<Unit> _changeItem = new Subject<Unit>();
	private int _itemNumber = 0;
    private AudioSource _audioSource = null;

	public Subject<Unit> SwitchItem {
		get {
			return _changeItem;
		}
	}

	private void Awake()
	{
		_initialEquipment = GameObject.Find("FlashLight");
		
		_possessionItemUI = GameObject.Find("PossessionItems");
        _audioSource = GetComponent<AudioSource>();
	}

	private void Start()
	{
		ResetDisplayItem();

		if (TutorialSceneManager.TutorialFinish) {
			GetItem(_initialEquipment);
		}
	}

	public void GetItem(GameObject item)
	{
		_item = item.GetComponent<Item>();
		
		if (_possessionItems.Count < 3 && item.name != "KeyItem") {
			_changeItem.OnNext(Unit.Default);

			_possessionItems.Add(_item);
			_itemNumber = _possessionItems.Count - 1;
			DisplayItem(_itemNumber);
			
			_item.ItemPick();
            _audioSource.PlayOneShot(_takeItem);
        }
		else if (item.name == "KeyItem") {
			_item.ItemPick();
            _audioSource.PlayOneShot(_takeClearItem);
		}

        _itemUI.text = "持てるあいてむ " + _possessionItems.Count.ToString() + " / 3";
	}
	
	public void DisposeItem()
	{
		if (_possessionItems.Count != 0) {
			_possessionItems[_itemNumber].ItemDrop();
			_possessionItems.Remove(_possessionItems[_itemNumber]);
			
			if (_possessionItems.Count == 0) {
				//ライトの関係上、OnNextしてからアイテム表示を消す
				_changeItem.OnNext(Unit.Default);
				ResetDisplayItem();
			}
			else {
				ChangeItem(true);
			}

            _itemUI.text = "持てるあいてむ" + _possessionItems.Count.ToString() + " / 3";
        }
    }
	
	public void UseItem()
	{
		if (_possessionItems.Count != 0) {
			_possessionItems[_itemNumber].ItemEffect();

			if (_possessionItems[_itemNumber].name == "FlashLight") {
				_audioSource.PlayOneShot(_useFlashlight);
			}

			if (_possessionItems[_itemNumber].name == "MatchBox") {
				_audioSource.PlayOneShot(_useMatchstick);
			}

			if (_possessionItems[_itemNumber].name == "Tranquilizer") {
				_audioSource.PlayOneShot(_usePill);
			}
		}
	}

	public void ChangeItem(bool up)
	{
		_changeItem.OnNext(Unit.Default);
		
		if (_possessionItems.Count != 0) {
			if (up) {
				_itemNumber++;
				
				if(_itemNumber > _possessionItems.Count - 1){
					_itemNumber = 0;
				}
			}
			else {
				_itemNumber--;
				
				if (_itemNumber < 0){
					_itemNumber = _possessionItems.Count - 1;
				}
			}

			DisplayItem(_itemNumber);
            _audioSource.PlayOneShot(_takeItem);
		}
	}
	
	private void DisplayItem(int itemNumber)
	{
		ResetDisplayItem();
		
		foreach (Transform item in _possessionItemUI.transform) {
			if(item.gameObject.name == _possessionItems[itemNumber].name) {
				item.gameObject.SetActive(true);
				
                _riseItem = item.GetComponentInChildren<Animator>();
				_riseItem.Play("RiseItem");
				break;
			}
		}
	}

	private void ResetDisplayItem()
	{
		foreach (Transform child in _possessionItemUI.transform) {
			child.gameObject.SetActive(false);
		}
	}
}
