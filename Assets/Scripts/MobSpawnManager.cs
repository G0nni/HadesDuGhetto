using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobSpawnManager : MonoBehaviour
{
    public GameObject[] mobPrefabs; // Liste des prefabs de mobs à spawner
    public Transform[] spawnPoints; // Les points de spawn des mobs
    public float spawnInterval = 3f; // Intervalle entre chaque spawn
    public int maxMobs = 10; // Nombre maximal de mobs à la fois

    private int currentMobs = 0; // Nombre actuel de mobs

    void Start()
    {
        // Lancer la routine de spawn
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            // Vérifier si le nombre maximal de mobs n'a pas été atteint
            if (currentMobs < maxMobs)
            {
                // Choisir un point de spawn aléatoire
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

                // Choisir un prefab de mob aléatoire
                GameObject mobPrefab = mobPrefabs[Random.Range(0, mobPrefabs.Length)];

                // Vérifier si le point de spawn est sur la surface de la nav mesh
                NavMeshHit hit;
                if (NavMesh.SamplePosition(spawnPoint.position, out hit, 1.0f, NavMesh.AllAreas))
                {
                    // Instancier le mob au point de spawn
                    Instantiate(mobPrefab, hit.position, Quaternion.identity);
                    currentMobs++;
                }
            }

            // Attendre l'intervalle de spawn
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // Méthode appelée lorsque le mob meurt pour réduire le nombre de mobs actifs
    public void MobDied()
    {
        currentMobs--;
    }
}
