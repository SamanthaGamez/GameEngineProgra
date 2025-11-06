using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistola : MonoBehaviour
{
    public float maxRayDistance;
    public LayerMask enemyMask;
    public float shotCooldownTime;
    private bool isInCooldown;
    public Transform muzzlePoint;

    private void Update()
    {
        if (!isInCooldown && Input.GetMouseButtonDown(0))
        {
            PerformGunShot();
        }
    }

    private void PerformGunShot()
    {
        StartCoroutine(StartShotCooldown());

        Camera mainCamera = Camera.main;
        Ray shootingRay = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hitResult;

        if (Physics.Raycast(shootingRay, out hitResult, maxRayDistance, enemyMask))
        {
            Enemigo enemy = hitResult.collider.GetComponent<Enemigo>();
            if (enemy != null)
            {
                enemy.TakeDamage(1);
            }
        }
    }

    private IEnumerator StartShotCooldown()
    {
        isInCooldown = true;
        yield return new WaitForSeconds(shotCooldownTime);
        isInCooldown = false;
    }

}