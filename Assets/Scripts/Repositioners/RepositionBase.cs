using UnityEngine;

public class RepositionBase : Reposition
{
    [SerializeField] private GameObject targetCollider;

    protected override string GetTargetTag()
    {
        return targetCollider.tag;
    }
}
