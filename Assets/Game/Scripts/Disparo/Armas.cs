using UnityEngine;
using System.Collections;

public abstract class Armas : MonoBehaviour
{
    [Header("Configuracion Basica")]
    public string id;

    [Header("Estadisticas de Combate")]
    public int municionMaxima;
    public int municionDisponible;
    public float duracionRecarga;
    public float velocidadDisparo;
    public int danoArma;
    public float potenciaDisparo;

    [Header("Referencias del Sistema")]
    public GameObject prefabBala;
    public Transform puntoDisparo;
    public Sprite spriteUI;

    public bool recargandoArma = false;
    public bool enPeriodoCooldown = false;

    public abstract void Shoot();
    public abstract IEnumerator Reload();
    public abstract IEnumerator FireCooldown();
}