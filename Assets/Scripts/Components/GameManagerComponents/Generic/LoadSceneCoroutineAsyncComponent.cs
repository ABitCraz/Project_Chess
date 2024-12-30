using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneCoroutineAsyncComponent : MonoBehaviour
{
    public string Target;
    public GameObject LoadSceneButton;

    private void Awake()
    {
        StartCoroutine(LoadTargetScene(Target));
    }

    IEnumerator LoadTargetScene(string sceneTarget)
    {
        AsyncOperation scene_loader = SceneManager.LoadSceneAsync(sceneTarget);
        while (true)
        {
            if (scene_loader.isDone)
            {
                yield return false;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
