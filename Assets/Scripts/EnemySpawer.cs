using UnityEngine;
using System.Collections.Generic;

public class EnemySpawer : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject player;
    [SerializeField] List<Transform> spawnPoints;
    [SerializeField] float spawnDelay = 5f;

    void Awake()
    {
        
    }

    void Start()
    {
        InvokeRepeating("Spawn", spawnDelay, spawnDelay);           
    }

    void Update()
    {
        
    }

    void Spawn(){
        foreach (Transform sp in spawnPoints){
            GameObject enemy = Instantiate(enemyPrefab, sp);
            var ef = enemy.GetComponent<EnemyFollow>();
            ef.player = player;
            ef.enabled = true;
        }
    }
}
