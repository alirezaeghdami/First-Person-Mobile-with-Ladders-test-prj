using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbLadder : MonoBehaviour
{
    private PlayerTouchMovement playerMovement;

    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerTouchMovement>();
    }

    public void ClibLadder()
    {
        playerMovement.Climb();
    }
}
