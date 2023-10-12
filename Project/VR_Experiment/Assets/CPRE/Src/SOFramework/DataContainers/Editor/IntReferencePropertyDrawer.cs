using CPRE.SOFramework.DataContainers.Variables;
using UnityEditor;
using UnityEngine;

namespace CPRE.SOFramework.DataContainers.Editor {
    
    [CustomPropertyDrawer(typeof(IntReference))]
    public class IntReferencePropertyDrawer : VariableReferencePropertyDrawer {
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            
            EditorGUI.BeginProperty(position, label, property);
            
            FindProperties(property);
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            DrawPopup(position);

            if (referenceOption == VariableReferenceOptions.UseReference) {
                var intVariableRect = new Rect(position.x, position.y, position.width, position.height);
                reference.objectReferenceValue = EditorGUI.ObjectField(intVariableRect, reference.objectReferenceValue, typeof(IntVariable), false); 
            }
            else {
                var intConstRect = new Rect(position.x, position.y, position.width, position.height);
                directValue.intValue = EditorGUI.IntField(intConstRect, directValue.intValue);
            }
            
            EditorGUI.EndProperty();
        }
    }
    
}