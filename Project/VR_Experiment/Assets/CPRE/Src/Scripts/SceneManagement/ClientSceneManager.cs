using CPRE.SOFramework.DataContainers.Variables;
using CPRE.SOFramework.SceneManagement;
using UnityEngine;

namespace CPRE.Scripts.SceneManagement {
    public class ClientSceneManager : MonoBehaviour
    {
        [SerializeField] private ScenesData setupScenesDatabase;
        [SerializeField] private ScenesData experimentSceneDatabase;
        [SerializeField] private ScenesData treatmentScenesDatabase;
    
        [SerializeField] private IntReference treatmentTypeReference;
    
        // Start is called before the first frame update
        void Start() {
            setupScenesDatabase.LoadSceneByIndex(0);
            setupScenesDatabase.LoadSceneByIndex(1);
        }

        public void OnStartExperiment() {
            Debug.Log("Starting Experiment...");

            Debug.Log("Unloading Active Setup Scenes...");
            setupScenesDatabase.UnloadActiveScenes();

            Debug.Log("Loading Experiment Scenes...");
            experimentSceneDatabase.LoadSceneByIndex(0);
            Debug.Log("Loaded Experiment Scene at index 0");

            experimentSceneDatabase.LoadSceneByIndex(1);
            Debug.Log("Loaded Experiment Scene at index 1");

            Debug.Log($"Loading Treatment Scene with Treatment Type: {treatmentTypeReference.Value}");
            treatmentScenesDatabase.LoadSceneByIndex(treatmentTypeReference.Value);
        }
    }
}
