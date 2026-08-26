using UnityEngine;

public class GhostCrossingTrigger : MonoBehaviour
{
    public GhostCrossing ghost;

    private bool activated = false;

    void OnTriggerEnter(Collider other)
    {
        if (activated)
            return;

        if (other.CompareTag("Player"))
        {
            activated = true;

            if (ghost != null)
            {
                ghost.StartCrossing();
            }
        }
    }
}
