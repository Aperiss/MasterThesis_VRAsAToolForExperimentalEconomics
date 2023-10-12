using CPRE.SOFramework.DataContainers.Variables;
using UnityEditor;
using UnityEngine;

namespace CPRE.SOFramework.DataContainers.Editor {
    
    [CustomPropertyDrawer(typeof(StringReference))]
    public class StringReferencePropertyDrawer : VariableReferencePropertyDrawer {
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            
            EditorGUI.BeginProperty(position, label, property);
            
            FindProperties(property);
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            DrawPopup(position);

            if (referenceOption == VariableReferenceOptions.UseReference) {
                var stringVariableRect = new Rect(position.x, position.y, position.width, position.height);
                reference.objectReferenceValue = EditorGUI.ObjectField(stringVariableRect, reference.objectReferenceValue, typeof(StringVariable), false); 
            }
            else {
                var stringConstRect = new Rect(position.x, position.y, position.width, position.height);
                directValue.stringValue = EditorGUI.TextField(stringConstRect, directValue.stringValue);
            }
            
            EditorGUI.EndProperty();
        }
    }
    
}