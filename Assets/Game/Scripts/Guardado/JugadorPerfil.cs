using UnityEngine;

[System.Serializable]
public class JugadorPerfil
{
    public int balasHandGun, balasEscopeta, balasRifle;
    public float posX, posY, posZ;
    public string nivelActual;
    public string[] armasObtenidas = new string[3];

    public JugadorPerfil()
    {
        balasHandGun = GameManager.Instance.balasHandGun;
        balasEscopeta = GameManager.Instance.balasEscopeta;
        balasRifle = GameManager.Instance.balasRifle;

        armasObtenidas = GameManager.Instance.identificadoresArmas;

        posX = GameManager.Instance.coordenadaX;
        posY = GameManager.Instance.coordenadaY;
        posZ = GameManager.Instance.coordenadaZ;
    }
}