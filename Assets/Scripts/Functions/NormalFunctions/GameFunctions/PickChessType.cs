using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickChessType : MonoBehaviour
{
    public ChessType slottype;

    public void Awake()
    {
        this.GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                RaycastInGame.GetInstance().ReadyForDrop();
                RaycastInGame.GetInstance().PutChessType = slottype;
            });
    }
}
