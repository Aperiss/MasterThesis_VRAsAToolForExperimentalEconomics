using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CPRE.SOFramework.SceneManagement {
    [CreateAssetMenu(fileName = "scene Database", menuName = "Scene Data/Scene Database")]
    public class ScenesData : ScriptableObject {
        
        public List<SceneReference> scenes = new List<SceneReference>();
        public List<SceneReference> activeScenes = new List<SceneReference>();

        public void OnEnable() {
            activeScenes.Clear();
        }
        
        public void LoadSceneByName(string sceneName, bool loadAdditively = true) {
            foreach (var scene in scenes) {
                if (scene.sceneName == sceneName) {
                    if(activeScenes.Contains(scene)) continue;
                    LoadSceneMode loadMode = loadAdditively ? LoadSceneMode.Additive : LoadSceneMode.Single;
                    SceneManager.LoadSceneAsync(scene.sceneName, loadMode);
                    activeScenes.Add(scene);
                }
            }
        }
        
        public void LoadSceneByIndex(int index, bool loadAdditively = true) {
            foreach (var scene in scenes) {
                if (scene.sceneIndex == index) {
                    if(activeScenes.Contains(scene)) continue;
                    LoadSceneMode loadMode = loadAdditively ? LoadSceneMode.Additive : LoadSceneMode.Single;
                    SceneManager.LoadSceneAsync(scene.sceneName, loadMode);
                    activeScenes.Add(scene);
                }
            }
        }
        
        public void UnloadSceneByIndex(int index) {
            foreach (var scene in scenes) {
                if (scene.sceneIndex == index) {
                    SceneManager.UnloadSceneAsync(scene.sceneName);
                    activeScenes.Remove(scene);
                }
            }
        }

        public void UnloadActiveScenes() {
            for (int i = activeScenes.Count - 1; i >= 0; i--) {
                SceneManager.UnloadSceneAsync(activeScenes[i].sceneName);
                activeScenes.RemoveAt(i);
            }
        }
    }
}