using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerSkin : MonoBehaviour {

    public Texture[] _textures;
    public int _num;
	// Use this for initialization
	void Start () {
        SetSkin(_num);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.T))
        {
            SetSkin(_num);
        }
	}

    public void SetSkin(int num)
    {
        GetComponentInChildren<Renderer>().material.mainTexture = _textures[num];

    }
}
