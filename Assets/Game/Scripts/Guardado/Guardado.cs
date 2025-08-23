using System.Collections;
using UnityEngine;

public class Guardado : MonoBehaviour
{
    public float radioDeteccion;
    public LayerMask capaJugador;
    private bool bloqueadoPorCooldown;

    private void Update()
    {
        if (JugadorEnRango() && Input.GetKeyDown(KeyCode.E) && !bloqueadoPorCooldown)
        {
            EjecutarGuardado();
        }
    }

    private void EjecutarGuardado()
    {
        GameManager.Instance.GuardarPartida();
        StartCoroutine(MostrarEfectoGuardado());
    }

    private bool JugadorEnRango()
    {
        Collider[] objetosDetectados = Physics.OverlapBox(gameObject.transform.position, new Vector3(radioDeteccion, radioDeteccion, radioDeteccion), Quaternion.identity, capaJugador);

        foreach (Collider col in objetosDetectados)
        {
            if (col.CompareTag("Player"))
                return true;
        }

        return false;
    }

    private IEnumerator MostrarEfectoGuardado()
    {
        bloqueadoPorCooldown = true;
        yield return new WaitForSeconds(2);
        bloqueadoPorCooldown = false;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, new Vector3(radioDeteccion, radioDeteccion, radioDeteccion));
    }
#endif
}