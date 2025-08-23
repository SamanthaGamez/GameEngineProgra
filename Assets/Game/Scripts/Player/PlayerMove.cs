using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMove : MonoBehaviour
{
    [Header("Sistema de Movimiento")]
    private CharacterController controladorPersonaje;
    private float inputHorizontal;
    private float inputVertical;

    [SerializeField] private float rapidezMovimiento;

    [Header("Gravedad y Salto")]
    [SerializeField] private float fuerzaGravedad = -9.8f;
    [SerializeField] private Transform verificadorSuelo;
    [SerializeField] private float potenciaSalto;

    private Vector3 velocidadVertical;
    [SerializeField] private bool tocandoSuelo;
    [SerializeField] private float radioDeteccionSuelo;
    [SerializeField] private LayerMask mascaraSuelo;

    public int puntosVida = 100;

    private void Awake()
    {
        controladorPersonaje = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (controladorPersonaje != null && controladorPersonaje.enabled)
        {
            ProcesarMovimiento();
            AplicarGravedad();
        }

        if (Input.GetKeyDown(KeyCode.F9))
        {
            GameManager.Instance.CargarGuardado("Save1");
        }
    }

    private void ProcesarMovimiento()
    {
        inputHorizontal = Input.GetAxis("Horizontal") * rapidezMovimiento * Time.deltaTime;
        inputVertical = Input.GetAxis("Vertical") * rapidezMovimiento * Time.deltaTime;

        Vector3 direccionMovimiento = transform.right * inputHorizontal + transform.forward * inputVertical;
        controladorPersonaje.Move(direccionMovimiento);
    }

    public void RecibirDano(int cantidadDano)
    {
        puntosVida -= cantidadDano;

        if (puntosVida <= 0)
        {
            puntosVida = 0;
            GameManager.Instance.Derrota();
            if(GetComponent<CharacterController>() != null) GetComponent<CharacterController>().enabled = false;
            this.enabled = false; 
        }
    }

    private void AplicarGravedad()
    {
        velocidadVertical.y += fuerzaGravedad * Time.deltaTime;

        if (EstaEnSuelo() && velocidadVertical.y > 0)
        {
            velocidadVertical.y = 0;
        }

        if (Input.GetKey(KeyCode.Space) && EstaEnSuelo())
        {
            AudioManager.instance.Play("Jump");
            velocidadVertical.y = Mathf.Sqrt(potenciaSalto * fuerzaGravedad * -2);
        }

        controladorPersonaje.Move(velocidadVertical * Time.deltaTime);
    }

    private bool EstaEnSuelo()
    {
        return Physics.CheckSphere(verificadorSuelo.position, radioDeteccionSuelo, mascaraSuelo);
    }
}