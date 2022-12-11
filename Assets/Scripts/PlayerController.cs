using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float upwardsForce;
    
    [Header("Rotation Variables")]
    [SerializeField] private float upwardsRotationAngle;
    [SerializeField] private float rotationSpeed;
    
    
    [SerializeField] private UnityEvent onTap;
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

            if (TimeBetweenTaps(_lastClickTime) > 0.2f)
            {            
                _rigidbody.velocity = Vector3.zero;
            }
            
            _lastClickTime = Time.time;
            _rigidbody.velocity *= 0.8f;
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
        var rotationAngle = yVelocity * 20;
        
        if (rotationAngle < -90)
        {
            rotationAngle = -90;
        }
        
        var targetRotation = Quaternion.Euler(new Vector3(0f, 0f, rotationAngle));
        var increasingRotationSpeed = Mathf.Abs((float) (yVelocity * 0.3 * rotationSpeed));
        _birdSprite.transform.rotation = Quaternion.RotateTowards(_birdSprite.transform.rotation, targetRotation, increasingRotationSpeed * Time.deltaTime);
    }

    private void UpRotation()
    {
        var targetRotation = Quaternion.Euler(new Vector3(0f, 0f, upwardsRotationAngle));
        _birdSprite.transform.rotation = Quaternion.RotateTowards(_birdSprite.transform.rotation, targetRotation, rotationSpeed * 5 * Time.deltaTime);
    }

    private void Die()
    {
        _isAlive = false;
        onDeath?.Invoke();
        FallRotation(-400f);
        GameManager.Instance.SetGameOver();
    }
    
}
