using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// マウスでのクリック、ホイール操作を行う
/// </summary>
public class MouseController : MonoBehaviour
{
    [SerializeField]private float _radius = 1.0f;
    [SerializeField]private float _rayDistance = 3.0f;

    private GameObject _playerObject = null;
    private GameObject _cameraObject = null;
    private Transform _cameraTransform = null;

    private GameObject _itemManagerObject = null;
	private ItemManager _itemManager = null;

	private InterfereObject _interfereObject = null;

    private RaycastHit _hit = new RaycastHit();
	private float _mouseWheel = 0.0f;
	private int _layerMask = ~(1 << 2 | 1 << 9);

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
		
		_itemManagerObject = GameObject.Find("ItemManager");
		_itemManager = _itemManagerObject.GetComponent<ItemManager>();
    }

    private void Update()
    {
		//左クリック
        if (Input.GetMouseButtonDown(0)) {
            if (Physics.SphereCast(transform.position, _radius, _cameraTransform.forward, out _hit, _rayDistance, _layerMask)) {
				_interfereObject = _hit.collider.gameObject.GetComponent<InterfereObject>();
				_interfereObject.ObjectAction();
            }
        }

		//右クリック（アイテム使用）
		if (Input.GetMouseButtonDown(1)) {
			Physics.SphereCast(transform.position, _radius, _cameraTransform.forward, out _hit, _rayDistance, _layerMask);
			_itemManager.UseItem();
		}

		//マウスホイール（アイテム切り替え）
		_mouseWheel = Input.GetAxis("Mouse ScrollWheel");

		if (_mouseWheel > 0.0f) {
			_itemManager.ChangeItem(true);
		}

		if (_mouseWheel < 0.0f) {
			_itemManager.ChangeItem(false);
		}
	}
}
