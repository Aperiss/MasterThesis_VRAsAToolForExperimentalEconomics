using CPRE.SOFramework.DataContainers.Variables;
using UnityEditor;
using UnityEngine;

namespace CPRE.SOFramework.DataContainers.Editor {
    
    [CustomPropertyDrawer(typeof(Vector4Reference))]
    public class Vector4ReferencePropertyDrawer : VariableReferencePropertyDrawer {
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            
            EditorGUI.BeginProperty(position, label, property);
            
            FindProperties(property);
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            DrawPopup(position);

            if (referenceOption == VariableReferenceOptions.UseReference) {
                var vector4VariableRect = new Rect(position.x, position.y, position.width, position.height);
                reference.objectReferenceValue = EditorGUI.ObjectField(vector4VariableRect, reference.objectReferenceValue, typeof(Vector4Variable), false); 
            }
            else {
                var vector4ConstRect = new Rect(position.x, position.y, position.width, position.height);
                directValue.vector4Value = EditorGUI.Vector4Field(vector4ConstRect, "", directValue.vector4Value);
            }
            
            EditorGUI.EndProperty();
        }
    }
    
}