using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Transform _playerTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerTransform = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = (Vector2)_playerTransform.position;
    }
}
