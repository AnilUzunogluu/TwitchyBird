using UnityEngine;

public class ScrollLeft : MonoBehaviour
{
    [SerializeField] private float speed = 2f;

    private void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }


    public void GameOver()
    {
        speed = 0f;
    }
    
}
