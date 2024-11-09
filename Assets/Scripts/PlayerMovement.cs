using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerMovement : NetworkBehaviour
{
    public float movementSpeed = 5f;
    public Rigidbody2D rb;
    private Vector2 movement;
    public bool isReady = false;
    public bool isCollectMagnet = false;

    private NetworkUI networkUI;

    // Start is called before the first frame update
    void Start()
    {
        networkUI = FindObjectOfType<NetworkUI>();
        // transform.Rotate(0f, 0f, -90f);
    }

    public override void OnNetworkSpawn()
    {
        if (IsOwner)  // Pastikan hanya pemain ini yang mengatur posisinya sendiri
        {
            Vector3 spawnPosition = GetSpawnPosition();
            transform.position = spawnPosition;
        }
    }

    private Vector3 GetSpawnPosition()
    {
        if (OwnerClientId == 0)  // Pemain pertama
        {
            return new Vector3(-8f, 1.5f, 0f);
        }
        else  // Pemain kedua atau lainnya
        {
            return new Vector3(-8f, -1.5f, 0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsOwner){
            return;
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(KeyCode.Space))
        {
            CheckDamage();
        }

        //menyimpan posisi pemain
        if(networkUI.GetTransitionStatus())
        {
            GameManager.instance.playerPosition = transform.position;
            LoadNextScene();
        }

    }

    private void FixedUpdate() 
    {
        rb.MovePosition(rb.position + movement * movementSpeed *  Time.fixedDeltaTime);
    }

    private void LoadNextScene()
    {
        if(IsServer)
        {
            NetworkManager.SceneManager.LoadScene("In-Game", UnityEngine.SceneManagement.LoadSceneMode.Single);
        }
    }

    public void CheckDamage()
    {
        Shooting playerDamage = GetComponentInChildren<Shooting>();

        Debug.Log("Damage: " + playerDamage.damageBullet);
    }

    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("Enemy"))
        {
            this.gameObject.SetActive(false);
        }
    }
}
