using UnityEngine;

public class GhostCrossing : MonoBehaviour
{
    [Header("Puntos del recorrido")]
    public Transform pointA;
    public Transform pointB;

    [Header("Movimiento")]
    public float moveSpeed = 1.2f;

    [Header("Animación")]
    public Animator animator;

    private bool moving = false;
    private bool finished = false;

    void Start()
    {
        if (pointA != null)
        {
            transform.position = pointA.position;
        }

        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        if (animator != null)
        {
            animator.SetBool("IsWalking", false);
        }
    }

    void Update()
    {
        if (!moving || finished || pointB == null)
            return;

        Vector3 targetPosition = pointB.position;

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            moveSpeed * Time.deltaTime
        );

        Vector3 direction = targetPosition - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                4f * Time.deltaTime
            );
        }

        if (Vector3.Distance(transform.position, pointB.position) < 0.05f)
        {
            finished = true;
            moving = false;

            if (animator != null)
            {
                animator.SetBool("IsWalking", false);
            }

            gameObject.SetActive(false);
        }
    }

    public void StartCrossing()
    {
        if (finished)
            return;

        moving = true;

        if (animator != null)
        {
            animator.SetBool("IsWalking", true);
        }
    }
}