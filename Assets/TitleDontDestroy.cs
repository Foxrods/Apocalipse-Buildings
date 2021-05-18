using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleDontDestroy : MonoBehaviour
{
    private static TitleDontDestroy instance = null;
    public static TitleDontDestroy Instance
    {
        get { return instance; }
    }
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
