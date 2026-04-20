#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public static class AutoBootstrap
{
    public static int currentSceneIndex;
    static AutoBootstrap()
    {
        Debug.Log("AutoBootstrap INIT");

        EditorApplication.playModeStateChanged += OnPlayModeChanged;
    }

    static void OnPlayModeChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingEditMode)
        {
            currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            if (currentSceneIndex != 0)
            {
                EditorSceneManager.OpenScene("Assets/_game/Scenes/BootstrapScene.unity");
            }
        }
        if (state == PlayModeStateChange.ExitingPlayMode)
        {
            EditorSceneManager.LoadScene(SceneManager.GetSceneByBuildIndex(currentSceneIndex).path);
        }
    }
}
#endif