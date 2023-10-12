using System.Collections.Generic;
using CPRE.SOFramework.DataContainers.Sets;
using UnityEngine;

namespace CPRE.Scripts.UI {
    public class ResourceVisualizer : MonoBehaviour {
    
        [SerializeField] Vector3RuntimeSet resourcePositions;
        [SerializeField] GameObject resourceIndicatorPrefab;
    
        [SerializeField] float scale;

        private List<GameObject> _resourceIndicators;

        private void Update() {
            if (_resourceIndicators == null || _resourceIndicators.Count != resourcePositions.Items.Count) {
                ClearResourceIndicators();
                InstantiateResourceIndicators();
            }
        }

        private void InstantiateResourceIndicators() {
            _resourceIndicators = new List<GameObject>();
            foreach (Vector3 resourcePosition in resourcePositions.Items) {
                Vector3 scaledPosition = resourcePosition * scale;
                scaledPosition.y = 0;
                Vector3 offsetPosition = this.transform.position + scaledPosition;
                GameObject resourceIndicator = Instantiate(resourceIndicatorPrefab, offsetPosition, Quaternion.identity);
                resourceIndicator.transform.localScale = Vector3.one * scale;
                resourceIndicator.transform.SetParent(this.transform);
                _resourceIndicators.Add(resourceIndicator);
            }
        }

        private void ClearResourceIndicators() {
            if (_resourceIndicators != null) {
                foreach (GameObject resourceIndicator in _resourceIndicators) {
                    Destroy(resourceIndicator);
                }
                _resourceIndicators.Clear();
                _resourceIndicators = null;
            }
        }
    }
}
