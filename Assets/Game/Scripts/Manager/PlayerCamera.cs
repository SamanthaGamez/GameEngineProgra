using UnityEngine;
using UnityEngine.InputSystem.XInput;

public class PlayerCamera : MonoBehaviour
{
    private Transform transformPadre;

    private float entradaX;
    private float entradaY;

    [SerializeField] private float sensibilidadX = 200f;
    [SerializeField] private float sensibilidadY = 200f;
    [SerializeField] private float tiempoSuavizado = 5f;

    private float rotX = 0f;
    private float velocidadRotX = 0f;
    private float velocidadRotY = 0f;
    private float rotacionActualX = 0f;
    private float rotacionActualY = 0f;

    private void Start()
    {
        transformPadre = transform.parent;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        MovimientoCamara();
    }

    private void MovimientoCamara()
    {
        entradaX = Input.GetAxis("Mouse X") * sensibilidadX * Time.deltaTime;
        entradaY = Input.GetAxis("Mouse Y") * sensibilidadY * Time.deltaTime;

        rotX -= entradaY;
        rotX = Mathf.Clamp(rotX, -90f, 90f);

        rotacionActualX = Mathf.Lerp(rotacionActualX, rotX, tiempoSuavizado * Time.deltaTime);
        rotacionActualY = Mathf.Lerp(rotacionActualY, entradaX, tiempoSuavizado * Time.deltaTime);

        transform.localRotation = Quaternion.Euler(rotacionActualX, 0f, 0f);
        transformPadre.Rotate(0f, rotacionActualY, 0f);
    }
}