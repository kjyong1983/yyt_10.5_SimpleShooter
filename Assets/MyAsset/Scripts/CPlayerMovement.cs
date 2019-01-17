using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerMovement :Photon. MonoBehaviour {

    float positionX;
    float positionY;
    float rotationX;
    float rotationY;

    [SerializeField]Vector2 direction;
    Vector3 lastRot;

    public float _moveSpeed = 200;
    public float _smooth = 0.5f;

    Rigidbody _rigidbody;
    GameObject _firePos;
    CPlayerAttack _attack;

    Joystick _joyLeft;
    Joystick _joyRight;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _firePos = GetComponentInChildren<FirePos>().gameObject;
        _attack = GetComponent<CPlayerAttack>();

        _joyLeft = GameObject.Find("Joystick L").GetComponent<FloatingJoystick>();
        _joyRight = GameObject.Find("Joystick R").GetComponent<FloatingJoystick>();
        
    }

    // Use this for initialization
    void Start () {
        lastRot = Vector3.forward;
	}
	
	// Update is called once per frame
	void Update () {
        if (!photonView.isMine) return;

        MoveByTouch();
        TurnFirePosByTouch();

        //MoveByKeyboard();
        //TurnFirePosbyKeyboard();

        TurnShip();

    }

    private void MoveByTouch()
    {
        direction = new Vector2(_joyLeft.Horizontal, _joyLeft.Vertical);
        _rigidbody.AddForce(direction * _moveSpeed * Time.deltaTime);
    }

    private void TurnFirePosByTouch()
    {
        if (_joyRight.Horizontal == 0 && _joyRight.Vertical == 0) return;
        
        Quaternion newRot = Quaternion.LookRotation(new Vector3(_joyRight.Horizontal, _joyRight.Vertical));
        lastRot = newRot.eulerAngles;

        _firePos.transform.rotation =
            Quaternion.Slerp(_firePos.transform.rotation, newRot, _smooth);

        //attack
        _attack.Attack(_firePos.transform.forward,
          _firePos.transform.position,
          _firePos.transform.rotation,
          photonView.viewID);
    }

    void MoveByKeyboard()
    {
        if (Input.GetKey(KeyCode.A))
        {
            positionX = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            positionX = 1;
        }
        if (Input.GetKey(KeyCode.W))
        {
            positionY = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            positionY = -1;
        }

        direction = new Vector2(positionX, positionY).normalized;

        if (direction != Vector2.zero)
        {
            //animation
        }

        _rigidbody.AddForce(direction * _moveSpeed * Time.deltaTime);

        positionX = positionY = 0;
    }

    void TurnShip()
    {
        float rotZ = Vector3.Angle(Vector3.up, direction);
        if (direction.x > 0)
        {
            rotZ *= -1;
        }

        if (direction != Vector2.zero)
        {
            transform.rotation = 
                Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, rotZ), _smooth);
        }
    }

    void TurnFirePosbyKeyboard()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rotationX = -1;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rotationX = 1;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rotationY = 1;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            rotationY = -1;
        }

        if (rotationX == 0 && rotationY == 0)
        {
            return;
        }

        Quaternion newRot = Quaternion.LookRotation(new Vector3(rotationX, rotationY));
        lastRot = newRot.eulerAngles;

        _firePos.transform.rotation = 
            Quaternion.Slerp(_firePos.transform.rotation, newRot, _smooth);

        //attack
        _attack.Attack(_firePos.transform.forward,
          _firePos.transform.position,
          _firePos.transform.rotation,
          photonView.viewID);

        rotationX = rotationY= 0;

    }
}
