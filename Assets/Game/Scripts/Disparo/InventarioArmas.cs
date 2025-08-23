using UnityEngine;

public class InventarioArmas : MonoBehaviour
{
    public Transform spawnArma;
    public GameObject[] armasObtenidas = new GameObject[3];

    private Armas armaEnMano;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && armasObtenidas[0] != null) CambiarArma(0);
        if (Input.GetKeyDown(KeyCode.Alpha2) && armasObtenidas[1] != null) CambiarArma(1);
        if (Input.GetKeyDown(KeyCode.Alpha3) && armasObtenidas[2] != null) CambiarArma(2);

        if (Input.GetKeyDown(KeyCode.R) && armaEnMano != null)
        {
            StartCoroutine(armaEnMano.Reload());
        }

        if (armaEnMano != null && Input.GetMouseButton(0))
        {
            armaEnMano.Shoot();
        }
    }

    public void RecogerArma(GameObject prefabArma)
    {
        for (int i = 0; i < armasObtenidas.Length; i++)
        {
            if (armasObtenidas[i] == null)
            {
                GameObject nuevaArma = Instantiate(
                    prefabArma,
                    spawnArma.position,
                    spawnArma.rotation,
                    spawnArma
                );
                nuevaArma.SetActive(false);

                armasObtenidas[i] = nuevaArma;
                break;
            }
        }
    }

    private void CambiarArma(int indiceArma)
    {
        if (armaEnMano != null)
        {
            if (armaEnMano.recargandoArma || armaEnMano.enPeriodoCooldown) return;

            armaEnMano.gameObject.SetActive(false);

            if (armaEnMano == armasObtenidas[indiceArma].GetComponent<Armas>())
            {
                UiManager.instance.imagenArma.sprite = null;
                UiManager.instance.textoBalas.text = "";
                armaEnMano = null;
                Debug.Log("Desesquipar");
                return;
            }
        }

        if (armasObtenidas[indiceArma] != null)
        {
            armasObtenidas[indiceArma].SetActive(true);
            armaEnMano = armasObtenidas[indiceArma].GetComponent<Armas>();

            UiManager.instance.imagenArma.sprite = armaEnMano.spriteUI;
            UiManager.instance.textoBalas.text = armaEnMano.municionDisponible + "/" + armaEnMano.municionMaxima;

            AudioManager.instance.Play("RecogerArma");
        }
    }
}