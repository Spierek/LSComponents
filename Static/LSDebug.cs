using UnityEngine;
using System.Text;
using System.Collections.Generic;

[RequireComponent(typeof(GUIText))]
public class LSDebug : MonoBehaviour {
	#region Variables
    public bool                     enableDebug = true;

    private static GUIText          textDisplay;
    private static StringBuilder    text;
    private static List<DebugWord>  textList;

    private static Vector3          tempV3;
    #endregion

    #region Monobehaviour Methods
	// Sets default values on script attach
	void Reset() {
		transform.position = new Vector3(0, 1, 0);

        textDisplay = GetComponent<GUIText>();

		textDisplay.text = "debug";
		textDisplay.anchor = TextAnchor.UpperLeft;
		textDisplay.alignment = TextAlignment.Left;
		textDisplay.fontSize = 10;
		textDisplay.color = new Color(0.1f, 0.1f, 0.1f, 0.9f);
	}

    void Awake() {
        textDisplay = GetComponent<GUIText>();
        textDisplay.enabled = enableDebug;
        text = new StringBuilder();
        textList = new List<DebugWord>();
    }

    void LateUpdate() {
        if (Input.GetKeyDown(KeyCode.KeypadMinus)) {
            enableDebug = !enableDebug;
            textDisplay.enabled = enableDebug;
        }

        // display debug text
        if (!enableDebug) return;

        AppendList();
        textDisplay.text = text.ToString();
        text.Remove(0, text.Length);
    }
    #endregion

    #region Methods
    public static void Write(string txt) {
        text.Append(txt);
    }

    public static void Write(string name, string value) {
        text.Append(name);
        text.Append(": ");
        text.Append(value);
    }

    public static void WriteLine(string txt) {
        text.AppendLine(txt);
    }

    public static void WriteLine(string name, string value) {
        text.Append(name);
        text.Append(": ");
        text.AppendLine(value);
    }

    public static void WriteLine(string txt, float duration) {
        textList.Add(new DebugWord(txt, Time.realtimeSinceStartup + duration));
    }

    private void AppendList() {
        for (int i = 0; i < textList.Count; i++) {
            // check if word is still valid
            if (textList[i].time < Time.realtimeSinceStartup) {
                textList.RemoveAt(i);
            }
            if (i < textList.Count)
                text.AppendLine(textList[i].text);
        }
    }
    #endregion
}

public struct DebugWord {
    public string   text;
    public float    time;

    public DebugWord(string text, float time) {
        this.text = text;
        this.time = time;
    }
}