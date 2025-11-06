using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    public int maxHitPoints;
    public int actualHitPoints;

    private NavMeshAgent navAgent;
    private float initialSpeed;
    private float halfSpeed;

    private void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        initialSpeed = navAgent.speed;
        halfSpeed = initialSpeed / 2f;
    }

    public void TakeDamage(int damageAmount)
    {
        actualHitPoints -= damageAmount;

        StartCoroutine(ApplySlowEffect());

        if (actualHitPoints <= 0)
        {
            EnemigoManager.instance.EnemyDeath(gameObject);
        }
    }

    private IEnumerator ApplySlowEffect()
    {
        const float SLOW_DURATION = 2f;
        float currentElapsedTime = 0f;

        navAgent.speed = halfSpeed;

        yield return new WaitForSeconds(0.5f);

        currentElapsedTime = 0f;
        while (currentElapsedTime < SLOW_DURATION)
        {
            navAgent.speed = Mathf.Lerp(halfSpeed, initialSpeed, currentElapsedTime / SLOW_DURATION);
            currentElapsedTime += Time.deltaTime;
            yield return null;
        }
        navAgent.speed = initialSpeed;
    }

    private void Update()
    {
        if (PlayerMove.instance != null)
        {
            navAgent.SetDestination(PlayerMove.instance.transform.position);
        }
    }
}