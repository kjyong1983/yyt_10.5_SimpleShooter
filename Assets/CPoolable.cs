using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPoolable : MonoBehaviour {

    public CObjectPool _pool;
    public float _speed = 2f;

    private void OnEnable()
    {
        Invoke("objectReset", 3f);
    }

    void objectReset()
    {
        _pool.ResetPoolable(gameObject);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.right * _speed);
	}
}
