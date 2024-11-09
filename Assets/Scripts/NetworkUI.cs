using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class NetworkUI : NetworkBehaviour
{
    [SerializeField] private Button hostButton;
    [SerializeField] private Button clientButton;
    [SerializeField] private Button readyButton;
    [SerializeField] private TextMeshProUGUI playersCount;

    private NetworkVariable<int> playersNum = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone);  //agar semua pemain dapat melihat

    public bool isTranstioning = false;
    private bool isAllReady = false;

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

    private void Start() {
        foreach (var client in NetworkManager.Singleton.ConnectedClients)
        {
            client.Value.PlayerObject.GetComponent<PlayerMovement>().enabled = false;
            client.Value.PlayerObject.GetComponentInChildren<Shooting>().enabled = false;
        }
    }

    private void Update() {

        if(playersNum.Value != 2){
            readyButton.interactable = false;
        }
        else{
            readyButton.interactable = true;
        }

        playersCount.text = "Players : " + playersNum.Value.ToString();
        if(!IsServer){
            return;
        }

        ReadyPlayer();

        //apabila pemain berjumlah 2 maka akan pindah scene selanjutnya
        if(isAllReady)
        {
            foreach (var client in NetworkManager.Singleton.ConnectedClients)
            {
                client.Value.PlayerObject.GetComponent<PlayerMovement>().enabled = true;
                client.Value.PlayerObject.GetComponentInChildren<Shooting>().enabled = true;
            }
            // SceneTransition();
            isTranstioning = true;
        }
        else{
            foreach (var client in NetworkManager.Singleton.ConnectedClients)
            {
                client.Value.PlayerObject.GetComponent<PlayerMovement>().enabled = false;
                client.Value.PlayerObject.GetComponentInChildren<Shooting>().enabled = false;
            }
            isTranstioning = false;
        }
        
        playersNum.Value = NetworkManager.Singleton.ConnectedClients.Count;
    }

    private void ReadyPlayer()
    {
        //make button ready to be clicked in host and client, check if player is ready
        if(NetworkManager.Singleton.ConnectedClients.Count == 2)
        {
            readyButton.onClick.AddListener(() =>
            {
                if(NetworkManager.Singleton.IsHost){
                    NetworkManager.Singleton.ConnectedClients[0].PlayerObject.GetComponent<PlayerMovement>().isReady = true;
                }
                else if(NetworkManager.Singleton.IsClient){
                    NetworkManager.Singleton.ConnectedClients[1].PlayerObject.GetComponent<PlayerMovement>().isReady = true;
                }
                // NetworkManager.Singleton.ConnectedClients[1].PlayerObject.GetComponent<PlayerMovement>().isReady = true;
                isAllReady = true;
            });
            
        }
    }

    public void SceneTransition()
    {
        NetworkManager.SceneManager.LoadScene("In-Game", LoadSceneMode.Single);
    }

    public bool GetTransitionStatus()
    {
        return isTranstioning;
    }
}
