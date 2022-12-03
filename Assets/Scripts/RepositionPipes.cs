using UnityEngine;
using Random = UnityEngine.Random;

public class RepositionPipes : Reposition
{
    [SerializeField] private float minYOffset;
    [SerializeField] private float maxYOffset;

    private void OnEnable()
    {
        SetRandomYOffset();
        _targetTag = GameObject.FindWithTag("PipeReset").tag;
    }

    protected override void ResetPosition()
    {
        transform.position = resetPosition;
        SetRandomYOffset();
    }

    private void SetRandomYOffset()
    {
        var yOffset = Random.Range(minYOffset, maxYOffset);
        var newPosition = new Vector3(transform.position.x, yOffset, transform.position.z);
        transform.position = newPosition;

    }
}
