using CPRE.SOFramework.EventSystem.Channels;
using UnityEditor;
using UnityEngine;

namespace CPRE.SOFramework.EventSystem.Editor {
    [CustomEditor(typeof(Vector3EventChannel))]
    public class Vector3EventChannelEditor : UnityEditor.Editor {
        private Vector3 parameter;
        public override void OnInspectorGUI() {
            DrawDefaultInspector();

            Vector3EventChannel eventChannel = (Vector3EventChannel)target;
            parameter = EditorGUILayout.Vector3Field("Raise Event In Editor Parameter", parameter);
            
            if (GUILayout.Button("Raise Event")) {
                eventChannel.Raise(parameter);
            }
        }
    }
}