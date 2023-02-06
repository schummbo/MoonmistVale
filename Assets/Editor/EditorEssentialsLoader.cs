using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace Assets.Editor
{
    [InitializeOnLoad]
    public static class EditorEssentialsLoader
    {
        static EditorEssentialsLoader()
        {
            UnityEditor.SceneManagement.EditorSceneManager.sceneOpened += SceneOpenedCallback;
        }

        private static void SceneOpenedCallback(Scene scene, OpenSceneMode mode)
        {
            if (!EditorSceneManager.GetSceneByName("Essentials").isLoaded)
            {
                EditorSceneManager.OpenScene("Assets/Scenes/Essentials.unity", OpenSceneMode.Additive);
            }
        }
    }
}
