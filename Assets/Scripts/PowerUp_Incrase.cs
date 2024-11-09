using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_Incrase : MonoBehaviour
{
    private void Update() {
        transform.position = Vector2.MoveTowards(transform.position, Vector2.left * 100, Time.deltaTime * 5);

        Destroy(gameObject, 5);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player"))
        {
            Shooting shooting = other.GetComponentInChildren<Shooting>();
            if (shooting != null)
            {
                shooting.IncreaseBullet();
                Destroy(gameObject);
            }
        }
    }

}
