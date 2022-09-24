using UnityEngine;
using System.Collections;
using UnityEditor;

public class MakeScriptableObject
{
    [MenuItem("Assets/Create/Power ups/Power up")]
    public static void CreatePowerUp()
    {
        PowerUp asset = ScriptableObject.CreateInstance<PowerUp>();

        AssetDatabase.CreateAsset(asset, "Assets/Prefabs/PowerUps/NewPowerUp.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }

    /*[MenuItem("Assets/Create/Power ups/Apply stat modifier")]
    public static void CreateApplyStatModifierPowerUp()
    {
        ApplyStatModifierPowerUp asset = ScriptableObject.CreateInstance<ApplyStatModifierPowerUp>();

        AssetDatabase.CreateAsset(asset, "Assets/Prefabs/PowerUps/NewPowerUp.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }*/

    /*[MenuItem("Assets/Create/Power ups/Force field")]
    public static void CreateForceFieldPowerUp()
    {
        ForceFieldPowerUp asset = ScriptableObject.CreateInstance<ForceFieldPowerUp>();

        AssetDatabase.CreateAsset(asset, "Assets/Prefabs/PowerUps/NewPowerUp.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }*/
}