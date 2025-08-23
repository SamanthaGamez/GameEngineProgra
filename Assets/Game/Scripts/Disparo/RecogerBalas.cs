using UnityEngine;

public class RecogerBalas : MonoBehaviour
{
    public TipoMunicion tipoDeMunicion;
    public int cantidadBalas;

    public enum TipoMunicion
    {
        handGun,
        rifle,
        shotGun
    }

    private void EntregarBalas()
    {
        switch (tipoDeMunicion)
        {
            case TipoMunicion.handGun:
                GameManager.Instance.balasHandGun += cantidadBalas;
                break;

            case TipoMunicion.rifle:
                GameManager.Instance.balasRifle += cantidadBalas;
                break;

            case TipoMunicion.shotGun:
                GameManager.Instance.balasEscopeta += cantidadBalas;
                break;
        }
    }

    private void OnTriggerEnter(Collider otroCollider)
    {
        if (otroCollider.CompareTag("Player"))
        {
            EntregarBalas();
            AudioManager.instance.Play("DarBalas");
            Destroy(gameObject);
        }
    }
}