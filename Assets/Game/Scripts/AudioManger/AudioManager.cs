using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sonidos[] listaSonidos;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        foreach (Sonidos sonido in listaSonidos)
        {
            sonido.fuenteAudio = gameObject.AddComponent<AudioSource>();
            sonido.fuenteAudio.clip = sonido.archivoAudio;
            sonido.fuenteAudio.volume = sonido.volumenAudio;
            sonido.fuenteAudio.loop = sonido.reproducirEnBucle;
        }
    }

    public void Play(string nombreSonido)
    {
        Debug.Log("AudioManager: Intentando reproducir el sonido: " + nombreSonido);
        foreach (Sonidos sonido in listaSonidos)
        {
            if (sonido.nombreSonido == nombreSonido)
            {
                Debug.Log("AudioManager: Sonido encontrado! Reproduciendo: " + nombreSonido);
                sonido.fuenteAudio.Play();
                return;
            }
        }
        Debug.LogWarning("AudioManager: No se encontró un sonido con el nombre: " + nombreSonido);
    }

    public void Stop(string nombreSonido)
    {
        foreach (Sonidos sonido in listaSonidos)
        {
            if (sonido.nombreSonido == nombreSonido)
            {
                sonido.fuenteAudio.Stop();
                return;
            }
        }
    }
}
