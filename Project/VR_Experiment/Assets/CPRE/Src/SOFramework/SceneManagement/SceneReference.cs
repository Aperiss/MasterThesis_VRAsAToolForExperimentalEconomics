using UnityEngine;

namespace CPRE.SOFramework.SceneManagement {
    [CreateAssetMenu(fileName = "Scene Reference", menuName = "Scene Data/Scene Reference")]
    public class SceneReference : ScriptableObject {
        [Header("Information")]
        public string sceneName;
        public int sceneIndex;
        public string shortDescription;
    }
}