using System.Collections.Generic;
using CPRE.SOFramework.DataContainers.Sets;
using CPRE.SOFramework.DataContainers.Variables;
using UnityEngine;

namespace CPRE.Scripts.Environment {
    public class CommonPoolInstanceHandler : MonoBehaviour {
        [SerializeField] protected List<GameObject> prefabs;

        [SerializeField] private Vector3RuntimeSet resourcePositions;
        [SerializeField] private BoolRuntimeSet resourceAvailabilitySet;
        [SerializeField] private IntReference capacity;
        
        [SerializeField] private Vector2 sizeVariation = new Vector2(0.8f, 1.2f);
        [SerializeField] private Vector2 rotationVariation = new Vector2(0.8f, 1.2f);
        [SerializeField] private Vector2 positionVariation = new Vector2(0.8f, 1.2f);
        
        void Start() 
        {
            PoolInstances(capacity);
            for(int i = 0; i < _instances.Count; i++)
                _instances[i].transform.parent = this.transform;
        }		
        
        private void Update() {
            for (int i = 0; i < resourceAvailabilitySet.Items.Count; i++) 
            {
                if (!resourceAvailabilitySet.Items[i])
                {
                    _instances[i].gameObject.SetActive(false);
                }
                else 
                {
                    if(!_instances[i].gameObject.activeSelf) 
                    {
                        _instances[i].gameObject.SetActive(true);
					
                        float x =  resourcePositions.Items[i].x + Random.Range(positionVariation.x, positionVariation.y);
                        float y = resourcePositions.Items[i].y;
                        float z =  resourcePositions.Items[i].z + Random.Range(positionVariation.x, positionVariation.y);

                        _instances[i].transform.position = new Vector3(x, y, z);
                        _instances[i].transform.rotation = Quaternion.Euler(0f, Random.Range(rotationVariation.x, rotationVariation.y), 0f);
                        _instances[i].transform.localScale = Vector3.one * Random.Range(sizeVariation.x, sizeVariation.y);
                    }
                }
            }
        }
        
        protected List<GameObject> _instances = new List<GameObject>();

        protected void PoolInstances(int poolSize) {
            for (int i = 0; i < poolSize; i++) {
                int resourceType = Random.Range(0, prefabs.Count);
                var instance = Instantiate(prefabs[resourceType]);
                
                instance.gameObject.SetActive(false);
                _instances.Add(instance);
            }
        }
        
    }
}