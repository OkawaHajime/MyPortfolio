using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    [SerializeField]private float _radius = 1.0f;
    [SerializeField]private float _rayDistance = 3.0f;

    private GameObject _playerObject = null;
    private GameObject _cameraObject = null;
    private Transform _cameraTransform = null;

    private GameObject _itemManagerObject = null;
	private ItemManager _itemManager = null;

    private Locker _locker = null;
	private Door _door = null;
	private Goal _goal = null;

    private RaycastHit _hit = new RaycastHit();
	private float _mouseWheel = 0.0f;

	public RaycastHit HitObject {
		get {
			return _hit;
		}
	}

    private void Awake()
    {
		//Cameraのforward方向へRayを飛ばすため、Cameraの情報を取得
		_playerObject = transform.root.gameObject;
        _cameraObject = GameObject.Find("PlayerFovCamera");
        _cameraTransform = _cameraObject.GetComponent<Transform>();

		//アイテム管理者の情報を取得
		_itemManagerObject = GameObject.Find("ItemManager");
		_itemManager = _itemManagerObject.GetComponent<ItemManager>();
    }

    private void Update()
    {
		//左クリック
        if (Input.GetMouseButtonDown(0)) {
			//ロッカーから出る
			if(_playerObject.tag == "HideArea") {
				_locker.ExitLocker();
			}

			//Rayを飛ばし、当たったオブジェクトのタグによりアクションを分岐
            if (Physics.SphereCast(transform.position, _radius, _cameraTransform.forward, out _hit, _rayDistance)){
				Debug.Log(_hit.collider.gameObject.tag);
				switch (_hit.collider.gameObject.tag) {
					case "Item":
						_itemManager.GetItem(_hit.collider.gameObject);
						break;

					case "Locker":
						_locker = _hit.collider.gameObject.GetComponent<Locker>();
						_locker.HiddenLocker();
						break;

					case "Door":
						_door = _hit.collider.gameObject.GetComponent<Door>();
						_door.DoorAction();
						break;

					case "Finish":
						_goal = _hit.collider.gameObject.GetComponent<Goal>();
						_goal.GoalCondition();
						break;
				}
            }
        }

		//右クリック（アイテム使用）
		if (Input.GetMouseButtonDown(1)) {
			Physics.SphereCast(transform.position, _radius, _cameraTransform.forward, out _hit, _rayDistance);
			_itemManager.UseItem();
		}

		//マウスホイール（アイテム切り替え）
		_mouseWheel = Input.GetAxis("Mouse ScrollWheel");

		if(_mouseWheel > 0.0f) {
			_itemManager.ChangeItem(true);
		}

		if(_mouseWheel < 0.0f) {
			_itemManager.ChangeItem(false);
		}
	}
}
