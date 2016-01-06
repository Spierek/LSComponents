using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Flags]
public enum ShakeAxes {
	None, X, Y, XY, Z, XZ, YZ, XYZ
}

[RequireComponent(typeof(Camera))]
public class CameraShaker : MonoBehaviour {
	#region Variables
	public static CameraShaker Instance;

	private Camera		camera;
	private Vector3		cameraPos;
	#endregion

	#region Monobehaviour
	private void Awake() {
		Instance = this;

		camera = GetComponent<Camera>();
		cameraPos = camera.transform.localPosition;
	}
	#endregion

	#region Methods
	public void Shake(float duration, float strength, ShakeAxes axes = ShakeAxes.XY) {
		StartCoroutine(ShakeRoutine(duration, strength, axes));
	}

	// TODO: smoother shaking?
	private IEnumerator ShakeRoutine(float duration, float strength, ShakeAxes axes) {
		float timer = 0f;
		Vector3 offset = new Vector3();
		float ratio = 0f;
		
		while (timer < duration) {
			timer += Time.deltaTime;
			ratio = (1 - (timer / duration)) * strength;

			// calculate offset for each axis
			offset = Random.insideUnitSphere;
			offset.x *= ((axes & ShakeAxes.X) == ShakeAxes.X) ? ratio : 0;
			offset.y *= ((axes & ShakeAxes.Y) == ShakeAxes.Y) ? ratio : 0;
			offset.z *= ((axes & ShakeAxes.Z) == ShakeAxes.Z) ? ratio : 0;

			camera.transform.localPosition = cameraPos + offset;

			yield return null;
		}

		camera.transform.localPosition = cameraPos;
	}
	#endregion
}