using UnityEngine;

/// <summary>
/// Gestor principal del puzzle de pernos que controla el orden correcto ]
/// de apriete.
/// </summary>
public class BoltPuzzleManager : MonoBehaviour
{
    // Array con referencias a todos los pernos en el puzzle
    public Bolt[] bolts;

    // Referencia a los paneles UI que se muestran al completar 
    // o fallar el puzzle
    public GameObject successPanel;
    public GameObject failPanel;

    // Índice que rastrea en qué paso del puzzle se encuentra el jugador
    private int currentStep = 0;

    // Array que define el orden correcto en que deben apretarse los pernos
    public int[] correctOrder = {1,5,3,7,2,6,4,8};

    // Inicializa el puzzle ocultando los paneles de resultado
    void Start()
    {
        successPanel.SetActive(false);
        failPanel.SetActive(false);
    }

    /// <summary>
    /// Funcion que procesa cuando se hace click en un perno
    /// </summary>
    /// <param name="bolt">El perno en el que se hizo click</param>
    public void BoltClicked(Bolt bolt)
    {
        // Si el perno ya ha sido completado, ignora el click
        if (bolt.completed)
            return;

        // Obtiene el número de perno esperado en el paso actual
        int expected = correctOrder[currentStep];

        // Verifica si el perno clickeado es el correcto
        if (bolt.boltNumber == expected)
        {
            // Ejecuta la animación de apriete del perno si es correcto
            bolt.Screw();

            // Avanza al siguiente paso
            currentStep++;

            // Si se han completado todos los pasos, muestra el panel de éxito
            if (currentStep >= correctOrder.Length)
            {
                successPanel.SetActive(true);
            }
        }
        else
        {
            // Si el perno es incorrecto, muestra el panel de fallo
            failPanel.SetActive(true);
        }
    }

    /// <summary>
    /// Funcion para reiniciar el puzzle a su estado inicial por via boton del UI
    /// para cuando el jugador falle o complete con exito el puzzle.
    /// </summary>
    public void ResetPuzzle()
    {
        // Reinicia el contador de pasos
        currentStep = 0;

        // Oculta los paneles de éxito y fallo
        successPanel.SetActive(false);
        failPanel.SetActive(false);

        // Loop para reiniciar cada perno a su estado inicial
        foreach (Bolt bolt in bolts)
        {
            bolt.ResetBolt();
        }
    }
}