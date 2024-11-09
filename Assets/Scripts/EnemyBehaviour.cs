using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float randomValue;
    public float currentHealth;
    public float maxHealth;
    public GameObject[] itemsDropArray;

    private void Start() {
        currentHealth = maxHealth;

        randomValue = Random.Range(-3, 3);
        transform.position = new Vector2(transform.position.x, randomValue);
    }

    private void Update() {
        transform.position = Vector2.MoveTowards(transform.position, Vector2.left * 100, Time.deltaTime * 5);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;   //mengurangi currentHealth dengan damage
        
        //apabila currentHealth kurang dari 0 maka akan di destroy
        if(currentHealth <= 0){
            for(int i = 0; i < itemsDropArray.Length; i++){
                // Instantiate(itemsDropArray[Random.Range(0, itemsDropArray.Length)], transform.position, Quaternion.identity);   //menampilkan item drop
                Instantiate(itemsDropArray[i], transform.position, Quaternion.identity);   //menampilkan item drop
            }
            Destroy(gameObject);
        }
    }
}
