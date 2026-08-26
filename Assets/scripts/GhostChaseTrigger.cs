using UnityEngine;

public class GhostChaseTrigger : MonoBehaviour
{
    public GhostController ghost;

    private bool activated = false;

    void OnTriggerEnter(Collider other)
    {
        if (activated)
            return;

        if (other.CompareTag("Player"))
        {
            activated = true;

            if (ghost != null)
                ghost.StartChase();
        }
    }
}