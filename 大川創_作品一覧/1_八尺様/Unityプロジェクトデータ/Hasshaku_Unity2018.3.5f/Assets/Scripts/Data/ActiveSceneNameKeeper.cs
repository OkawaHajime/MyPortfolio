using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DataAsset/ActiveSceneNameKeeper")]
public class ActiveSceneNameKeeper : ScriptableObject {
	public string scene_name { get; set; } = "Title";
}