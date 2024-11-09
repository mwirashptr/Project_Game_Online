using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_Magnet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player"))
        {
            
        }
    }
}
