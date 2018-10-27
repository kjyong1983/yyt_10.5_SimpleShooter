using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CFollowCam : MonoBehaviour {

    public Transform _target;
    public Vector3 _offset;
    public float _smooth;

    // Use this for initialization
	public void Init(Transform target) {

        _target = target;
        transform.position = Vector3.zero;
        transform.position = _target.position + _offset;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if (_target == null) return;

        transform.position =
            Vector3.Lerp(transform.position, _target.position + _offset, _smooth);
	}
}
