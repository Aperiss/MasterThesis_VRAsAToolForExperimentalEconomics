using CPRE.SOFramework.DataContainers.Variables;
using TMPro;
using UnityEngine;

namespace CPRE.Scripts.UI {
    public class FloatReferenceDialog : MonoBehaviour {
        [SerializeField] private StringReference prefix;
        [SerializeField] private FloatReference floatReference;
        [SerializeField] private StringReference suffix;
        [SerializeField] private TextMeshProUGUI dialogText;
    
    
        public void Update() {
            dialogText.text = prefix.Value + floatReference.Value + suffix.Value;
        }
    }
}
