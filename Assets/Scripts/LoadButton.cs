using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadButton : MonoBehaviour
{
    [SerializeField] private string sceneNameToLoad;
    [SerializeField] private float loadDelay;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void StartGame()
    {
        _animator.SetTrigger("Pressed");
        StartCoroutine(LoadScene(sceneNameToLoad));
    }
    private IEnumerator LoadScene(string sceneName)
    {
        yield return new WaitForSeconds(loadDelay);
        SceneManager.LoadScene(sceneName);
    }
    
    
    
}
