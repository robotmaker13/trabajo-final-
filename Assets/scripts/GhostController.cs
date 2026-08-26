using UnityEngine;

public class GhostController : MonoBehaviour
{
    [Header("Objetivo")]
    public Transform player;

    [Header("Movimiento")]
    public float moveSpeed = 2.2f;
    public float stopDistance = 1.5f;

    [Header("Animación")]
    public Animator animator;

    [Header("Cámara de ataque")]
    public Transform attackCameraPoint;
    public Transform mainCamera;
    public float cameraMoveSpeed = 3f;

    [Header("Control del jugador")]
    public AgentXController playerController;

    [Header("Game Over")]
    public GameOverUI gameOverUI;

    private bool chasePlayer = false;
    private bool attacking = false;
    private bool attackCameraActive = false;

    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        if (animator != null)
        {
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsRunning", false);
        }
    }

    void Update()
    {
        // Mover la cámara hacia el punto de ataque
        if (attackCameraActive &&
            attackCameraPoint != null &&
            mainCamera != null)
        {
            mainCamera.position = Vector3.Lerp(
                mainCamera.position,
                attackCameraPoint.position,
                cameraMoveSpeed * Time.deltaTime
            );

            mainCamera.rotation = Quaternion.Slerp(
                mainCamera.rotation,
                attackCameraPoint.rotation,
                cameraMoveSpeed * Time.deltaTime
            );
        }

        // Si todavía no persigue o ya está atacando, no seguir moviendo
        if (!chasePlayer || player == null || attacking)
            return;

        Vector3 direction =
            player.position - transform.position;

        direction.y = 0f;

        float distance =
            Vector3.Distance(
                transform.position,
                player.position
            );

        // Persecución
        if (distance > stopDistance)
        {
            if (direction.sqrMagnitude > 0.001f)
            {
                Quaternion targetRotation =
                    Quaternion.LookRotation(direction);

                transform.rotation =
                    Quaternion.Slerp(
                        transform.rotation,
                        targetRotation,
                        5f * Time.deltaTime
                    );
            }

            transform.position +=
                direction.normalized *
                moveSpeed *
                Time.deltaTime;

            if (animator != null)
            {
                animator.SetBool("IsWalking", true);
                animator.SetBool("IsRunning", true);
            }
        }
        else
        {
            StartAttack();
        }
    }

    public void StartChase()
    {
        if (attacking)
            return;

        chasePlayer = true;
    }

    void StartAttack()
    {
        if (attacking)
            return;

        attacking = true;
        chasePlayer = false;

        // Frenar animación de persecución
        if (animator != null)
        {
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsRunning", false);
            animator.SetTrigger("Attack");
        }

        // Bloquear a Leona
        if (playerController != null)
        {
            playerController.enabled = false;
        }

        // Activar cámara de ataque
        attackCameraActive = true;

        // Activar secuencia de Game Over
        if (gameOverUI != null)
        {
            gameOverUI.StartGameOver();
        }
    }
}