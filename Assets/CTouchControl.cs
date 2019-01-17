using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CTouchControl : MonoBehaviour/*, IPointerDownHandler, IPointerUpHandler*/ {

    RectTransform _touchPad;
    Vector3 _startPos = Vector3.zero;
    float _dragRadius = 60f;

    private int _touchId = -1;
    private bool _buttonPressed;

    public Vector2 _normDiff;

    private void Awake()
    {
        _touchPad = GetComponent<RectTransform>();
        _startPos = _touchPad.position;

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        HandleTouchInput();
        Debug.Log(_normDiff);
    }

    private void HandleTouchInput()
    {
        int i = 0;

        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                i++;
                Vector3 touchPos = new Vector3(touch.position.x, touch.position.y);

                if (touch.phase == TouchPhase.Began)
                {
                    if (touch.position.x <= (_startPos.x + _dragRadius))
                    {
                        _touchId = i;
                    }
                }

                if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                {
                    if (_touchId == i)
                    {
                        HandleInput(touchPos);
                    }
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    if (_touchId == i)
                    {
                        _touchId = -1;
                    }
                }


            }
        }

    }

    private void HandleInput(Vector3 input)
    {
        if (_buttonPressed)
        {
            Vector3 diffVector = (input - _startPos);
            if (diffVector.sqrMagnitude > _dragRadius * _dragRadius)
            {
                diffVector.Normalize();
                _touchPad.position = _startPos + diffVector * _dragRadius;
            }
            else
            {
                _touchPad.position = input;
            }

        }
        else
        {
            _touchPad.position = _startPos;
        }

        Vector3 diff = _touchPad.position - _startPos;
        _normDiff = new Vector3(diff.x / _dragRadius, diff.y / _dragRadius);

    }
}
