using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerMovement : NetworkBehaviour
{
    public float movementSpeed = 5f;

    public Rigidbody2D rb;

    private Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
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
    }

    private void FixedUpdate() 
    {
        rb.MovePosition(rb.position + movement * movementSpeed *  Time.fixedDeltaTime);
    }
}
