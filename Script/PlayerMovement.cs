using UnityEngine;

/// <summary>
/// Script inicial para el movimiento del jugador estando en modo libre.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    // Velocidad de movimiento
    public float speed = 5f;
    // Sensibilidad de la cámara
    public float mouseSensitivity = 2f;
    // Rotación acumulada de la cámara en el eje X
    private float xRotation = 0f; 

    // Referencia a la cámara del jugador en el modo libre
    private Transform cameraTransform;
    // Referencia al CharacterController del jugador
    private CharacterController controller; 
    // Indica si el jugador puede moverse
    public bool canMove = true; 

    /// <summary>
    /// Inicializa las referencias y bloquea el cursor al inicio
    /// </summary>
    void Start()
    {
        // Obtener referencias del controller y la camara
        controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;

        // Bloquear el cursor al inicio del juego
        LockCursor();
    }

    /// <summary>
    /// Procesa el movimiento y la rotación del jugador en cada frame
    /// mientras el jugador no este en el modo libre, para evitar 
    /// conflictos con el puzzle de las escaleras.
    /// </summary>
    void Update()
    {
        // Si no puede moverse, no procesar entradas
        if (!canMove)
            return;
        
        // Procesar movimiento, rotación y vision del jugador
        Move();
        Look();
    }

    /// <summary>
    /// Función para procesar el movimiento del jugador basado 
    /// en entradas de teclado
    /// </summary>
    void Move()
    {
        // Leer entradas de movimiento horizontal y vertical
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Calcular el vector de movimiento relativo al jugador
        Vector3 move = transform.right * x + transform.forward * z;

        // Mover al jugador usando CharacterController
        controller.SimpleMove(move * speed);
    }

    /// <summary>
    /// Función para procesar la rotación de la cámara basada en 
    /// entradas del ratón
    /// </summary>
    void Look()
    {
        // Leer movimiento del ratón e impactar sensibilidad
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Aplicar rotación local en el eje X a la cámara
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotar el cuerpo del jugador en el eje Y
        transform.Rotate(Vector3.up * mouseX);
    }

    /// <summary>
    /// Funcion para desbloquear el cursor si el jugador esta 
    /// en el puzzle de las escaleras 
    /// </summary>
    public void UnlockCursor()
    {
        // Desbloquear y mostrar el cursor, impedir movimiento del jugador
        // Para la camara de la escalera y el puzzle de los pernos
        canMove = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    /// <summary>
    /// Funcion para bloquear el cursor si el jugador esta en el modo libre
    /// </summary>
    public void LockCursor()
    {
        // Bloquear y ocultar el cursor, permitir movimiento del jugador
        // Para la camara principal y el modo libre del jugador
        canMove = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}