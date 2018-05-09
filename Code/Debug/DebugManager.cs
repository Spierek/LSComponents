using LSTools;
using UnityEngine;

public class DebugManager : ASingleton<DebugManager>
{
#if UNITY_EDITOR
    private static readonly Vector2 TIMESCALE_RANGE = new Vector2(0.125f, 8f);

    private void Update()
    {
        UpdateTimescale();
    }

    private void UpdateTimescale()
    {
        if (Input.GetKeyDown(KeyCode.PageUp))
        {
            MultiplyTimescale(2);
        }
        else if (Input.GetKeyDown(KeyCode.PageDown))
        {
            MultiplyTimescale(0.5f);
        }
    }

    private void MultiplyTimescale(float mod)
    {
        float newTimescale = Time.timeScale * mod;
        if (newTimescale >= TIMESCALE_RANGE.x && newTimescale <= TIMESCALE_RANGE.y)
        {
            Time.timeScale = newTimescale;
            Debug.Log("Current timescale: " + Time.timeScale);
        }
    }
#endif
}
