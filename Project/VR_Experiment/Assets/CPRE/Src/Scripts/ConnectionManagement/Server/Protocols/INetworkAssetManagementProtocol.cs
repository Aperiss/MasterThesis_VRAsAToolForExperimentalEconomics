namespace CPRE.Scripts.ConnectionManagement.Server.Protocols {
    public interface INetworkAssetManagementProtocol {
        void SpawnNetworkObjects();
        void DestroyNetworkObjects();
    }
}