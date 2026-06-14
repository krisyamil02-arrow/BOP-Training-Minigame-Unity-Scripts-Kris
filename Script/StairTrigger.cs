using UnityEngine;

/// <summary>
/// Controla el sistema de trigger para acceder a las escaleras con 
/// cámaras alternativas
/// PlayerCam: Cámara principal en primera persona del jugador, 
/// con control de movimiento y visión normal.
/// StairCam: Cámara estatico para las escaleras, 
/// con un ángulo que permita ver los pernos.
/// </summary>
public class StairTrigger : MonoBehaviour
{
    // Referencia al texto de UI que indica al jugador que puede interactuar
    public GameObject uiText;

    // Cámaras: primera persona del jugador y cámara estratégica 
    // de las escaleras
    public Camera playerCam;
    public Camera stairCam;

// Referencias a los canvas de UI para el jugador y el puzzle de las escaleras
    public GameObject playerCanvas;     // Canvas de UI para el jugador en modo libre
    public GameObject puzzleCanvas;     // Canvas de UI específico para el puzzle de las escaleras

    // Referencias a componentes para controlar el movimiento del 
    // jugador y el sistema de clicks
    public PlayerMovement playerMovement;
    public StairClickSystem clickSystem;

    // Booleanas para rastrear si el jugador está en el área de trigger 
    // y si está usando la cámara de escaleras
    private bool playerInside = false;
    private bool usingStairCam = false;

    /// <summary>
    /// Inicializa las cámaras y desactiva el UI de interacción
    /// </summary>
    void Start()
{
    uiText.SetActive(false);

    playerCam.gameObject.SetActive(true);
    stairCam.gameObject.SetActive(false);

    playerCanvas.SetActive(true);
    puzzleCanvas.SetActive(false);
}
    /// <summary>
    /// Detecta cuando el jugador entra en el área de trigger
    /// </summary>
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;

            // Muestra el UI si no está usando la cámara de escaleras
            if (!usingStairCam)
                uiText.SetActive(true);
        }
    }

    /// <summary>
    /// Detecta cuando el jugador sale del área de trigger
    /// </summary>
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;

            // Oculta el UI si no está usando la cámara de escaleras
            if (!usingStairCam)
                uiText.SetActive(false);
        }
    }

    /// <summary>
    /// Maneja la entrada del jugador para cambiar entre cámaras
    /// </summary>
    void Update()
    {
        // Detecta presión de la tecla F
        if (Input.GetKeyDown(KeyCode.F))
        {
            // Si no está usando la cámara de escaleras y está dentro 
            // del trigger, cambia a ella
            if (!usingStairCam && playerInside)
            {
                SwitchCamera();
            }
            // Si está usando la cámara de escaleras y vuelve apresionar F, 
            // regresa a la cámara principal
            else if (usingStairCam)
            {
                ReturnCamera();
            }
        }
    }

    /// <summary>
    /// Cambia a la cámara de las escaleras y activa el sistema de clicks
    /// </summary>
void SwitchCamera()
{
    //indica si la camara de la escalera esta activa o no
    usingStairCam = true;

// Activa la cámara de escaleras y desactiva la cámara del jugador
    playerCam.gameObject.SetActive(false);
    stairCam.gameObject.SetActive(true);

//activa la UI del puzzle de las escaleras y desactiva el UI del jugador
    playerCanvas.SetActive(false);
    puzzleCanvas.SetActive(true);

// Desbloquea el cursor para permitir interacción con la cámara de escaleras
    playerMovement.UnlockCursor();
    clickSystem.active = true;

// Oculta el UI de interacción del jugador para evitar conflictos con el puzzle
    uiText.SetActive(false);
}

    /// <summary>
    /// Regresa a la cámara principal del jugador y desactiva 
    /// el sistema de clicks
    /// </summary>
  void ReturnCamera()
{
    // Indica que la cámara de las escaleras ya no está activa
    usingStairCam = false;

// Activa la cámara del jugador y desactiva la cámara de escaleras
    playerCam.gameObject.SetActive(true);
    stairCam.gameObject.SetActive(false);

// Activa el UI del jugador y desactiva el UI del puzzle de las escaleras
    playerCanvas.SetActive(true);
    puzzleCanvas.SetActive(false);

// Bloquea el cursor para volver al control normal del jugador
    playerMovement.LockCursor();
    clickSystem.active = false;

// Si el jugador sigue dentro del trigger, muestra el UI de interacción
    if (playerInside)
        uiText.SetActive(true);
}
}