using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    protected NavMeshAgent agenteNavegacion;
    protected Transform jugador;
    protected PlayerMove scriptJugador;
    protected Animator controladorAnimacion;

    [Header("Estadisticas")]
    public int danoAtaque = 10;
    public int puntosVida = 20;

    [Header("Sistema de Deteccion")]
    [SerializeField] private float radioDeteccion = 10f;
    [SerializeField] private LayerMask mascaraJugador;

    protected bool jugadorDetectado;
    protected bool enCooldownAtaque = false;
    protected bool estaMuerto = false;

    [Header("Animacion")]
    [SerializeField] private float duracionAnimacionMuerte = 2f;

    protected virtual void Start()
    {
        agenteNavegacion = GetComponent<NavMeshAgent>();
        jugador = GameObject.FindWithTag("Player").transform;
        scriptJugador = jugador.GetComponent<PlayerMove>();
        controladorAnimacion = GetComponent<Animator>();
    }

    private void Update()
    {
        if (estaMuerto) return;

        float velocidadMovimiento = agenteNavegacion.velocity.magnitude;
        controladorAnimacion.SetBool("Moving", velocidadMovimiento > 0.1f);

        BuscarJugador();

        if (jugadorDetectado)
        {
            SeguirJugador();
        }
        EjecutarAtaque();
    }

    private void BuscarJugador()
    {
        jugadorDetectado = Physics.CheckSphere(transform.position, radioDeteccion, mascaraJugador);
    }

    private void SeguirJugador()
    {
        if (!agenteNavegacion.isOnNavMesh) return;

        agenteNavegacion.stoppingDistance = 1;
        agenteNavegacion.SetDestination(jugador.position);
    }

    private void EjecutarAtaque()
    {
        Debug.Log($"Distancia al jugador: {Vector3.Distance(transform.position, jugador.position)}, En Cooldown: {enCooldownAtaque}");
        if (Vector3.Distance(transform.position, jugador.position) < 2f && !enCooldownAtaque)
        {
            controladorAnimacion.SetTrigger("Attack");
            Debug.Log("Golpeo al player");
            DanarJugador();
        }
    }

    public void RecibirDano(int cantidadDano)
    {
        if (estaMuerto) return;

        Debug.Log("Enemigo recibio " + cantidadDano + " de dano.");
        controladorAnimacion.SetTrigger("Damage");
        puntosVida -= cantidadDano;
        StartCoroutine(PausarMovimiento(0.5f));

        if (puntosVida <= 0)
        {
            StartCoroutine(ProcesarMuerte());
        }
    }

    private IEnumerator PausarMovimiento(float duracion)
    {
        agenteNavegacion.isStopped = true;
        agenteNavegacion.SetDestination(transform.position);
        yield return new WaitForSeconds(duracion);
        if (!estaMuerto)
        {
            SeguirJugador();
            agenteNavegacion.isStopped = false;
        }
    }

    protected virtual IEnumerator ProcesarMuerte()
    {
        estaMuerto = true;
        controladorAnimacion.SetTrigger("Die");
        
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(duracionAnimacionMuerte);

        Destroy(gameObject);
    }

    private void DanarJugador()
    {
        if (scriptJugador != null)
        {
            scriptJugador.RecibirDano(danoAtaque);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radioDeteccion);
    }
}