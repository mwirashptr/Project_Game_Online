using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private float time = 0;
    // private float spawnTime = 7;
    public float countEnemy;
    public GameObject enemyPhoenix;
    public GameObject enemyTorik;
    public GameObject enemyBossIpan;
    public GameObject powerUp_Incrase;

    // Update is called once per frame
    void Update()
    {
        if(time <= 0){
            if(countEnemy < 5)
            {
                SpawnPhoenix();
                time = 7;
                countEnemy++;
            }
            else if(countEnemy == 5)
            {
                SpawnPowerUpIncrase();
                time = 7;
                countEnemy++;
            }
            else if(countEnemy < 8 && countEnemy >= 5)
            {
                SpawnTorik();
                time = 7;
                // time = 15;
                countEnemy++;
            }
            else if(countEnemy < 10 && countEnemy >= 8)
            {
                SpawnBossIpan();
                // time = 30;
                time = 7;
                countEnemy++;
            }
        }
        else{
            time -= Time.deltaTime;
        }
    }

    private void SpawnPhoenix()
    {
        Instantiate(enemyPhoenix, transform.position, Quaternion.identity);
    }

    private void SpawnTorik()
    {
        Instantiate(enemyTorik, transform.position, Quaternion.identity);
    }

    private void SpawnBossIpan()
    {
        Instantiate(enemyBossIpan, transform.position, Quaternion.identity);
    }

    private void SpawnPowerUpIncrase()
    {
        Instantiate(powerUp_Incrase, transform.position, Quaternion.identity);
    }
}
