using CPRE.SOFramework.EventSystem.Channels;
using UnityEditor;
using UnityEngine;

namespace CPRE.SOFramework.EventSystem.Editor {
    [CustomEditor(typeof(IntEventChannel))]
    public class IntEventChannelEditor : UnityEditor.Editor {
        private int parameter;
        public override void OnInspectorGUI() {
            DrawDefaultInspector();

            IntEventChannel eventChannel = (IntEventChannel)target;
            parameter = EditorGUILayout.IntField("Raise Event In Editor Parameter", parameter);
            
            if (GUILayout.Button("Raise Event")) {
                eventChannel.Raise(parameter);
            }
        }
    }
}