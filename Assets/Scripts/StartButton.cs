using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void StartGame()
    {
        _animator.SetTrigger("Pressed");
        StartCoroutine(LoadGame());
    }
    private IEnumerator LoadGame()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Game");
    }
    
    
    
}
