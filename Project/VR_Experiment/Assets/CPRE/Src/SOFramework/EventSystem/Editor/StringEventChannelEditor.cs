using CPRE.SOFramework.EventSystem.Channels;
using UnityEditor;
using UnityEngine;

namespace CPRE.SOFramework.EventSystem.Editor {
    [CustomEditor(typeof(StringEventChannel))]
    public class StringEventChannelEditor : UnityEditor.Editor {
        private string parameter;
        public override void OnInspectorGUI() {
            DrawDefaultInspector();

            StringEventChannel eventChannel = (StringEventChannel)target;
            parameter = EditorGUILayout.TextField("Raise Event In Editor Parameter", parameter);
            
            if (GUILayout.Button("Raise Event")) {
                eventChannel.Raise(parameter);
            }
        }
    }
}