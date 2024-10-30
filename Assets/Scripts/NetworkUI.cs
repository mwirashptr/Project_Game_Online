using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using TMPro;

public class NetworkUI : NetworkBehaviour
{
    [SerializeField] private Button hostButton;
    [SerializeField] private Button clientButton;
    [SerializeField] private TextMeshProUGUI playersCount;

    private NetworkVariable<int> playersNum = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone);  //agar semua pemain dapat melihat

    private void Awake() {
        hostButton.onClick.AddListener(()=>
        {
            NetworkManager.Singleton.StartHost();
        });

        clientButton.onClick.AddListener(() => 
        {
            NetworkManager.Singleton.StartClient();
        });
    }

    private void Update() {

        playersCount.text = "Players : " + playersNum.Value.ToString();
        if(!IsServer){
            return;
        }
        playersNum.Value = NetworkManager.Singleton.ConnectedClients.Count;
    }
}
