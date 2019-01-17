using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {

    CPlayerHealth _health;
    Image _healthImage;

	// Use this for initialization
	void Awake () {

        _health = GetComponentInParent<CPlayerHealth>();
        _healthImage = GetComponent<Image>();
    }
	
	// Update is called once per frame
	void LateUpdate () {
		
	}
}
