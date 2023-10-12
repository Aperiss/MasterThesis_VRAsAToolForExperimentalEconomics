using CPRE.SOFramework.EventSystem.Channels;
using UnityEditor;
using UnityEngine;

namespace CPRE.SOFramework.EventSystem.Editor {
    [CustomEditor(typeof(BoolEventChannel))]
    public class BoolEventChannelEditor : UnityEditor.Editor {
        private bool parameter;
        public override void OnInspectorGUI() {
            DrawDefaultInspector();

            BoolEventChannel eventChannel = (BoolEventChannel)target;
            parameter = EditorGUILayout.Toggle("Raise Event In Editor Parameter", parameter);
            
            if (GUILayout.Button("Raise Event")) {
                eventChannel.Raise(parameter);
            }
        }
    }
}