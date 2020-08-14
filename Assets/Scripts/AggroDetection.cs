using System;
using UnityEngine;

public class AggroDetection : MonoBehaviour
{
    public event Action<Transform> OnAggro = delegate { };

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerMovement>();
        if (player != null)
        {
            OnAggro(player.transform);
            Debug.Log("Aggrod");
        }
    }
}
