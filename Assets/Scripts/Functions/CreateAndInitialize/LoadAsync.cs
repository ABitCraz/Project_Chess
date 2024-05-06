using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadAsync : MonoBehaviour
{
    public string Target;
    public GameObject LoadSceneButton;
    private Coroutine sceneLoad;

    private void Awake()
    {
        LoadSceneButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            sceneLoad ??= StartCoroutine(LoadTargetScene());
        });
    }

    IEnumerator LoadTargetScene()
    {
        var loadasync = SceneManager.LoadSceneAsync(Target);

        while(true)
        {
            if(loadasync.isDone)
            {
                yield return new WaitForEndOfFrame();
            }
            else
            {
                StopCoroutine(sceneLoad);
                sceneLoad = null;
                yield return false;
            }
        }
    }
}
