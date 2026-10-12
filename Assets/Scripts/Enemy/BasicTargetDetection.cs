using UnityEngine;

public class BasicTargetDetection : MonoBehaviour
{
    public bool AwareOfPlayer { get; private set; }

    public Vector2 DirectionToPlayer { get; private set; }

    [SerializeField]
    private float _playerAwarenessDistance;

    [SerializeField]
    private Transform _player;

    private void Awake()
    {
        if (_player == null)
        {
            PlayerController playerController = FindAnyObjectByType<PlayerController>();
            if (playerController == null)
            {
                enabled = false;
                return;
            }

            _player = playerController.transform;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_player == null)
        {
            enabled = false;
            return;
        }

        Vector2 enemyToPlayerVector = _player.position - transform.position;
        DirectionToPlayer = enemyToPlayerVector.normalized;

        if (enemyToPlayerVector.magnitude <= _playerAwarenessDistance)
        {
            AwareOfPlayer = true;
        }
        else
        {
            AwareOfPlayer = false;
        }
    }
}
