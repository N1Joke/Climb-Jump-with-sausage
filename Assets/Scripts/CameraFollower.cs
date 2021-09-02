using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Rigidbody _player;
    [SerializeField] private Vector3 _forwardDiraction;
    [SerializeField] private float _speed;
    [SerializeField] private float _angle;
    [SerializeField] private float _distance;
    [SerializeField] private float _maxVectorLength = 2;

    private Vector3 _nextPosition;

    private void Start()
    {
        float rotationY = Mathf.Rad2Deg * Mathf.Asin(_forwardDiraction.x / _forwardDiraction.magnitude);
        transform.rotation = Quaternion.Euler(_angle, rotationY, transform.rotation.eulerAngles.z);
    }

    private void Update()
    {
        _nextPosition = _player.position + Vector3.ClampMagnitude(_player.velocity, _maxVectorLength);
        _nextPosition += Vector3.up * Mathf.Cos(Mathf.Deg2Rad * _angle) * _distance;
        _nextPosition += -_forwardDiraction * Mathf.Sin(Mathf.Deg2Rad * _angle) * _distance;
        transform.position = Vector3.Lerp(transform.position, _nextPosition, _speed * Time.fixedDeltaTime);
    }
}
