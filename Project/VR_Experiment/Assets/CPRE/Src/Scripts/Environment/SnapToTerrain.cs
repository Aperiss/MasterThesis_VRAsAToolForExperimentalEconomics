using UnityEngine;

namespace CPRE.Scripts.Environment {
    [ExecuteInEditMode]
    public class SnapToTerrain : MonoBehaviour {
        [SerializeField] private Terrain terrain;
       
        void OnEnable()
        {
            SnapChildrenToTerrain();
        }

        // Call this function whenever you want to snap children to the terrain
        public void SnapChildrenToTerrain()
        {
            if (terrain == null)
            {
                Debug.LogWarning("No Terrain found in the scene.");
                return;
            }

            // Snap each child to terrain
            foreach (Transform child in transform)
            {
                SnapObjectToTerrain(child, terrain);
            }
        }

        private void SnapObjectToTerrain(Transform obj, Terrain terrain)
        {
            // Get terrain height at object's position
            float terrainHeight = terrain.SampleHeight(obj.position);

            // Create new position
            Vector3 newPosition = new Vector3(obj.position.x, terrainHeight + terrain.transform.position.y, obj.position.z);

            // Snap object to terrain
            obj.position = newPosition;
        }
    }
}
