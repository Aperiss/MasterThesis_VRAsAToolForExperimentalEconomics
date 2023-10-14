using CPRE.SOFramework.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CPRE.Scripts.SceneManagement {
    public class SimpleSceneManager : MonoBehaviour {
        [SerializeField] private ScenesData serverSceneDatabase;
        [SerializeField] private ScenesData clientSceneDatabase;
        [SerializeField] private bool isServer;
        
        private void Awake() {
            if(isServer) {
                LoadServerScene();
            } else {
                LoadClientScene();
            }
        }
    
        public void LoadServerScene() {
            serverSceneDatabase.LoadSceneByIndex(0);
            serverSceneDatabase.LoadSceneByIndex(1);
        }

        public void LoadClientScene() {
            clientSceneDatabase.LoadSceneByIndex(0);
        }
    }
}
