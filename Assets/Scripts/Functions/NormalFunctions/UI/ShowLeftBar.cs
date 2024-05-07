using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShowLeftBar : MonoBehaviour
{
    public GameObject ShowLeftBarButton;
    public GameObject PlayerStatus;
    public bool isleftopen = false;

    void Awake()
    {
        ShowLeftBarButton.GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                if (!isleftopen)
                {
                    ShowLeftBarButton.transform.GetChild(0).GetComponent<TMP_Text>().text = "<";
                    isleftopen = true;
                }
                else
                {
                    ShowLeftBarButton.transform.GetChild(0).GetComponent<TMP_Text>().text = ">";
                    isleftopen = false;
                }
            });
    }

    private void Update()
    {
        RectTransform lbr = this.GetComponent<RectTransform>();
        RectTransform psr = PlayerStatus.GetComponent<RectTransform>();
        if (isleftopen)
        {
            if (lbr.anchoredPosition.x < 100f)
            {
                lbr.anchoredPosition = new Vector2(lbr.anchoredPosition.x + 1, 0);
            }
            if (psr.sizeDelta.x < 400f)
            {
                psr.sizeDelta = new Vector2(psr.sizeDelta.x + 1, psr.sizeDelta.y);
                psr.GetChild(1).gameObject.SetActive(true);
            }
        }
        else
        {
            if (lbr.anchoredPosition.x > -90f)
            {
                lbr.anchoredPosition = new Vector2(lbr.anchoredPosition.x - 1, 0);
            }
            if (psr.sizeDelta.x > 50f)
            {
                psr.sizeDelta = new Vector2(psr.sizeDelta.x - 1, psr.sizeDelta.y);
                psr.GetChild(1).gameObject.SetActive(false);
            }
        }
    }
}
