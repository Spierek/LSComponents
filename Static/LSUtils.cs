using UnityEngine;
using System.Collections;

public class LSUtils : MonoBehaviour {
	#region Methods
	// Spawns an object and parents it to another object, resetting local position / rotation
	public static Transform InstantiateAndParent(GameObject prefab, Transform parent) {
		return InstantiateAndParent(prefab, parent, Vector3.zero);
	}

	public static Transform InstantiateAndParent(GameObject prefab, Transform parent, Vector3 initialPos) {
		Transform obj = (Instantiate(prefab) as GameObject).transform;
		obj.parent = parent;
		obj.localPosition = initialPos;
		obj.localRotation = Quaternion.identity;
		return obj;
	}
	#endregion
}
