using UnityEngine;

namespace PubSub
{
    public class Client : MonoBehaviour
    {
        public enum ClientStatus
        {
            Inactive,
            Activating,
            Active,
            Deactivating
        }
    
        [SerializeField] private string host;
        [SerializeField] private string port;
        private Listener _listener;
        private ClientStatus _clientStatus = ClientStatus.Inactive;

        private void Start()
        {
            _listener = new Listener(host, port, HandleMessage);
            EventManager.Instance.onStartClient.AddListener(OnStartClient);
            EventManager.Instance.onClientStarted.AddListener(() => _clientStatus = ClientStatus.Active);
            EventManager.Instance.onStopClient.AddListener(OnStopClient);
            EventManager.Instance.onClientStopped.AddListener(() => _clientStatus = ClientStatus.Deactivating);
        }

        private void Update()
        {
            if (_clientStatus == ClientStatus.Active)
                _listener.DigestMessage();
        }

        private void OnDestroy()
        {
            _listener.Stop();
        }

        private void HandleMessage(string message)
        {
            Debug.Log(message);
        }

        private void OnStartClient()
        {
            Debug.Log("Starting client...");
            _listener.Start();
            _clientStatus = ClientStatus.Activating;
            Debug.Log("Client started!");
        }

        private void OnStopClient()
        {
            Debug.Log("Stopping client...");
            _listener.Stop();
            _clientStatus = ClientStatus.Deactivating;
            Debug.Log("Client stopped!");
        }
    }
}