using System.Collections.Generic;
using UnityEngine;

namespace CPRE.Scripts.VRInteraction {
	public class InstanceHandler : MonoBehaviour{
		
		[SerializeField] protected List<GameObject> prefabs;
		
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