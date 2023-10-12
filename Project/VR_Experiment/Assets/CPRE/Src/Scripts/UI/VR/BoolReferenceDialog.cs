using CPRE.SOFramework.DataContainers.Variables;
using TMPro;
using UnityEngine;

namespace CPRE.Scripts.UI {
    public class BoolReferenceDialog : MonoBehaviour{
        
        [SerializeField] private StringReference prefix;
        [SerializeField] private BoolReference boolReference;
        [SerializeField] private StringReference trueString;
        [SerializeField] private StringReference falseString;
        [SerializeField] private StringReference suffix;
        [SerializeField] private TextMeshProUGUI dialogText;
    
    
        public void Update() {
            dialogText.text = prefix.Value + (boolReference.Value ? trueString : falseString) + suffix.Value;
        }
    }
}