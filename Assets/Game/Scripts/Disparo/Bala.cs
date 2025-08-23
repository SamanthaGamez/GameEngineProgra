using UnityEngine;

public class Bala : MonoBehaviour
{
    private GameObject objetoColision;
    public int danoProyectil;

    private void OnCollisionEnter(Collision colision)
    {
        if (colision.gameObject.CompareTag("Enemy"))
        {
            colision.gameObject.GetComponent<Enemigo>().RecibirDano(danoProyectil);
            AudioManager.instance.Play("ImpactoDisparoEnemigo");
            Destroy(gameObject);
        }
        else if (colision.gameObject.CompareTag("Ground"))
        {
            AudioManager.instance.Play("ImpactoDisparoTierra");
            Destroy(gameObject, 2);
        }
    }
}