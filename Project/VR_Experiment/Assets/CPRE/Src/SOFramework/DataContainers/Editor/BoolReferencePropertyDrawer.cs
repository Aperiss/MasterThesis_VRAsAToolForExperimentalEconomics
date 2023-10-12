using CPRE.SOFramework.DataContainers.Variables;
using UnityEditor;
using UnityEngine;

namespace CPRE.SOFramework.DataContainers.Editor {
    
    [CustomPropertyDrawer(typeof(BoolReference))]
    public class BoolReferencePropertyDrawer : VariableReferencePropertyDrawer {
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            
            EditorGUI.BeginProperty(position, label, property);
            
            FindProperties(property);
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            DrawPopup(position);

            if (referenceOption == VariableReferenceOptions.UseReference) {
                var boolVariableRect = new Rect(position.x, position.y, position.width, position.height);
                reference.objectReferenceValue = EditorGUI.ObjectField(boolVariableRect, reference.objectReferenceValue, typeof(BoolVariable), false); 
            }
            else {
                var boolConstRect = new Rect(position.x, position.y, position.width, position.height);
                directValue.boolValue = EditorGUI.Toggle(boolConstRect, directValue.boolValue);
            }
            EditorGUI.EndProperty();
        }
    }
    
}