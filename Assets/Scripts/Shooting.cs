using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Shooting : NetworkBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float damageBullet = 7;

    [SerializeField] private List<GameObject> bullets = new List<GameObject>();

    public float bulletForce = 20f;
    public float fireRate = 0.05f;

    // Start is called before the first frame update
    private void Start()
    {
        transform.Rotate(0f, 0f, -90f);
        if (!IsOwner) return;
        StartCoroutine(AutoShoot());
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private IEnumerator AutoShoot()
    {
        while (true)
        {
            ShootServerRpc();
            yield return new WaitForSeconds(fireRate);
        }
    }

    public void IncreaseBullet()
    {
        damageBullet *= 2;
    }

    [ServerRpc]
    private void ShootServerRpc()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if(bulletScript != null)
        {
            bulletScript.SetDamage(damageBullet);
        }
        bullets.Add(bullet);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        }
        bullet.GetComponent<Bullet>().parent = this;
        bullet.GetComponent<NetworkObject>().Spawn();
    }

    [ServerRpc(RequireOwnership = false)]
    public void DestroyServerRpc()
    {
        GameObject toDestroy = bullets[0];
        toDestroy.GetComponent<NetworkObject>().Despawn();
        bullets.Remove(toDestroy);
        Destroy(toDestroy);
    }
}