using CPRE.SOFramework.SceneManagement;
using UnityEngine;

namespace CPRE.Scripts.SceneManagement {
    public class InitialSceneLoader : MonoBehaviour {
        [SerializeField] private ScenesData sceneDatabase;
    
        void Start() {
            sceneDatabase.LoadSceneByIndex(0);
            sceneDatabase.LoadSceneByIndex(1);
        }
    }
}
