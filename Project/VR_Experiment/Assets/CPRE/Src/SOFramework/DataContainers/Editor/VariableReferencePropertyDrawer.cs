using UnityEditor;
using UnityEngine;

namespace CPRE.SOFramework.DataContainers.Editor {
    
    public class VariableReferencePropertyDrawer : PropertyDrawer {
        protected enum VariableReferenceOptions {
            UseDirect, UseReference
        }
        protected VariableReferenceOptions referenceOption;
        
        private SerializedProperty _useDirect;
        protected SerializedProperty directValue;
        protected SerializedProperty reference;

        private GUIStyle _popupStyle;
        
        protected void FindProperties(SerializedProperty property) {
            _useDirect = property.FindPropertyRelative("_useDirect");
            directValue = property.FindPropertyRelative("_directValue");
            reference = property.FindPropertyRelative("_variableReference");
        }

        protected void DrawPopup(Rect position) {            
            var referenceOptionRect = new Rect(position.x - 20, position.y + 1, 20, position.height);
            
            if (_popupStyle == null) {
                _popupStyle = new GUIStyle(GUI.skin.GetStyle("PaneOptions"));
                _popupStyle.imagePosition = ImagePosition.ImageOnly;
            }
            
            referenceOption = _useDirect.boolValue ? VariableReferenceOptions.UseDirect : VariableReferenceOptions.UseReference;
            referenceOption = (VariableReferenceOptions) EditorGUI.EnumPopup(referenceOptionRect, referenceOption, _popupStyle);
            _useDirect.boolValue = referenceOption == VariableReferenceOptions.UseDirect;
        }
    }
}