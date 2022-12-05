using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadButton : MonoBehaviour
{
    [SerializeField] private string SceneNameToLoad;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void StartGame()
    {
        _animator.SetTrigger("Pressed");
        StartCoroutine(LoadScene(SceneNameToLoad));
    }
    private IEnumerator LoadScene(string sceneName)
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneName);
    }
    
    
    
}
