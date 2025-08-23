using System.Collections;
using UnityEngine;

public class Pistola : Armas
{
    private void Start()
    {
        municionDisponible = municionMaxima;
    }

    public override void Shoot()
    {
        if (recargandoArma || enPeriodoCooldown || municionDisponible <= 0) return;

        GameObject clonBala = Instantiate(prefabBala, puntoDisparo.position, puntoDisparo.rotation);
        clonBala.GetComponent<Rigidbody>().AddForce(puntoDisparo.forward * potenciaDisparo);
        clonBala.GetComponent<Bala>().danoProyectil = danoArma;

        Destroy(clonBala, 5);
        municionDisponible--;
        UiManager.instance.textoBalas.text = municionDisponible + "/" + municionMaxima;
        AudioManager.instance.Play("DisparoPistola");
        StartCoroutine(FireCooldown());
    }

    public override IEnumerator Reload()
    {
        municionDisponible = 0;
        recargandoArma = true;

        if (GameManager.Instance.balasHandGun <= 0)
        {
            UiManager.instance.textoBalas.text = "0";
            recargandoArma = false;
            yield break;
        }

        UiManager.instance.textoBalas.text = "Recargando";
        AudioManager.instance.Play("RecargarPistola");

        yield return new WaitForSeconds(duracionRecarga);

        int cantidadRecargar = GameManager.Instance.balasHandGun >= municionMaxima ? municionMaxima : GameManager.Instance.balasHandGun;
        GameManager.Instance.balasHandGun -= cantidadRecargar;
        municionDisponible = cantidadRecargar;
        recargandoArma = false;
        AudioManager.instance.Stop("RecargarPistola");
        UiManager.instance.textoBalas.text = municionDisponible + "/" + municionMaxima;
    }

    public override IEnumerator FireCooldown()
    {
        enPeriodoCooldown = true;
        yield return new WaitForSeconds(velocidadDisparo);
        enPeriodoCooldown = false;
    }
}