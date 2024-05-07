using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tests : MonoBehaviour
{
    int i = 0;

    private void Awake()
    {
        foreach (var b in A())
        {
            print(b);
        }
    }

    private IEnumerable<WaitForEndOfFrame> A()
    {
        while (i > -1)
        {
            i += 1;
            print(i);
            yield return new WaitForEndOfFrame();
        }
    }
}
