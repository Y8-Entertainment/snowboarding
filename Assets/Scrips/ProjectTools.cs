#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;

public static class ProjectTools
{
    [MenuItem("Tools/Project/Open Main Scene")]
    public static void OpenMainScene()
    {
        // CHỈNH đường dẫn scene chính của bạn (ví dụ):
        var scenePath = "Assets/Scenes/Level 1.unity";
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            EditorSceneManager.OpenScene(scenePath);
            Debug.Log($"[Tools] Opened scene: {scenePath}");
        }
    }

    [MenuItem("Tools/Project/Clear PlayerPrefs")]
    public static void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("[Tools] Cleared PlayerPrefs.");
    }

    [MenuItem("Tools/Project/Ping PersistentDataPath")]
    public static void PingDataPath()
    {
        Debug.Log($"[Tools] persistentDataPath: {Application.persistentDataPath}");
        EditorUtility.RevealInFinder(Application.persistentDataPath);
    }
}
#endif