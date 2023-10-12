using CPRE.Scripts.Experiment.Events.Extractions;
using UnityEditor;
using UnityEngine;

namespace CPRE.Scripts.Experiment.Events.Editor {
    [CustomEditor(typeof(ExtractionRequestEventChannel))]
    public class ExtractionRequestEventChannelEditor : UnityEditor.Editor {
        private ushort _participantID;
        private int _extractionAmount;

        public override void OnInspectorGUI() {
            DrawDefaultInspector();

            ExtractionRequestEventChannel eventChannel = (ExtractionRequestEventChannel)target;
            _participantID = (ushort)EditorGUILayout.IntField("Participant GUID", _participantID);
            _extractionAmount = EditorGUILayout.IntField("Extraction Amount", _extractionAmount);
            
            if (GUILayout.Button("Raise Event")) {
                var requestData = new ExtractionRequestData {
                    participantId = _participantID,
                    extractionAmount = _extractionAmount
                };
                eventChannel.Raise(requestData);
            }
        }
    }
}