using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RunButton : MonoBehaviour
{
    public void Run()
    {
        GameManager._instance.Run();
    }
}
