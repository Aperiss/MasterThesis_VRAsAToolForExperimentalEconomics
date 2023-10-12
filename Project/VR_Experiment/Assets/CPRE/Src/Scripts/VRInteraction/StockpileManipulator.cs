using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CPRE.SOFramework.DataContainers.Sets;
using CPRE.SOFramework.EventSystem.Channels;
using UnityEngine;

namespace CPRE.Scripts.VRInteraction {
    public class StockpileManipulator : MonoBehaviour
    {	
        [SerializeField] private BoolRuntimeSet resourceAvailabilitySet;
        [SerializeField] private VoidEventChannel allLogsManufacturedEventChannel;
    
        public void AddLogsToStockpile(int numberOfLogs)
        {
            for (int i = 0; i < numberOfLogs; i++) 
            {
                var randomAvailableResourceSlot = FindRandomAvailableResourceSlot();
                if (randomAvailableResourceSlot < 0)
                    return;
			
                resourceAvailabilitySet.Items[randomAvailableResourceSlot] = true;
            }
        }

        public void ClearStockpile()
        {
            for (int i = 0; i < resourceAvailabilitySet.Items.Count; i++)
            {
                resourceAvailabilitySet.Items[i] = false;
            }
        }
        
        public void RemoveLogsFromStockpile(int numberOfLogs)
        {
            for (int i = 0; i < numberOfLogs; i++) 
            {
                var randomUnavailableResourceSlot = FindRandomUnavailableResourceSlot();
                if (randomUnavailableResourceSlot < 0) {
                    StartCoroutine(NotifyAllLogsManufactured());
                    return;
                }
			
                resourceAvailabilitySet.Items[randomUnavailableResourceSlot] = false;
                randomUnavailableResourceSlot = FindRandomUnavailableResourceSlot();
                if (randomUnavailableResourceSlot < 0) {
                    StartCoroutine(NotifyAllLogsManufactured());
                }
            }
        }
        
        private IEnumerator NotifyAllLogsManufactured()
        {
            yield return new WaitForSeconds(2f);
            allLogsManufacturedEventChannel.Raise();
        }
        
        public void RemoveLogFromStockpileByIndex(int index)
        {
            if (index < 0 || index >= resourceAvailabilitySet.Items.Count) 
            {
                Debug.LogError("Index out of range");
                return;
            }

            if (!resourceAvailabilitySet.Items[index]) 
            {
                Debug.LogError("No log available at the provided index");
                return;
            }

            resourceAvailabilitySet.Items[index] = false;

            if (resourceAvailabilitySet.Items.All(item => !item))
            {
                StartCoroutine(NotifyAllLogsManufactured());
            }
        }
    

        private int FindRandomAvailableResourceSlot()
        {
            List<int> availableSlots = new List<int>();
            for (int i = 0; i < resourceAvailabilitySet.Items.Count; i++)
            {
                if (!resourceAvailabilitySet.Items[i])
                {
                    availableSlots.Add(i);
                }
            }
            if (availableSlots.Count > 0)
            {
                var index = Random.Range(0, availableSlots.Count);
                return availableSlots[index];
            }
            return -1;
        }
        private int FindRandomUnavailableResourceSlot()
        {
            List<int> availableSlots = new List<int>();
            for (int i = 0; i < resourceAvailabilitySet.Items.Count; i++)
            {
                if (resourceAvailabilitySet.Items[i])
                {
                    availableSlots.Add(i);
                }
            }
            if (availableSlots.Count > 0)
            {
                var index = Random.Range(0, availableSlots.Count);
                return availableSlots[index];
            }
            return -1;
        }

    }
}