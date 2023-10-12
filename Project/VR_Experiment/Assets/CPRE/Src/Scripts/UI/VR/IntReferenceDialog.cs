using CPRE.SOFramework.DataContainers.Variables;
using TMPro;
using UnityEngine;

namespace CPRE.Scripts.UI {
    public class IntReferenceDialog : MonoBehaviour {
        [SerializeField] private StringReference prefix;
        [SerializeField] private IntReference intReference;
        [SerializeField] private StringReference suffix;
        [SerializeField] private TextMeshProUGUI dialogText;
    
    
        public void Update() {
            dialogText.text = prefix.Value + intReference.Value + suffix.Value;
        }
    }
}
