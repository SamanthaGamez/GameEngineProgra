using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ControladorPantallaFinal : MonoBehaviour
{
    public TextMeshProUGUI mensajeFinalTexto;
    public static string mensajeFinal;

    void Start()
    {
        if (mensajeFinalTexto != null && !string.IsNullOrEmpty(mensajeFinal))
        {
            mensajeFinalTexto.text = mensajeFinal;
        }
        else if (mensajeFinalTexto != null)
        {
            mensajeFinalTexto.text = "Fin del Juego";
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ReiniciarJuego()
    {
        SceneManager.LoadScene("Menu");
    }
}
