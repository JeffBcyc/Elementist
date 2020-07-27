// LookAt.cs

using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LookAt : MonoBehaviour
{
    private Animator animator;
    public Transform head;
    public float lookAtCoolTime = 0.2f;
    public float lookAtHeatTime = 0.2f;

    private Vector3 lookAtPosition;
    public Vector3 lookAtTargetPosition;
    private float lookAtWeight;
    public bool looking = true;

    private void Start()
    {
        if (!head)
        {
            Debug.LogError("No head transform - LookAt disabled");
            enabled = false;
            return;
        }

        animator = GetComponent<Animator>();
        lookAtTargetPosition = head.position + transform.forward;
        lookAtPosition = lookAtTargetPosition;
    }

    private void OnAnimatorIK()
    {
        lookAtTargetPosition.y = head.position.y;
        var lookAtTargetWeight = looking ? 1.0f : 0.0f;

        var curDir = lookAtPosition - head.position;
        var futDir = lookAtTargetPosition - head.position;

        curDir = Vector3.RotateTowards(curDir, futDir, 6.28f * Time.deltaTime, float.PositiveInfinity);
        lookAtPosition = head.position + curDir;

        var blendTime = lookAtTargetWeight > lookAtWeight ? lookAtHeatTime : lookAtCoolTime;
        lookAtWeight = Mathf.MoveTowards(lookAtWeight, lookAtTargetWeight, Time.deltaTime / blendTime);
        animator.SetLookAtWeight(lookAtWeight, 0.2f, 0.5f, 0.7f, 0.5f);
        animator.SetLookAtPosition(lookAtPosition);
    }
}