using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class DropItem : NetworkBehaviour
{
    private void Update() {
        transform.position = Vector2.MoveTowards(transform.position, Vector2.left * 100, Time.deltaTime * 5);

        Destroy(gameObject, 5);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // if(IsOwner){
            //     UI_Manager.instance.scoreTextP1.text = UI_Manager.instance.scoreP1.ToString();
            //     UI_Manager.instance.scoreP1 += 10;
            // }
            // else{
            //     UI_Manager.instance.scoreTextP2.text = UI_Manager.instance.scoreP1.ToString();
            //     UI_Manager.instance.scoreP2 += 10;
            // }
            
            UI_Manager.instance.scoreTextP1.text = UI_Manager.instance.scoreP1.ToString();
            UI_Manager.instance.scoreP1 += 10;

            Destroy(gameObject);
        }
    }
}
