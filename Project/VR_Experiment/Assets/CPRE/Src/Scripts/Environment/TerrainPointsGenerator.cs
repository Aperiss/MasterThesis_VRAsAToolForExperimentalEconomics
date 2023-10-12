using System.Linq;
using CPRE.SOFramework.DataContainers.Sets;
using CPRE.SOFramework.DataContainers.Variables;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CPRE.Scripts.Environment {
    public class TerrainPointsGenerator : MonoBehaviour
    {
        public Texture2D rejectionTexture;
        public Terrain terrain;
        public float minDistance;

        [SerializeField] private Vector3Reference playerOrigin;
    
        [SerializeField] private Vector3RuntimeSet points;
        [SerializeField] private BoolRuntimeSet resourceAvailability;
        [SerializeField] private IntReference capacity;
        [SerializeField] private IntReference initialResources;
    

        public void Start() {
            GenerateResourceAvailabilitySet();
            GeneratePoints();
        }

        public void GeneratePoints()
        {
            points.Clear();
            int counter = 0;
            while(points.Items.Count < capacity) {
                counter++;
                if (counter > 1000000) break;
            
                Vector3 potentialPoint = GetRandomPointOnTerrain();

                float textureValue = GetValueFromTextureAtPoint(potentialPoint);
                if(Random.value > textureValue)
                {
                    continue;
                }

                if(IsFarEnough(potentialPoint))
                {
                    points.Add(potentialPoint);
                }
            }
        
            points.Items.Sort(
                delegate(Vector3 a, Vector3 b) {
                    return Vector3.Distance(playerOrigin.Value, a).CompareTo(Vector3.Distance(playerOrigin.Value, b));
                });
        }

        private Vector3 GetRandomPointOnTerrain() {
            float x = terrain.transform.position.x + Random.value * terrain.terrainData.size.x;
            float z = terrain.transform.position.z + Random.value * terrain.terrainData.size.z;
            float y = terrain.SampleHeight(new Vector3(x, 0, z)) + terrain.transform.position.y;
            return new Vector3(x, y, z);
        }

        private float GetValueFromTextureAtPoint(Vector3 point)
        {
            Vector3 localPoint = point - terrain.transform.position;
            int textureX = (int)((localPoint.x / terrain.terrainData.size.x) * rejectionTexture.width);
            int textureY = (int)((localPoint.z / terrain.terrainData.size.z) * rejectionTexture.height);
            Color textureColor = rejectionTexture.GetPixel(textureX, textureY);
            return textureColor.grayscale;
        }
        private bool IsFarEnough(Vector3 point)
        {
            foreach(Vector3 existingPoint in points.Items)
            {
                if(Vector3.Distance(existingPoint, point) < minDistance)
                {
                    return false;
                }
            }

            return true;
        }
		
        private void GenerateResourceAvailabilitySet() {
			
            resourceAvailability.Clear();
            resourceAvailability.Items = Enumerable.Range(0, capacity.Value).Select(x => x >= capacity.Value - initialResources.Value).ToList();
        }
    }
}
