using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Shooting : NetworkBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    public float bulletForce = 20f;
    public float fireRate = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(0f, 0f, -90f);
        if(!IsOwner) return;
        StartCoroutine(AutoShoot());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator AutoShoot(){
        while(true){
            ShootServerRpc();
            yield return new WaitForSeconds(fireRate);
        }
    }

    [ServerRpc]
    private void ShootServerRpc(){
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if(rb != null){
            rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        }

        bullet.GetComponent<NetworkObject>().Spawn();

    }

}
