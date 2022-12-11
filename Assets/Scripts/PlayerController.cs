using UnityEngine;
using UnityEngine.Events;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float upwardsForce;
    [SerializeField] private float rapidTapVelocityReducer = 0.8f;
    [SerializeField] private float rapidTapTimeThreshold = 0.2f;

    [Header("Rotation Variables")]
    [SerializeField] private float upwardsRotationAngle;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float fallRotationAngleMultiplier = 20f;
    [SerializeField] private float increasingRotationSpeedMultiplier = 0.3f;


    [SerializeField, Space] private UnityEvent onTap;
    [SerializeField] private UnityEvent onPoint;
    [SerializeField] private UnityEvent onDeath;

    private Rigidbody2D _rigidbody;
    private float _lastClickTime;
    private GameObject _birdSprite;
    private bool _isAlive = true;


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _birdSprite = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        if (!_isAlive)
        {
            _birdSprite.GetComponent<Animator>().SetTrigger("Dead");
            return;
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            onTap?.Invoke();

            if (TimeBetweenTaps(_lastClickTime) > rapidTapTimeThreshold)
            {            
                _rigidbody.velocity = Vector3.zero;
            }
            else
            {
                _rigidbody.velocity *= rapidTapVelocityReducer;
            }
            
            _lastClickTime = Time.time;
            _rigidbody.AddForce(Vector3.up * upwardsForce, ForceMode2D.Force);
        }
        
        SetRotation();
    }

    private float TimeBetweenTaps(float lastClick)
    {
        var currentTime = Time.time;
        return currentTime - lastClick;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("ScoreTrigger"))
        {
            onPoint?.Invoke();
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if ((col.gameObject.CompareTag("Pipe") || col.gameObject.CompareTag("Base")) && _isAlive)
        {
            Die();
        }
    }

    private void SetRotation()
    {
        var yVelocity = _rigidbody.velocity.y;

        if (yVelocity < 0)
        {
            FallRotation(yVelocity);
        }
        else
        {
            UpRotation();
        }
    }

    private void FallRotation(float yVelocity)
    {
        var rotationAngle = yVelocity * fallRotationAngleMultiplier;
        
        if (rotationAngle < -90)
        {
            rotationAngle = -90;
        }
        
        var targetRotation = Quaternion.Euler(Vector3.forward * rotationAngle);
        var increasingRotationSpeed = Mathf.Abs(yVelocity * increasingRotationSpeedMultiplier * rotationSpeed);
        SetBirdSpriteRotation(targetRotation, increasingRotationSpeed);
    }

    private void UpRotation()
    {
        var targetRotation = Quaternion.Euler(Vector3.forward * upwardsRotationAngle);
        
        SetBirdSpriteRotation(targetRotation, rotationSpeed * 5);
    }

    private void SetBirdSpriteRotation(Quaternion targetRotation, float rotationSpeed)
    {
        _birdSprite.transform.rotation = Quaternion.RotateTowards(_birdSprite.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void Die()
    {
        _isAlive = false;
        onDeath?.Invoke();
        FallRotation(float.MinValue);
        GameManager.Instance.SetGameOver();
    }
    
}
