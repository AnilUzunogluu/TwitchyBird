using UnityEngine;
using Random = UnityEngine.Random;

public class RepositionPipes : Reposition
{
    [SerializeField] private float minYOffset;
    [SerializeField] private float maxYOffset;

    protected override string GetTargetTag()
    {
        return GameObject.FindWithTag("PipeReset").tag;
    }
    
    private void Start()
    {
        SetRandomYOffset();
    }

    protected override void ResetPosition()
    {
        base.ResetPosition();
        SetRandomYOffset();
    }

    private void SetRandomYOffset()
    {
        var yOffset = Random.Range(minYOffset, maxYOffset);
        var newPosition = new Vector3(transform.position.x, yOffset, transform.position.z);
        transform.position = newPosition;
    }
}
