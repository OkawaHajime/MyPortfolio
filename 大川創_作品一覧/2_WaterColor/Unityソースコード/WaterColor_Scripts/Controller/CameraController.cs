using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	[SerializeField] private List<Transform> _subCameras = new List<Transform>();
	[SerializeField] private List<float> RATE = new List<float>();
	private Transform _mainCamera = null;
	private Vector3 _mainCamera_start = new Vector3();
	private Vector3[] _subCameras_start;
	private Vector3 positionPlus = new Vector3();
	private int length = 0;

	private void Awake()
	{
		length = _subCameras.Count;
		_subCameras_start = new Vector3[length];
		_mainCamera = GetComponent<Transform>();
		_mainCamera_start = _mainCamera.position;

		for (int i = 0; i < length; i++) {
			_subCameras_start[i] = _subCameras[i].position;
		}
	}

	private void Update()
	{
		for (int i = 0; i < length; i++) {
			positionPlus.x = (_mainCamera.position.x - _mainCamera_start.x) * RATE[i];
			positionPlus.y = _mainCamera.position.y - _mainCamera_start.y;
			_subCameras[i].position = _subCameras_start[i] + positionPlus;
		}
	}
}
