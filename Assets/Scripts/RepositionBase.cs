using UnityEngine;

public class RepositionBase : Reposition
{
    [SerializeField] private GameObject targetCollider;

    private void Start()
    {
        _targetTag = targetCollider.tag;
    }
    
    protected override void ResetPosition()
    {
        transform.position = resetPosition;
    }
}