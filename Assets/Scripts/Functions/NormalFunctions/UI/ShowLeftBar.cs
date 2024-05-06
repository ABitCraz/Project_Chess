using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShowLeftBar : MonoBehaviour
{
    public GameObject LeftBar;
    public GameObject PlayerStatus;
    bool isleftopen = false;

    void Awake()
    {
        this.GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                if (!isleftopen)
                {
                    this.transform.GetChild(0).GetComponent<TMP_Text>().text = "<";
                    isleftopen = true;
                }
                else
                {
                    this.transform.GetChild(0).GetComponent<TMP_Text>().text = ">";
                    isleftopen = false;
                }
            });
    }

    private void Update()
    {
        RectTransform lbr = LeftBar.GetComponent<RectTransform>();
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
            if (lbr.anchoredPosition.x > -150f)
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
