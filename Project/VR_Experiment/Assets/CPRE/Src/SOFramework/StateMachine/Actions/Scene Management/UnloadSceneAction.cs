using CPRE.SOFramework.SceneManagement;
using UnityEngine;

namespace CPRE.SOFramework.StateMachine.Actions.Scene_Management {
    [CreateAssetMenu(fileName = "Enable Game Objects Action", menuName = "State Machine/Actions/Scene Management/Unload Scene", order = 0)]
    public class UnloadSceneAction : Action {
        public ScenesData sceneDatabase;
        public SceneReference scene;

        public override void Act() {
            sceneDatabase.UnloadSceneByIndex(scene.sceneIndex);
        }
    }
}