using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CObjectPool : MonoBehaviour {

    public GameObject _poolablePrefab;
    GameObject[] _poolObjects;
    [SerializeField]int _iter= 0;
    public int _objectNum = 10;

	void Start () {

        _poolObjects = new GameObject[_objectNum];

        for (int i = 0; i < _objectNum; i++)
        {
            GameObject g = Instantiate(_poolablePrefab, transform);
            g.GetComponent<CPoolable>()._pool = this;
            g.SetActive(false);
            _poolObjects[i] = g;
        }	
	}
	
    public void ActivatePoolable(Vector3 pos, Quaternion qt)
    {
        if (_iter >= _objectNum)
        {
            _iter -= _objectNum;
        }
        GameObject o = _poolObjects[_iter];
        o.transform.position = pos;
        o.transform.rotation = qt;

        var renderer = o.GetComponent<TrailRenderer>();
        if (renderer != null)
        {
            renderer.time = 10;
            //renderer.enabled = true;
        }

        o.SetActive(true);
        _iter++;
     
    }

    public void ResetPoolable(GameObject o)
    {
        o.SetActive(false);
        var renderer = o.GetComponent<TrailRenderer>();
        if (renderer != null)
        {
            renderer.time = 0;
//            renderer.enabled = false;
        }

        o.transform.position = Vector3.zero;
        o.transform.rotation = Quaternion.identity;

    }

	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, 2, 0));
    }
}
