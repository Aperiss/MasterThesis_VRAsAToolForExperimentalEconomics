using CPRE.SOFramework.SceneManagement;
using UnityEngine;

namespace CPRE.SOFramework.StateMachine.Actions.Scene_Management {
    [CreateAssetMenu(fileName = "Enable Game Objects Action", menuName = "State Machine/Actions/Scene Management/Load Scene", order = 0)]
    public class LoadSceneAction : Action {
        public ScenesData sceneDatabase;
        public SceneReference scene;

        public override void Act() {
            sceneDatabase.LoadSceneByIndex(scene.sceneIndex);
            UnityEngine.Debug.Log("Loading scene: " + scene.sceneName);
        }
    }
}