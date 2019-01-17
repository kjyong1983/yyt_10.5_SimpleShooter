using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolTest : MonoBehaviour {

    public CObjectPool _pool;

    void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            _pool.ActivatePoolable(transform.position, Quaternion.identity);
        }
	}
}
