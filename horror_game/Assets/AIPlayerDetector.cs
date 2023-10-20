using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayerDetector : MonoBehaviour
{
    [field: SerializeField]
    public bool playerDetected {get; private set;}
    public Vector2 directionToTarget => target.transform.position - detectorOrigin.position;

    [Header("Overlap parameters")]
    [SerializeField] 
    private Transform detectorOrigin;
    public Vector2 detectorSize = Vector2.one;
    public Vector2 detectorOffset = Vector2.zero;
    public float detectionDelay = 0.3f;
    public LayerMask detectionLayerMask;

    [Header("Gizmo parameters")]
    public Color gizmoIdleColor = Color.green;
    public Color gizmoDetectedColor = Color.red;
    public bool showGizmos = true;

    public GameObject target;

    public GameObject Target
    {
        get => target;
        private set
        {
            target = value;
            playerDetected = target != null;
        }
    }

    private void Start()
    {
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
            if (playerDetected) {
                Gizmos.color = gizmoDetectedColor;
            }
            Gizmos.DrawCube((Vector2)detectorOrigin.position + detectorOffset, detectorSize);
        }
    }
}
