using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int balasHandGun, balasEscopeta, balasRifle;
    public string archivoGuardado;
    public GameObject[] inventarioArmas = new GameObject[3];
    public string[] identificadoresArmas = new string[3];

    public ArmasPrefab[] catalogoArmas = new ArmasPrefab[3];

    public float coordenadaX, coordenadaY, coordenadaZ;
    private JugadorPerfil datosJugador;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void GuardarPartida()
    {
        inventarioArmas = FindAnyObjectByType<InventarioArmas>().armasObtenidas;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        coordenadaX = player.transform.position.x;
        coordenadaY = player.transform.position.y;
        coordenadaZ = player.transform.position.z;
        ConvertirArmasAIDs();

        SistemaGuardado.GuardarPartida();
    }

    public void CargarGuardado(string nombreArchivo)
    {
        archivoGuardado = nombreArchivo;
        datosJugador = SistemaGuardado.CargarPartida();

        if (datosJugador != null)
        {
            balasHandGun = datosJugador.balasHandGun;
            balasEscopeta = datosJugador.balasEscopeta;
            balasRifle = datosJugador.balasRifle;

            identificadoresArmas = datosJugador.armasObtenidas;

            coordenadaX = datosJugador.posX;
            coordenadaY = datosJugador.posY;
            coordenadaZ = datosJugador.posZ;

            RestaurarArmas();

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<CharacterController>().enabled = false;
            player.transform.position = new Vector3(coordenadaX, coordenadaY, coordenadaZ);
            player.GetComponent<CharacterController>().enabled = true;
        }
    }

    private void RestaurarArmas()
    {
        InventarioArmas sistemaInventario = FindAnyObjectByType<InventarioArmas>();

        for (int i = 0; i < identificadoresArmas.Length; i++)
        {
            if (!string.IsNullOrEmpty(identificadoresArmas[i]))
            {
                GameObject prefabArma = catalogoArmas[EncontrarIndicePorID(identificadoresArmas[i])].prefabArma;
                GameObject armaInstanciada = Instantiate(prefabArma, sistemaInventario.spawnArma.position, sistemaInventario.spawnArma.rotation, sistemaInventario.spawnArma);
                armaInstanciada.SetActive(false);
                sistemaInventario.armasObtenidas[i] = armaInstanciada;
            }
        }
    }

    private void CargarInventarioArmas()
    {
        InventarioArmas sistemaInventario = FindAnyObjectByType<InventarioArmas>();
        for (int i = 0; i < identificadoresArmas.Length; i++)
        {
            sistemaInventario.armasObtenidas[i] = catalogoArmas[EncontrarIndicePorID(identificadoresArmas[i])].prefabArma;
        }
    }

    private int EncontrarIndicePorID(string identificador)
    {
        for (int i = 0; i < catalogoArmas.Length; i++)
        {
            if (catalogoArmas[i].identificador == identificador)
                return i;
        }
        return 0;
    }

    private void ConvertirArmasAIDs()
    {
        for (int i = 0; i < inventarioArmas.Length; i++)
        {
            if (inventarioArmas[i] != null && inventarioArmas[i].GetComponent<Armas>() != null)
            {
                string idArma = inventarioArmas[i].GetComponent<Armas>().id;
                identificadoresArmas[i] = idArma;
                Debug.Log("Arma " + idArma + " salvada en el espacio " + i);
            }
        }
    }

    public void Victoria()
    {
        ControladorPantallaFinal.mensajeFinal = "¡VICTORIA!";
        SceneManager.LoadScene("EscenaFinal");
    }

    public void Derrota()
    {
        ControladorPantallaFinal.mensajeFinal = "HAS SIDO DERROTADO";
        SceneManager.LoadScene("EscenaFinal");
    }
}