using UnityEngine;

public class RecogerArma : MonoBehaviour
{
    public GameObject armaEntregada;

    public void OnTriggerEnter(Collider otroCollider)
    {
        if (otroCollider.CompareTag("Player"))
        {
            InventarioArmas inventarioJugador = FindAnyObjectByType<InventarioArmas>();
            inventarioJugador.RecogerArma(armaEntregada);
            AudioManager.instance.Play("RecogerArma");
            Destroy(gameObject);
        }
    }
}