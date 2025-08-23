using System.Collections;
using UnityEngine;

public class JefeFinal : Enemigo
{
    protected override IEnumerator ProcesarMuerte()
    {
        if (estaMuerto)
        {
            yield break;
        }
        estaMuerto = true;

        StopAllCoroutines();

        if (controladorAnimacion != null) {
            controladorAnimacion.SetTrigger("Die");
        }
        
        if (GetComponent<UnityEngine.AI.NavMeshAgent>() != null)
        {
            GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        }
        if (GetComponent<Collider>() != null)
        {
            GetComponent<Collider>().enabled = false;
        }

        if (GameManager.Instance != null) {
            GameManager.Instance.Victoria();
        }

        yield return null;
    }
}
