using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LSGamepad : MonoBehaviour {
	#region Variables
	private static LSGamepad m_Instance;
	
	public static float 	leftStickDelta = 0;
	private static float 	prevAngle = 0;

	private static string 	defHorizontalAxis = "HorizontalGP";
	private static string 	defVerticalAxis = "VerticalGP";
	#endregion

	#region Singleton
	[RuntimeInitializeOnLoadMethod]
	private static void CreateSingleton() {
		if (m_Instance == null) {
			GameObject go = new GameObject("LSGamepad");
			m_Instance = go.AddComponent<LSGamepad>();
			DontDestroyOnLoad(go);
			go.hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector;

			m_Instance.Initialize();
		}
	}

	private void Initialize() {
		prevAngle = GetStickAngle();
	}
	#endregion

	#region MonoBehaviour
	private void LateUpdate() {
		CalculateStickDelta();

		LSDebug.WriteLine("Stick Angle", GetStickAngle().ToString());
	}
	#endregion

	#region Methods
	// Returns left stick angle from -180 to 180 (default set to 0)
	public static float GetStickAngle() {
		return GetStickAngle(Input.GetAxisRaw(defHorizontalAxis), Input.GetAxisRaw(defVerticalAxis));
	}

	public static float GetStickAngle(float axisHorizontal, float axisVertical) {
		float result = Mathf.Atan2(axisHorizontal, axisVertical) * Mathf.Rad2Deg;
		return result;
	}

	// Same as GetStickAngle() but takes dead zones into account
	public static bool GetStickAngleDZ(float deadZone, ref float angle) {
		float hor = Input.GetAxisRaw(defHorizontalAxis);
		float vert = Input.GetAxisRaw(defVerticalAxis);

		if (Mathf.Abs(hor) < deadZone && Mathf.Abs(vert) < deadZone)
			return false;

		angle = GetStickAngle(hor, vert);
		return true;
	}

	// Returns left stick rotation delta in current frame
	private void CalculateStickDelta() {
		float angle = 0f;
		GetStickAngleDZ(0.1f, ref angle);

		if (Mathf.Abs(angle) > 90f) {
			// check if 180/-180 threshold was crossed
			float prevSign = Mathf.Sign(prevAngle);
			if (Mathf.Sign(angle) != prevSign) {
				prevAngle -= (prevSign > 0) ? 360 : -360;
			}
		}

		leftStickDelta = angle - prevAngle;

		LSDebug.WriteLine("Delta", leftStickDelta.ToString());
		prevAngle = angle;
	}
	#endregion
}