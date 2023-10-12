using CPRE.SOFramework.DataContainers.Variables;
using UnityEditor;
using UnityEngine;

namespace CPRE.SOFramework.DataContainers.Editor {
    
    [CustomPropertyDrawer(typeof(FloatReference))]
    public class FloatReferencePropertyDrawer : VariableReferencePropertyDrawer {
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            
            EditorGUI.BeginProperty(position, label, property);
            
            FindProperties(property);
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            DrawPopup(position);

            if (referenceOption == VariableReferenceOptions.UseReference) {
                var floatVariableRect = new Rect(position.x, position.y, position.width, position.height);
                reference.objectReferenceValue = EditorGUI.ObjectField(floatVariableRect, reference.objectReferenceValue, typeof(FloatVariable), false); 
            }
            else {
                var floatDirectRect = new Rect(position.x, position.y, position.width, position.height);
                directValue.floatValue = EditorGUI.FloatField(floatDirectRect, directValue.floatValue);
            }
            
            EditorGUI.EndProperty();
        }
    }
}