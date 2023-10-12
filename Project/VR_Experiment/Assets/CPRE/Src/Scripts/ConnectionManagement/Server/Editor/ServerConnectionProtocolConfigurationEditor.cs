using CPRE.Scripts.ConnectionManagement.Server.Protocols;
using UnityEditor;
using UnityEngine;

namespace CPRE.Scripts.ConnectionManagement.Server.Editor {
    [CustomEditor(typeof(ServerConnectionProtocolConfiguration))]
    public class ServerConnectionProtocolConfigurationEditor : UnityEditor.Editor{
        public override void OnInspectorGUI()
        {
            ServerConnectionProtocolConfiguration config = (ServerConnectionProtocolConfiguration)target;

            // Draw the default inspector
            DrawDefaultInspector();

            // Validate properties and update the IsValid flag
            config.Validate();
            
            // Check if the assigned ScriptableObjects are of correct types for the editor message
            CheckPropertyType(config.ConnectionApprovalManagerSO, typeof(IAccessVerificationProtocol));
            CheckPropertyType(config.ClientConnectionsHandlerSO, typeof(IClientSessionManagementProtocol));
            CheckPropertyType(config.NetworkObjectManagerSO, typeof(INetworkAssetManagementProtocol));
        }

        private void CheckPropertyType(ScriptableObject property, System.Type expectedType)
        {
            if (property != null && !expectedType.IsAssignableFrom(property.GetType()))
            {
                EditorGUILayout.HelpBox($"Assigned ScriptableObject is not of type {expectedType.Name}", MessageType.Error);
            }
        }
    }
}