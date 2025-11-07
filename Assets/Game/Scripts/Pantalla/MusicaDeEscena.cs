using UnityEngine;

public class MusicaDeEscena : MonoBehaviour
{
    public AudioClip musicaDeEstaEscena;
    
    private static AudioSource musicaDeFondo;

    void Awake()
    {
        
        if (musicaDeFondo != null && musicaDeFondo.clip == musicaDeEstaEscena)
        {
            return;
        }

      
        if (musicaDeFondo == null)
        {
            GameObject musicObject = new GameObject("MusicaDeFondoGlobal");
            musicaDeFondo = musicObject.AddComponent<AudioSource>();
            DontDestroyOnLoad(musicObject);
        }
        
        
        if (musicaDeEstaEscena != null)
        {
            musicaDeFondo.clip = musicaDeEstaEscena;
            musicaDeFondo.loop = true;
            musicaDeFondo.volume = 0.5f; 
            musicaDeFondo.Play();
        }
        else
        {
            musicaDeFondo.Stop();
        }
    }
}
