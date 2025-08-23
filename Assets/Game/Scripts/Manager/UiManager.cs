using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    public TextMeshProUGUI textoBalas;
    public Image imagenArma;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "EscenaFinal")
        {
            GameObject textoBalasObj = GameObject.FindGameObjectWithTag("TextoBalasUI");
            if (textoBalasObj != null)
            {
                textoBalas = textoBalasObj.GetComponent<TextMeshProUGUI>();
            }
            else
            {
                Debug.LogWarning("UiManager: No se encontró ningún objeto con el tag 'TextoBalasUI' en la escena.");
            }

            GameObject imagenArmaObj = GameObject.FindGameObjectWithTag("ImagenArmaUI");
            if (imagenArmaObj != null)
            {
                imagenArma = imagenArmaObj.GetComponent<Image>();
            }
            else
            {
                Debug.LogWarning("UiManager: No se encontró ningún objeto con el tag 'ImagenArmaUI' en la escena.");
            }
            
            if (imagenArma != null) imagenArma.sprite = null;
            if (textoBalas != null) textoBalas.text = "";
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
