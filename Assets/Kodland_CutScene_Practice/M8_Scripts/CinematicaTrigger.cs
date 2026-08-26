using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CinematicaTrigger : MonoBehaviour
{
    [SerializeField] PlayableDirector cinematica;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cinematica.Play();
            Destroy(gameObject);
        }
    }
}