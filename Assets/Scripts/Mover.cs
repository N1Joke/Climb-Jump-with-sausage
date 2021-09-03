using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _xCoefficient = 80;
    [SerializeField] private float _yCoefficient = 80;
    [SerializeField] private LineRendererController _lineRenderer;

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
        }

        if (Input.GetMouseButtonUp(0))
        {
            _release = Input.mousePosition;

            if (_grounded)
                PushObject();
            _lineRenderer.DisablelineRenderer();
        }

        if (Input.GetMouseButton(0))
        {
            DrawGizmos();
        }

        RotateLineRenderer();
    }

    private void PushObject()
    {
        Vector3 start = transform.position;
        Vector3 direction = CalculateDirection();
        _rigidbody.AddForce(direction, ForceMode.Impulse);
    }

    private void DrawGizmos()
    {
        if (_grounded)
        {
            Vector3 start = transform.position;
            Vector3 direction = CalculateDirection();
                        
            _lineRenderer.SetUpLine(new Vector3[2] { new Vector3(0, 0, 0), direction });
            _lineRenderer.EnableLineRenderer();
            Debug.DrawRay(start, direction, Color.yellow);
        }
    }

    private Vector3 CalculateDirection()
    {
        Vector3 point = transform.position;

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

        return direction;
    }

    private void OnCollisionStay(Collision collision)
    {
        _grounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        _grounded = false;
    }

    private void RotateLineRenderer()
    {
        _lineRenderer.transform.position = this.transform.position;
    }

    public void StopAnyMove()
    {
        _rigidbody.useGravity = false;
        _rigidbody.velocity = Vector3.zero;
        enabled = false;
    }
}
