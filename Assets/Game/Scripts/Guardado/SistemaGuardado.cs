using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SistemaGuardado
{
    public static void GuardarPartida()
    {
        string rutaArchivo = Application.dataPath + GameManager.Instance.archivoGuardado;

        FileStream flujoArchivo = new FileStream(rutaArchivo, FileMode.Create);

        JugadorPerfil datosPartida = new JugadorPerfil();

        BinaryFormatter formateador = new BinaryFormatter();

        formateador.Serialize(flujoArchivo, datosPartida);

        flujoArchivo.Close();

        Debug.Log("Partida guardada en " + rutaArchivo);
    }

    public static JugadorPerfil CargarPartida()
    {
        string rutaArchivo = Application.dataPath + GameManager.Instance.archivoGuardado;

        if (File.Exists(rutaArchivo))
        {
            FileStream flujoArchivo = new FileStream(rutaArchivo, FileMode.Open);

            BinaryFormatter formateador = new BinaryFormatter();

            JugadorPerfil datosPartida = formateador.Deserialize(flujoArchivo) as JugadorPerfil;

            flujoArchivo.Close();

            Debug.Log("Partida cargada " + rutaArchivo);

            return datosPartida;
        }
        else
        {
            Debug.Log("No se encontro archivo");

            return null;
        }
    }
}