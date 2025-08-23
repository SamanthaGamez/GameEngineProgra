using UnityEngine;
using System.Collections;

public class SpawnEnemigos : MonoBehaviour
{
    public GameObject prefabEnemigo;
    public float tiempoEntreSpawn = 5f;

    public bool activo;

    void Start()
    {
        StartCoroutine(spawnearEnemigos());
    }

    private IEnumerator spawnearEnemigos()
    {
        while (activo)
        {
            GameObject enemigo = Instantiate(prefabEnemigo, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(tiempoEntreSpawn);
        }
    }
}
