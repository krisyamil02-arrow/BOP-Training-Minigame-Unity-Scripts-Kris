using UnityEngine;

/// <summary>
/// Representa un perno individual que puede ser apretado en el puzzle
/// </summary>
public class Bolt : MonoBehaviour
{
    // Número único que identifica este perno (usado para verificar el orden correcto)
    public int boltNumber;

    // Referencia al componente Animator para reproducir la animación de apriete
    public Animator animator;

    // Booleana que indica si este perno ya ha sido apretado correctamente
    public bool completed = false;

    /// <summary>
    /// Funcion que ejecuta la animación de apriete del perno
    /// </summary>
    public void Screw()
    {
        // Si el perno ya fue apretado, evita ejecutar la animación nuevamente
        if (completed)
            return;

        // Activa el trigger de animación "Screw"
        animator.SetTrigger("Screw");

        // Marca el perno como completado
        completed = true;
    }

    /// <summary>
    /// Funcion utilizada para reiniciar el perno a su estado inicial
    /// </summary>
    public void ResetBolt()
    {
        // Marca el perno como no completado
        completed = false;

        // Reproduce la animación "Idle" desde el inicio (0 segundos)
        animator.Play("Idle", 0, 0f);
        // Fuerza una actualización inmediata del animator
        animator.Update(0f);
    }
}