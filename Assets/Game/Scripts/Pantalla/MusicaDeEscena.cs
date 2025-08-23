using UnityEngine;

public class MusicaDeEscena : MonoBehaviour
{
    public AudioClip musicaDeEstaEscena;
    
    private static AudioSource musicaDeFondo;

    void Awake()
    {
        // Si la música que va a sonar es la misma que ya está sonando, no hacemos nada.
        if (musicaDeFondo != null && musicaDeFondo.clip == musicaDeEstaEscena)
        {
            return;
        }

        // Si el AudioSource estático no existe, lo creamos por primera vez.
        if (musicaDeFondo == null)
        {
            GameObject musicObject = new GameObject("MusicaDeFondoGlobal");
            musicaDeFondo = musicObject.AddComponent<AudioSource>();
            DontDestroyOnLoad(musicObject);
        }
        
        // Asignamos y reproducimos la nueva canción
        if (musicaDeEstaEscena != null)
        {
            musicaDeFondo.clip = musicaDeEstaEscena;
            musicaDeFondo.loop = true;
            musicaDeFondo.volume = 0.5f; // Puedes cambiar este volumen si quieres
            musicaDeFondo.Play();
        }
        else
        {
            musicaDeFondo.Stop();
        }
    }
}
