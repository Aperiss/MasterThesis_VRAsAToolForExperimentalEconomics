using CPRE.SOFramework.DataContainers.Variables;
using UnityEditor;
using UnityEngine;

namespace CPRE.SOFramework.DataContainers.Editor {
    
    [CustomPropertyDrawer(typeof(Vector2Reference))]
    public class Vector2ReferencePropertyDrawer : VariableReferencePropertyDrawer {
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            
            EditorGUI.BeginProperty(position, label, property);
            
            FindProperties(property);
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            DrawPopup(position);

            if (referenceOption == VariableReferenceOptions.UseReference) {
                var vector2VariableRect = new Rect(position.x, position.y, position.width, position.height);
                reference.objectReferenceValue = EditorGUI.ObjectField(vector2VariableRect, reference.objectReferenceValue, typeof(Vector2Variable), false); 
            }
            else {
                var vector2VariableRect = new Rect(position.x, position.y, position.width, position.height);
                directValue.vector2Value = EditorGUI.Vector2Field(vector2VariableRect, "", directValue.vector2Value);
            }
            
            EditorGUI.EndProperty();
        }
    }
    
}