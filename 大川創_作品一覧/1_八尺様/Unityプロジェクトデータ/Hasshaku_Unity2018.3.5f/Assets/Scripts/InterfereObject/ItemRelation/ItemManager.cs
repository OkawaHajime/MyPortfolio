using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

/// <summary>
/// アイテムの保持状態を管理する
/// </summary>
public class ItemManager : MonoBehaviour
{
    [SerializeField] private Text _itemUI = null;
	[SerializeField] private Text _itemNumberUI = null;
	[SerializeField] private List<Item> _possessionItems = new List<Item>();
    [SerializeField] private AudioClip _takeItem = null;
    [SerializeField] private AudioClip _takeClearItem = null;
    [SerializeField] private AudioClip _useFlashlight = null;
    [SerializeField] private AudioClip _useMatchstick = null;
    [SerializeField] private AudioClip _usePill = null;

	private GameObject _initialEquipment = null;
    private GameObject _possessionItemUI = null;
	private GameObject _getItemUI = null;
	private Animator _riseItem = null;
	private Item _item = null;
	private Subject<Unit> _changeItem = new Subject<Unit>();
	private int _itemNumber = 0;
    private bool _initialSetting = false;
	
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

		_getItemUI = GameObject.Find("GetItemTexts");

        _audioSource = GetComponent<AudioSource>();
	}

	private void Start()
	{
		ResetDisplayItem();

		if (TutorialSceneManager.TutorialFinish) {
			GetItem(_initialEquipment);
		}
	}

	//アイテム取得
	public void GetItem(GameObject item)
	{
		_item = item.GetComponent<Item>();

		if (_possessionItems.Count < 3 && item.name != "KeyItem") {
			_possessionItems.Add(_item);
            _audioSource.PlayOneShot(_takeItem);

			//初期装備でライトを取得
			if (!_initialSetting) {
				_initialSetting = true;
				_itemNumber = 0;
				DisplayItem(_itemNumber);
			}

			//何のアイテムを拾ったかテキストで表示
			foreach(Transform getItemText in _getItemUI.transform) {
				getItemText.gameObject.SetActive(false);
			}
			switch (item.name) {
				case "FlashLight":
					_getItemUI.transform.GetChild(0).gameObject.SetActive(true);
					break;

				case "MatchBox":
					_getItemUI.transform.GetChild(1).gameObject.SetActive(true);
					break;

				case "Tranquilizer":
					_getItemUI.transform.GetChild(2).gameObject.SetActive(true);
					break;
			}
        }
		//キーアイテムの時、専用のSEを再生
		else if (item.name == "KeyItem") {
            _audioSource.PlayOneShot(_takeClearItem);
		}

		//現在のアイテム所持数を表示
        _itemUI.text = "持てるあいてむ " + _possessionItems.Count.ToString() + " / 3";
	}
	
	//画面に表示されているアイテムの効果を発揮
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

	//アイテムをその場に置く
	public void DisposeItem()
	{
		if (_possessionItems.Count != 0) {
			_possessionItems[_itemNumber].ItemDrop();
			_possessionItems.Remove(_possessionItems[_itemNumber]);
			
			//ライトの関係上、アイテム変更の通知を出してからアイテム表示を消す
			if (_possessionItems.Count == 0) {
				_changeItem.OnNext(Unit.Default);
				ResetDisplayItem();
			}
			else {
				ChangeItem(true);
			}

            _itemUI.text = "持てるあいてむ" + _possessionItems.Count.ToString() + " / 3";
        }
    }
	
	//アイテムの変更
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
	
	//特定のアイテムを表示
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

	//画面に表示されているアイテムを全て非表示にする
	private void ResetDisplayItem()
	{
		foreach (Transform child in _possessionItemUI.transform) {
			child.gameObject.SetActive(false);
		}
	}
}
