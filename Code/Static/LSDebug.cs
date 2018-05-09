using UnityEngine;
using System.Text;
using System.Collections.Generic;

public struct DebugWord
{
    public string   text;
    public float    time;

    public DebugWord(string text, float time)
    {
        this.text = text;
        this.time = time;
    }
}

[RequireComponent(typeof(GUIText))]
public class LSDebug : MonoBehaviour
{
    #region Variables
    private static LSDebug          m_Instance;

    private static bool             enableDebug = false;

    private static GUIText          textDisplay;
    private static StringBuilder    text;
    private static List<DebugWord>  textList;

    private static StringBuilder    tempSB = new StringBuilder();
    private static Vector3          tempV3;
    #endregion

    #region Singleton
    [RuntimeInitializeOnLoadMethod]
    private static void CreateSingleton()
    {
        if (m_Instance == null)
        {
            GameObject go = new GameObject("LSDebug");
            m_Instance = go.AddComponent<LSDebug>();
            DontDestroyOnLoad(go);
            go.hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector;

            m_Instance.Reset();
        }
    }
    #endregion

    #region Monobehaviour
    // Sets default values on script attach
    void Reset()
    {
        transform.position = new Vector3(0, 1, 0);

        textDisplay = GetComponent<GUIText>();

        textDisplay.text = "debug";
        textDisplay.anchor = TextAnchor.UpperLeft;
        textDisplay.alignment = TextAlignment.Left;
        textDisplay.fontSize = 10;
        textDisplay.color = new Color(0.1f, 0.1f, 0.1f, 0.9f);

        textDisplay = GetComponent<GUIText>();
        textDisplay.enabled = enableDebug;
        text = new StringBuilder();
        textList = new List<DebugWord>();
    }

    void LateUpdate()
    {
        CheckEnabled();
        if (!enableDebug) return;

        AppendList();
        textDisplay.text = text.ToString();
        text.Remove(0, text.Length);
    }
    #endregion

    #region Methods
    // ENABLE
    public static void SetEnabled(bool set)
    {
        enableDebug = !enableDebug;
        textDisplay.enabled = enableDebug;
    }

    private void CheckEnabled()
    {
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            SetEnabled(enableDebug);
        }
    }

    // DISPLAY
    public static void Write(string txt)
    {
        text.Append(txt);
    }

    public static void Write(string name, string value)
    {
        text.Append(PrepareLine(name, value));
    }

    public static void WriteLine(string txt)
    {
        text.AppendLine(txt);
    }

    public static void WriteLine(string name, string value)
    {
        text.AppendLine(PrepareLine(name, value));
    }

    public static void WriteLine(string txt, float duration)
    {
        textList.Add(new DebugWord(txt, Time.realtimeSinceStartup + duration));
    }

    public static void WriteLine(string txt, string value, float duration)
    {
        string str = PrepareLine(txt, value);
        DebugWord d = new DebugWord(str, Time.realtimeSinceStartup + duration);
        textList.Add(d);
    }

    private static string PrepareLine(string name, string value)
    {
        tempSB.Remove(0, tempSB.Length);        // clear
        tempSB.Append(name);
        tempSB.Append(": ");
        tempSB.Append(value);

        return tempSB.ToString();
    }

    private void AppendList()
    {
        for (int i = 0; i < textList.Count; i++)
        {
            // check if word is still valid
            if (textList[i].time < Time.realtimeSinceStartup)
            {
                textList.RemoveAt(i);
            }
            if (i < textList.Count)
            {
                text.AppendLine(textList[i].text);
            }
        }
    }
    #endregion
}