using UnityEngine;

public class BasicEnemyMovementController : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _rotationSpeed;

    [SerializeField]
    private float _knockbackDuration = 0.25f;

    public Rigidbody2D _rigidbody;
    private BasicTargetDetection _playerAwarenessController;
    private Vector2 _targetDirection;
    private float _knockbackTimeRemaining;

    private void Awake()
    {
        _playerAwarenessController = GetComponent<BasicTargetDetection>();
    }

    private void FixedUpdate()
    {
        if (_knockbackTimeRemaining > 0f)
        {
            _knockbackTimeRemaining -= Time.fixedDeltaTime;
            return;
        }

        UpdateTargetDirection();
        RotateTowardsTarget();
        SetVelocity();
    }

    public void ApplyKnockback(Vector2 force)
    {
        _rigidbody.AddForce(force, ForceMode2D.Impulse);
        _knockbackTimeRemaining = _knockbackDuration;
    }

    private void UpdateTargetDirection()
    {
        if (true)//_playerAwarenessController.AwareOfPlayer)
        {
            _targetDirection = _playerAwarenessController.DirectionToPlayer;
        }
        else
        {
            //_targetDirection = Vector2.zero;
        }
    }

    private void RotateTowardsTarget()
    {
        if (_targetDirection == Vector2.zero)
        {
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _targetDirection);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

        _rigidbody.SetRotation(rotation);
    }

    private void SetVelocity()
    {
        if (_targetDirection == Vector2.zero)
        {
            _rigidbody.linearVelocity = Vector2.zero;
        }
        else
        {
            _rigidbody.linearVelocity = transform.up * _speed;
        }
    }
}
