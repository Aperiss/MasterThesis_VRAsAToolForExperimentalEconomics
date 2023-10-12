using CPRE.SOFramework.EventSystem.Channels;
using UnityEditor;
using UnityEngine;

namespace CPRE.SOFramework.EventSystem.Editor {
    [CustomEditor(typeof(VoidEventChannel))]
    public class VoidEventChannelEditor : UnityEditor.Editor{
        public override void OnInspectorGUI() {
            DrawDefaultInspector();

            VoidEventChannel eventChannel = (VoidEventChannel)target;
            if (GUILayout.Button("Raise Event")) {
                eventChannel.Raise();
            }
        }
    }
}