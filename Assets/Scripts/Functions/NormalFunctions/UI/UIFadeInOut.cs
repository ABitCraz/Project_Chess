using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFadeInOut : MonoBehaviour
{
    public List<CallMenu> callMenus = new List<CallMenu>();
    public GameObject ExitGameButton;

    private void Awake()
    {
        ExitGameButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            Application.Quit();
        });

        if(callMenus.Count > 0)
        {
            for (int i = 0; i < callMenus.Count; i++)
            {
                var target = callMenus[i];
                target.CallMenuButton.GetComponent<Button>().onClick.AddListener(() =>
                {
                    StartCoroutine(FadeIn(target.TargetPanel.GetComponent<CanvasGroup>()));
                    StartCoroutine(FadeOut(target.PrevPanel.GetComponent<CanvasGroup>()));
                });
            }
        }
    }

    IEnumerator FadeIn(CanvasGroup group)
    {
        while(group.alpha < 1)
        {
            group.alpha += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator FadeOut(CanvasGroup group)
    {
        while (group.alpha > 0)
        {
            group.alpha -= Time.deltaTime;
            yield return null;
        }
    }
}

[System.Serializable]
public class CallMenu
{
    public GameObject PrevPanel;
    public GameObject CallMenuButton;
    public GameObject TargetPanel;
}