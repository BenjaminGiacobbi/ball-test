﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // quit function here
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}
