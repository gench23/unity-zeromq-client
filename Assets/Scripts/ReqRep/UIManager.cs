using UnityEngine;
using UnityEngine.UI;

namespace ReqRep
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Button sendRequestButton;

        private void Start()
        {
            sendRequestButton.onClick.AddListener(() => EventManager.Instance.onSendRequest.Invoke());
        }
    }
}
