using UnityEngine;

public class BasicTargetDetection : MonoBehaviour
{
    public bool AwareOfPlayer { get; private set; }

    public Vector2 DirectionToPlayer { get; private set; }

    [SerializeField]
    private float _playerAwarenessDistance;

    [SerializeField]
    private Transform _player;

    private GameObject _playerGameObject;

    private void Awake()
    {
        if (_playerGameObject == null)
        {
            _playerGameObject = GameObject.FindWithTag("Player");
            _player = _playerGameObject.transform;
            PlayerController playerController = _playerGameObject.GetComponent<PlayerController>();
            if (playerController == null)
            {
                enabled = false;
                return;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_playerGameObject == null)
        {
            enabled = false;
            return;
        }

        Vector2 enemyToPlayerVector = _playerGameObject.transform.position - transform.position;
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
