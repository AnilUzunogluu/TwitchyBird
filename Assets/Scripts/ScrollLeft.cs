using UnityEngine;

public class ScrollLeft : MonoBehaviour
{
    [SerializeField] private float speed = 2f;

    private void Update()
    {
        MoveLeft();
    }

    private void MoveLeft()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }


    public void GameOver()
    {
        speed = 0f;
    }
    
}
