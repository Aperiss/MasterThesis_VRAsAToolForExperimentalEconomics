using CPRE.SOFramework.DataContainers.Sets;
using UnityEngine;

namespace CPRE.Scripts.Environment {
    public class CommonPoolInstanceManipulator : MonoBehaviour{
        
        [SerializeField] private BoolRuntimeSet resourceAvailabilitySet;
        
        
        public void ExtractResources(int extractions) {
            
            for (int i = 0; i < extractions; i++) {
                var closestResourceIndex = FindClosestAvailableResource();
                if (closestResourceIndex < 0) {
                    return;
                }

                resourceAvailabilitySet.Items[closestResourceIndex] = false;
            }
        }
        
        public void RegenerateResources(int regenerationAmount) {
			
            for (int i = 0; i < regenerationAmount; i++) {
                var furthestExtractedResource = FindFurthestNonAvailableResource();
                if (furthestExtractedResource < 0) {
                    return;
                }

                resourceAvailabilitySet.Items[furthestExtractedResource] = true;
            }
        }
		
    
        private int FindClosestAvailableResource() {
            
            for (int i = 0; i < resourceAvailabilitySet.Items.Count; i++) {
                if(resourceAvailabilitySet.Items[i])
                    return i;
            }
            return -1;
        }
        
        private int FindFurthestNonAvailableResource() {
			
            for (int i = resourceAvailabilitySet.Items.Count - 1; i >= 0; i--) {
                if (!resourceAvailabilitySet.Items[i]) {
                    return i;
                }
            }
            return -1;
        }
    }
}