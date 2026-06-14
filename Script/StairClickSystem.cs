using UnityEngine;

/// <summary>
/// Sistema que revisa si el jugador subio las escaleras para iniciar el
/// puzzle, y utilizando raycast para detecta clicks del ratón en los pernos
/// desde la cámara de escaleras.
/// </summary>
public class StairClickSystem : MonoBehaviour
{
    // Booleana que indica si el sistema está activo y debe procesar clicks
    public bool active = false;

    // Referencia a la cámara de escaleras para hacer raycast
    public Camera stairCamera;

    // Referencia al script gestor del puzzle de pernos
    public BoltPuzzleManager puzzleManager;

    /// <summary>
    /// Procesa los clicks del ratón en cada frame si el sistema está activo
    /// </summary>
    void Update()
    {
        // Si el sistema no está activo, no procesa clicks
        if (!active)
            return;

        // Detecta clicks del botón izquierdo del ratón
        if (Input.GetMouseButtonDown(0))
        {
            // Crea un rayo desde la cámara hacia la posición del ratón
            Ray ray = stairCamera.ScreenPointToRay(Input.mousePosition);

            // Realiza un raycast para detectar si el rayo golpea algún colider
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // Intenta obtener el componente Bolt del objeto golpeado
                Bolt bolt = hit.collider.GetComponent<Bolt>();

                // Si se encontró un perno, notifica al gestor del puzzle
                if (bolt != null)
                {
                    puzzleManager.BoltClicked(bolt);
                }
            }
        }
    }
}