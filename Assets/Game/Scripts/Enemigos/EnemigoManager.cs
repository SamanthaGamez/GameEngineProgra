using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoManager : MonoBehaviour
{
    public static EnemigoManager instance;

    public int enemiesToKill = 10;
    private int activeEnemiesCount = 0;

    public int maxActiveEnemies = 5;
    public Transform[] spawnPoints;
    public float spawnCooldownTime = 2f;
    public GameObject enemyPrefab;
    public LayerMask detectionMask;
    public float spawnCheckAreaRadius = 1f;

    public int poolSize = 8;
    public Queue<GameObject> enemyPool;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        InitializeEnemyPool();
    }

    private void InitializeEnemyPool()
    {
        enemyPool = new Queue<GameObject>(poolSize);

        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemyObject = Instantiate(enemyPrefab);
            enemyObject.SetActive(false);
            enemyPool.Enqueue(enemyObject);
        }
        SpawnInitialEnemies();
    }

    private void SpawnInitialEnemies()
    {
        for (int i = 0; i < maxActiveEnemies; i++)
        {
            TrySpawnEnemy();
        }
    }

    private void KillAllActiveEnemies()
    {
        Enemigo[] aliveEnemies = FindObjectsOfType<Enemigo>();

        foreach (Enemigo enemyScript in aliveEnemies)
        {
            GameObject enemyObject = enemyScript.gameObject;
            enemyObject.SetActive(false);
            enemyPool.Enqueue(enemyObject);
            activeEnemiesCount--;
        }
    }


    private void TrySpawnEnemy()
    {
        if (enemiesToKill <= 0)
        {
            KillAllActiveEnemies();
            return;
        }

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (CanSpawnAtArea(spawnPoints[i]))
            {
                GameObject newEnemy = enemyPool.Dequeue();
                Transform spawnPoint = spawnPoints[i];

                newEnemy.transform.position = spawnPoint.position + Vector3.up * 1.5f;
                newEnemy.SetActive(true);
                activeEnemiesCount++;
                Debug.Log("Enemigo spawneado");
                return;
            }
        }
        Debug.Log("No hay spawns disponibles");
        StartCoroutine(SpawnRetryCooldown());
    }

    private bool CanSpawnAtArea(Transform spawnPosition)
    {
        Collider[] hits = Physics.OverlapSphere(spawnPosition.position, spawnCheckAreaRadius, detectionMask);
        return hits.Length == 0;
    }

    private IEnumerator SpawnRetryCooldown()
    {
        yield return new WaitForSeconds(2);
        if (activeEnemiesCount < maxActiveEnemies)
        {
            TrySpawnEnemy();
            Debug.Log("Reintentando spawn");
        }
    }
    public void EnemyDeath(GameObject enemyObject)
    {
        Enemigo enemyScript = enemyObject.GetComponent<Enemigo>();
        enemyScript.actualHitPoints = enemyScript.maxHitPoints;

        enemyObject.SetActive(false);
        enemyPool.Enqueue(enemyObject);
        activeEnemiesCount--;
        enemiesToKill--;

        UiManager.instance.UpdateEnemiesTxt(enemiesToKill);

        if (activeEnemiesCount < maxActiveEnemies)
        {
            StartCoroutine(DelayAndSpawn());
        }
    }

    private IEnumerator DelayAndSpawn()
    {
        yield return new WaitForSeconds(spawnCooldownTime);
        if (activeEnemiesCount < maxActiveEnemies)
        {
            TrySpawnEnemy();
        }
    }

    public bool hideGizmos;
    private void OnDrawGizmos()
    {
        if (hideGizmos || enemyPrefab == null || spawnPoints == null) return;

        Gizmos.color = Color.red;
        MeshFilter meshFilter = enemyPrefab.GetComponentInChildren<MeshFilter>();
        if (meshFilter != null)
        {
            Mesh mesh = meshFilter.sharedMesh;
            foreach (Transform spawn in spawnPoints)
            {
                Gizmos.DrawMesh(mesh, spawn.position + Vector3.up * 1.5f, Quaternion.Euler(-90f, 0f, 0f));
            }
        }


        Gizmos.color = Color.yellow;

        foreach (Transform spawn in spawnPoints)
        {
            Gizmos.DrawWireSphere(spawn.position, spawnCheckAreaRadius);
        }
    }
}