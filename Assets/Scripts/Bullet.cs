using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // private void OnCollisionEnter2D (Collider2D other) 
    // {
    //     Debug.Log(other.name);
    //     Destroy(gameObject);
    // }
    
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log(other.name);
        Destroy(gameObject);
        
    }
}
