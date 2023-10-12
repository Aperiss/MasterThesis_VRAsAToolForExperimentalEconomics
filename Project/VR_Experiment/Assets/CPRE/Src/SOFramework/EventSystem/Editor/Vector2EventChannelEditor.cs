using CPRE.SOFramework.EventSystem.Channels;
using UnityEditor;
using UnityEngine;

namespace CPRE.SOFramework.EventSystem.Editor {
    [CustomEditor(typeof(Vector2EventChannel))]
    public class Vector2EventChannelEditor : UnityEditor.Editor {
        private Vector2 parameter;
        public override void OnInspectorGUI() {
            DrawDefaultInspector();

            Vector2EventChannel eventChannel = (Vector2EventChannel)target;
            parameter = EditorGUILayout.Vector2Field("Raise Event In Editor Parameter", parameter);
            
            if (GUILayout.Button("Raise Event")) {
                eventChannel.Raise(parameter);
            }
        }
    }
}