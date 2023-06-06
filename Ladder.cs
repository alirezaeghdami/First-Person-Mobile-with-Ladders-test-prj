using UnityEngine;

public class Ladder : MonoBehaviour
{
    [SerializeField]
    private Transform bottomCheck, topCheck;
    [SerializeField]
    private Transform bottomTransform, topTransform;

    [SerializeField]
    private float checkRadius = 1;
    [SerializeField]
    private LayerMask playerLayer;

    [SerializeField]
    private GameObject climbButton;

    private PlayerTouchMovement playerMovement;

    void Start()
    {
        playerMovement = FindObjectOfType<PlayerTouchMovement>();
        climbButton.SetActive(false);
    }

    void Update()
    {
        if (Physics.CheckSphere(bottomCheck.position, checkRadius, playerLayer) && !playerMovement.isClimbing)
        {
            playerMovement.ladder = bottomCheck;
            climbButton.SetActive(true);
        }
        else
        {
            playerMovement.ladder = null;
            climbButton.SetActive(false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(bottomCheck.position, checkRadius);
        Gizmos.DrawWireSphere(topCheck.position, checkRadius);
    }
}
