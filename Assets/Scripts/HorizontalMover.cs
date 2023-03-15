using UnityEngine;

public class HorizontalMover : MonoBehaviour, IObstacle
{
    public bool IsDirectionRight { get; set; }

    private Transform _transform;

    [SerializeField] private float moveSpeedMin = 1.3f;
    [SerializeField] private float moveSpeedMax = 2f;

    private Vector3 _prevPosition;
    private float _speed;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _prevPosition = _transform.position;
        _speed = Random.Range(moveSpeedMin, moveSpeedMax);
    }

    // Update is called once per frame
    void Update()
    {
        var newPosition = _prevPosition;
        if (IsDirectionRight)
        {
            newPosition.x += _speed * Time.deltaTime;
        }
        else
        {
            newPosition.x -= _speed * Time.deltaTime;   
        }

        _transform.position = newPosition;
        _prevPosition = newPosition;
    }

    public void Die(float waitSeconds)
    {
        Destroy(gameObject, waitSeconds);
    }
}
