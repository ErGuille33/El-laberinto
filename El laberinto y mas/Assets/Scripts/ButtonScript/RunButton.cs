using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunButton : MonoBehaviour
{
    public void useHint()
    {
        GameManager._instance.Run();
    }
}
