using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _xCoefficient = 80;
    [SerializeField] private float _yCoefficient = 80;

    private Rigidbody _rigidbody;
    private Vector2 _tapping;
    private Vector2 _release;
    private bool _grounded;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _tapping = Input.mousePosition;

            Debug.Log("Touch Position down: " + _tapping);
        }

        if (Input.GetMouseButtonUp(0))
        {
            _release = Input.mousePosition;

            Debug.Log("Touch Position up: " + _release);

            if (_grounded)
                PushObject();
        }

        if (Input.GetMouseButton(0))
        {
            DrawGizmos();
        }
    }

    private void PushObject()
    {
        Vector3 point = transform.position;
        Vector3 start = point;
        Debug.Log("start point " + point);
        if (_tapping.y < _release.y)
        {
            point.y = (_release.y - _tapping.y) / _yCoefficient;
        }
        else
        {
            point.y = (_tapping.y - _release.y) / _yCoefficient * -1;
        }

        if (_tapping.x < _release.x)
        {
            point.x = (_release.x - _tapping.x) / _xCoefficient;
        }
        else
        {
            point.x = (_tapping.x - _release.x) / _xCoefficient * -1;
        }

        Vector3 direction = new Vector3(point.x * -1, point.y * -1, 0);
        Debug.Log("new point " + direction);
        Debug.DrawRay(start, direction, Color.yellow, 30f);
        _rigidbody.AddForce(direction, ForceMode.Impulse);
    }

    private void DrawGizmos()
    {
        Vector3 point = transform.position;
        Vector3 start = point;

        Vector3 vector3rel = Input.mousePosition;

        if (_tapping.y < vector3rel.y)
        {
            point.y = (vector3rel.y - _tapping.y) / _yCoefficient;
        }
        else
        {
            point.y = (_tapping.y - vector3rel.y) / _yCoefficient * -1;
        }

        if (_tapping.x < vector3rel.x)
        {
            point.x = (vector3rel.x - _tapping.x) / _xCoefficient;
        }
        else
        {
            point.x = (_tapping.x - vector3rel.x) / _xCoefficient * -1;
        }

        Vector3 direction = new Vector3(point.x * -1, point.y * -1, 0);
        Debug.DrawRay(start, direction, Color.yellow);
    }

    private void OnCollisionStay(Collision collision)
    {
        _grounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        _grounded = false;
    }
}
