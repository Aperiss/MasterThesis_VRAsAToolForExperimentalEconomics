using System.Collections.Generic;
using System.Linq;
using CPRE.SOFramework.DataContainers.Sets;
using CPRE.SOFramework.DataContainers.Variables;
using UnityEngine;

namespace CPRE.Scripts.VRInteraction {
	public class StockpileInstanceHandler : InstanceHandler {

		[SerializeField] private BoolRuntimeSet resourceAvailabilitySet;
		[SerializeField] private IntReference capacity;
		[SerializeField] private IntReference initialResources;

		[SerializeField] private Vector2 depthVariation = new Vector2(0f, 0f);
		[SerializeField] private Vector2 positionVariation = new Vector2(0f, 0f);
		[SerializeField] private Vector2 rotationVariation = new Vector2(0f, 0f);
		[SerializeField] private List<StockpileSlot> logSlots;

		private void Start() {

			PoolInstances(capacity);
			
			resourceAvailabilitySet.Clear();
			resourceAvailabilitySet.Items = Enumerable.Range(0, capacity).Select(x => x >= capacity - initialResources).ToList();
		}

		private void Update() {

			for (int i = 0; i < resourceAvailabilitySet.Items.Count; i++) {
				if (resourceAvailabilitySet.Items[i]) {
					if (!_instances[i].activeSelf) {
						_instances[i].gameObject.transform.localScale = Vector3.one;
						_instances[i].gameObject.SetActive(true);
						
						_instances[i].GetComponent<Log>().logIndex = i;
						
						var randomSlot = GetRandomAvailableSlot();
						logSlots[randomSlot].attachedLog = _instances[i].gameObject;
						logSlots[randomSlot].isAvailable = false;

						_instances[i].transform.parent = logSlots[randomSlot].transform;
						_instances[i].GetComponent<Log>().rootTransform = logSlots[randomSlot].transform;
						
						SetRandomOffsets(_instances[i].GetComponent<Log>());
						_instances[i].GetComponent<Log>().SnapToRootTransform();
					}
				}
				else {
					_instances[i].gameObject.SetActive(false);
					_instances[i].transform.parent = null;
					UpdateAvailableSlots();
				}
			}
		}
		
		private int GetRandomAvailableSlot() {
			
			if (logSlots.Count != capacity) {
				Debug.Log("Make sure there is the same number of slots as stockpile capacity");
				return -1;
			}
			
			List<int> availableSlots = new List<int>();
			for (int i = 0; i < logSlots.Count; i++) {
				if (logSlots[i].isAvailable) {
					availableSlots.Add(i);
				}
			}
			if (availableSlots.Count > 0) {
				var index = Random.Range(0, availableSlots.Count);
				return availableSlots[index];
			}
			return -1;
		}

		private void UpdateAvailableSlots() 
		{
			if (logSlots.Count != capacity) {
				Debug.Log("Make sure there is the same number of slots as stockpile capacity");
				return;
			}
			for (int i = 0; i < logSlots.Count; i++) {
				if (!logSlots[i].isAvailable && !logSlots[i].attachedLog.activeSelf) {
					logSlots[i].isAvailable = true;
					logSlots[i].attachedLog = null;
				}
			}
		}

		private void SetRandomOffsets(Log instance) {
			
			var randomPositionOffset = new Vector3(Random.Range(depthVariation.x, depthVariation.y), 0f,
				Random.Range(positionVariation.x, positionVariation.y));
			var randomRotationOffset =
				Quaternion.Euler(new Vector3(0f, 0f, Random.Range(rotationVariation.x, rotationVariation.y)));
					
			instance.positionOffset = randomPositionOffset;
			instance.rotationOffset = randomRotationOffset;
		}
		
	}
}