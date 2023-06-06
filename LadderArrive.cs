using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderArrive : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        PlayerTouchMovement player = col.GetComponent<PlayerTouchMovement>();

        if (player != null)
        {
            if (player.isClimbing)
            {
                player.StopClimb(transform);
            }
        }
    }
}
