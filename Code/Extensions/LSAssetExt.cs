using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

public class LSAssetExt : MonoBehaviour
{
    // by @glitchers via http://answers.unity.com/answers/1216386/view.html
    public static List<T> FindAssetsByType<T>() where T : Object
    {
        List<T> assets = new List<T>();
        string[] guids = AssetDatabase.FindAssets(string.Format("t:{0}", typeof(T)));
        for (int i = 0; i < guids.Length; i++)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath( guids[i] );
            T asset = AssetDatabase.LoadAssetAtPath<T>( assetPath );
            if (asset != null)
            {
                assets.Add(asset);
            }
        }
        return assets;
    }
}
