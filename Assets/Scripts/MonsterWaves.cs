using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWaves : MonoBehaviour
{

    public GameObject Monster1;

    public float spawnLimitUp = -50;
    public float spawnLimitDown = 50;
    public float spawnLimitLeft = -40;
    public float spawnLimitRight = 40;
    public int spawnNb = 5;
    private int compteur = 0;
    public int maxMonsterPerWave = 10;

    public float timeBetweenWave = 4;
    public float timeBetweenMonsters = 1;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnWaves()
    {
        while (true)
        {
            int spawnNb = Random.Range(1, maxMonsterPerWave + 1);
            for (int i = 0; i < spawnNb; i++)
            {

                Vector3 spawnPosition = new Vector3(Random.Range(spawnLimitLeft, spawnLimitRight), 0, Random.Range(spawnLimitUp, spawnLimitDown));

                // générer un type d'astéroïde aléatoire
                /*int asteroidType = Random.Range(1, 4);

                if (asteroidType == 1)
                    Instantiate(asteroid, spawnPosition, Quaternion.Euler(0, 0, 0));
                else if (asteroidType == 2)
                    Instantiate(asteroid2, spawnPosition, Quaternion.Euler(0, 0, 0));
                else if (asteroidType == 3)
                    Instantiate(asteroid3, spawnPosition, Quaternion.Euler(0, 0, 0));*/
                
                Instantiate(Monster1, spawnPosition, Quaternion.Euler(0, 0, 0));



                compteur++;
                yield return new WaitForSeconds(timeBetweenMonsters);
            }

            yield return new WaitForSeconds(timeBetweenWave);

            
        }


    }
}
