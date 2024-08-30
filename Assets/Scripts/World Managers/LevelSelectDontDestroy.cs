using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectDontDestroy : MonoBehaviour
{
    private static LevelSelectDontDestroy instance;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
