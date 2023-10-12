using CPRE.SOFramework.EventSystem.Channels;
using UnityEditor;
using UnityEngine;

namespace CPRE.SOFramework.EventSystem.Editor {
    [CustomEditor(typeof(Vector4EventChannel))]
    public class Vector4EventChannelEditor : UnityEditor.Editor {
        private Vector4 parameter;
        public override void OnInspectorGUI() {
            DrawDefaultInspector();

            Vector4EventChannel eventChannel = (Vector4EventChannel)target;
            parameter = EditorGUILayout.Vector4Field("Raise Event In Editor Parameter", parameter);
            
            if (GUILayout.Button("Raise Event")) {
                eventChannel.Raise(parameter);
            }
        }
    }
}