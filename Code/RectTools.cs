using UnityEngine;

public static class RectTools
{
    public static Rect[] SplitHorizontal(this Rect rect)
    {
        return SplitHorizontal(rect, new float[] { 0.5f });
    }

    public static Rect[] SplitHorizontal(this Rect rect, float ratio)
    {
        return SplitHorizontal(rect, new float[] { ratio });
    }

    /// <summary>
    /// Splits Rect based on supplied ratios
    /// </summary>
    public static Rect[] SplitHorizontal(this Rect rect, params float[] ratios)
    {
        int count = ratios.Length + 1;
        Rect[] result = new Rect[count];

        float currentPos = 0;
        for (int i = 0; i < count; ++i)
        {
            float ratio = (i < count - 1) ? ratios[i] : 1 - currentPos;     // for final index, get remaining width
            result[i] = new Rect(rect.x + currentPos * rect.width, rect.y, rect.width * ratio, rect.height);
            currentPos += ratio;
        }

        return result;
    }

    public static Rect[] SplitVertical(this Rect rect)
    {
        return SplitVertical(rect, new float[] { 0.5f });
    }

    public static Rect[] SplitVertical(this Rect rect, float ratio)
    {
        return SplitVertical(rect, new float[] { ratio });
    }

    /// <summary>
    /// Splits Rect based on supplied ratios
    /// </summary>
    public static Rect[] SplitVertical(this Rect rect, params float[] ratios)
    {
        int count = ratios.Length + 1;
        Rect[] result = new Rect[count];

        float currentPos = 0;
        for (int i = 0; i < count; ++i)
        {
            float ratio = (i < count - 1) ? ratios[i] : 1 - currentPos;     // for final index, get remaining width
            result[i] = new Rect(rect.x, rect.y + currentPos * rect.height, rect.width, rect.height * ratio);
            currentPos += ratio;
        }

        return result;
    }
}
