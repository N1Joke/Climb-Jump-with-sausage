using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Rigidbody _rigidbody;

    private Vector3 _tapping;
    private Vector3 _release;
    
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Update the Text on the screen depending on current position of the touch each frame
            Debug.Log("Touch Position : " + touch.position);
        }
        else
        {
            Debug.Log("No touch contacts");
        }
    }
}
