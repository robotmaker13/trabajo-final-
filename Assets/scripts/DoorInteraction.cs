using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private float interactionDistance = 2.5f;
    [SerializeField] private float openAngle = 90f;
    [SerializeField] private float rotationSpeed = 3f;

    private Camera playerCamera;
    private Quaternion closedRotation;
    private Quaternion openRotation;
    private bool isOpen;

    private void Start()
    {
        playerCamera = Camera.main;

        if (playerCamera == null)
        {
            Debug.LogError("DoorInteraction: no se encontró una cámara con la etiqueta MainCamera.");
            enabled = false;
            return;
        }

        closedRotation = transform.localRotation;
        openRotation = closedRotation * Quaternion.Euler(0f, openAngle, 0f);
    }

    private void Update()
    {
        Quaternion targetRotation = isOpen ? openRotation : closedRotation;
        transform.localRotation = Quaternion.Slerp(
            transform.localRotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );

        if (Input.GetKeyDown(KeyCode.E) && IsLookingAtDoor())
        {
            isOpen = !isOpen;
        }
    }

    private bool IsLookingAtDoor()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance))
        {
            return hit.transform == transform || hit.transform.IsChildOf(transform);
        }

        return false;
    }
}