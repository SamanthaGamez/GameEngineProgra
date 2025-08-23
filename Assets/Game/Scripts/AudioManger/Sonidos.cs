using UnityEngine;

[System.Serializable]
public class Sonidos
{
    public string nombreSonido;
    public AudioClip archivoAudio;

    [Range(0, 1)]
    public float volumenAudio;
    public bool reproducirEnBucle;

    [HideInInspector] public AudioSource fuenteAudio;
}