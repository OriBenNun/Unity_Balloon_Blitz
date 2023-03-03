using UnityEngine;

public class HorizontalMover : MonoBehaviour, IObstacle
{
    public bool isDirectionRight { get; set; }

    private Transform _transform;

    [SerializeField] private float moveSpeed = 0.1f;

    private Vector3 _prevPosition;

    private void Start()
    {
        _transform = GetComponent<Transform>();
        _prevPosition = _transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var newPosition = _prevPosition;
        if (isDirectionRight)
        {
            newPosition.x += moveSpeed * Time.deltaTime;
        }
        else
        {
            newPosition.x -= moveSpeed * Time.deltaTime;   
        }

        _transform.position = newPosition;
        _prevPosition = newPosition;
    }

}
