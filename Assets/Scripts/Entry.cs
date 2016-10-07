using UnityEngine;
using UnityEngine.SceneManagement;

using System;
using System.Collections;

using Base;

public class Entry : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene(StageEnum.Battle.ToString());
    }


}

