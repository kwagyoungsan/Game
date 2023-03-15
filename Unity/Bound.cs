﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bound : MonoBehaviour
{

    private BoxCollider2D bound;

    private CameraManager theCamera;

    // Start is called before the first frame update
    void Start()
    {
        bound = FindObjectOfType<BoxCollider2D>();  
        theCamera = FindObjectOfType<CameraManager>();
        theCamera.setBound(bound);
    }

    
}
