using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Bullet : NetworkBehaviour
{
    public Shooting parent;

    //[SerializeField] private GameObject hitParticle;
    [SerializeField] private float shootForce;

    private float damage;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //GameObject hitImpact = (Instantiate(hitParticle, transform.position, Quaternion.identity));
        //hitImpact.transform.localEulerAngles = new Vector3(0f, 0f, -90f);
        if (!IsOwner) return;
        //Destroy(gameObject);

        if(other.gameObject.CompareTag("Enemy"))
        {
            EnemyBehaviour enemy = other.GetComponent<EnemyBehaviour>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
        else{
            Destroy(gameObject, 1f);
        }

        parent.DestroyServerRpc();
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }
}