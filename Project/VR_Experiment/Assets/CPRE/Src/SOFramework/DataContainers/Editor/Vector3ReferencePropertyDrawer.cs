using CPRE.SOFramework.DataContainers.Variables;
using UnityEditor;
using UnityEngine;

namespace CPRE.SOFramework.DataContainers.Editor {
    
    [CustomPropertyDrawer(typeof(Vector3Reference))]
    public class Vector3ReferencePropertyDrawer : VariableReferencePropertyDrawer {
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            
            EditorGUI.BeginProperty(position, label, property);
            
            FindProperties(property);
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            DrawPopup(position);

            if (referenceOption == VariableReferenceOptions.UseReference) {
                var vector3VariableRect = new Rect(position.x, position.y, position.width, position.height);
                reference.objectReferenceValue = EditorGUI.ObjectField(vector3VariableRect, reference.objectReferenceValue, typeof(Vector3Variable), false); 
            }
            else {
                var vector3ConstRect = new Rect(position.x, position.y, position.width, position.height);
                directValue.vector3Value = EditorGUI.Vector3Field(vector3ConstRect, "", directValue.vector3Value);
            }
            
            EditorGUI.EndProperty();
        }
    }
    
}