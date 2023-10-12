using CPRE.SOFramework.DataContainers.Variables;
using TMPro;
using UnityEngine;

namespace CPRE.Scripts.UI {
    public class StringReferenceDialog : MonoBehaviour {
        [SerializeField] private StringReference prefix;
        [SerializeField] private StringReference stringReference;
        [SerializeField] private StringReference suffix;
        [SerializeField] private TextMeshProUGUI dialogText;
    
    
        public void Update() {
            dialogText.text = prefix.Value + stringReference.Value + suffix.Value;
        }
    }
}
