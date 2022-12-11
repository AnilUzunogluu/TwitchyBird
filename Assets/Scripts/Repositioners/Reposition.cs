using UnityEngine;

public abstract class Reposition : MonoBehaviour
{
    [SerializeField] protected Vector3 resetPosition;
    
    protected string _targetTag;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(_targetTag))
        {
            ResetPosition();
        }
    }

    protected virtual void ResetPosition()
    {
        transform.position = resetPosition;
    }
}
