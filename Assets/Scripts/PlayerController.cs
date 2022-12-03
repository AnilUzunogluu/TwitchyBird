using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float upwardsForce;
    
    
    [SerializeField] private UnityEvent onTap;
    [SerializeField] private UnityEvent onPoint;


    private Rigidbody2D _rigidbody;
    private float _lastClickTime;


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            onTap?.Invoke();

            if (TimeBetweenTaps(_lastClickTime) > 0.2f)
            {            
                _rigidbody.velocity = Vector3.zero;
            }
            
            _lastClickTime = Time.time;
            
            _rigidbody.AddForce(Vector3.up * upwardsForce, ForceMode2D.Force);
        }
    }

    private float TimeBetweenTaps(float lastClick)
    {
        var currentTime = Time.time;
        return currentTime - lastClick;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Pipe"))
        {
            onPoint?.Invoke();
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Pipe"))
        {
            Debug.Log("Dead!");
        }
    }
}
