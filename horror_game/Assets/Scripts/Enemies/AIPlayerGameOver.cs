using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AIPlayerGameOver : MonoBehaviour
{
    [field: SerializeField]
    public bool playerHit {get; private set;}

    [Header("Overlap parameters")]
    private Transform detectorOrigin;
    public Vector2 detectorSize = Vector2.one;
    public Vector2 detectorOffset = Vector2.zero;
    public float detectionDelay = 0.3f;
    public LayerMask detectionLayerMask;
    

    [Header("Gizmo parameters")]
    [SerializeAs("")]
    public Color gizmoIdleColor = Color.green;
    public Color gizmoDetectedColor = Color.red;
    public bool showGizmos = true;
    public bool isCube = true;

    public GameObject target;

    public GameObject Target
    {
        get => target;
        private set
        {
            target = value;
            playerHit = target != null;
        }
    }

    private void Start()
    {
        detectorOrigin = transform;
        StartCoroutine(DetectionCoroutine());
    }

    IEnumerator DetectionCoroutine()
    {
        yield return new WaitForSeconds(detectionDelay);
        PerformDetection();
        StartCoroutine(DetectionCoroutine());
    }

    public void PerformDetection()
    {
        Collider2D collider = Physics2D.OverlapBox((Vector2)detectorOrigin.position + detectorOffset, detectorSize, 0f, detectionLayerMask);
        if (collider != null)
        {
            Target = collider.gameObject;
        }
        else
        {
            Target = null;
        }
    }

    private void OnDrawGizmos()
    {
        if (showGizmos && detectorOrigin != null) {
            Gizmos.color = gizmoIdleColor;
            if (playerHit) {
                Gizmos.color = gizmoDetectedColor;
            }
            
            Gizmos.DrawWireCube((Vector2)detectorOrigin.position + detectorOffset, detectorSize);
        }
    }
}
