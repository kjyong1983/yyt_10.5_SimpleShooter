using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrap : MonoBehaviour {
    private bool isWrappingX;
    private bool isWrappingY;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Wrap()
    {
        bool isVisible = CheckRenderers();

        if (isVisible)
        {
            isWrappingX = false;
            isWrappingY = false;
            return;
        }

        if (isWrappingX && isWrappingY)
        {
            return;
        }

        Vector3 newPosition = transform.position;

        if (newPosition.x > 1 || newPosition.y < 0)
        {

        }
    }

    private bool CheckRenderers()
    {
        throw new NotImplementedException();
    }
}
