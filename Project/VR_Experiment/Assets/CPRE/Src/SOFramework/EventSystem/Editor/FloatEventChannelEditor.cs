using CPRE.SOFramework.EventSystem.Channels;
using UnityEditor;
using UnityEngine;

namespace CPRE.SOFramework.EventSystem.Editor {
    [CustomEditor(typeof(FloatEventChannel))]
    public class FloatEventChannelEditor : UnityEditor.Editor {
        private float parameter;
        public override void OnInspectorGUI() {
            DrawDefaultInspector();

            FloatEventChannel eventChannel = (FloatEventChannel)target;
            parameter = EditorGUILayout.FloatField("Raise Event In Editor Parameter", parameter);
            
            if (GUILayout.Button("Raise Event")) {
                eventChannel.Raise(parameter);
            }
        }
    }
}