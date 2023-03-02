using UnityEngine;

public class BalloonController : MonoBehaviour
{

    private Rigidbody2D _rb2d;
    private Transform _transform;
    
    [SerializeField] private float upFireForce = 0.3f;
    [SerializeField] private float turnForce = 0.3f;
    [SerializeField] private float disableFireForceYHeight = 4.7f;
    [SerializeField] private float deathYHeight = -4.7f;

    private void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        ApplyMovementByInput();
    }

    private void Update()
    {
        if (_transform.position.y <= deathYHeight)
        {
            Death();
        }
    }

    private void ApplyMovementByInput()
    {
        var xForce = 0f;
        var yForce = 0f;

        if (Input.GetKey(KeyCode.Space) && _transform.position.y <= disableFireForceYHeight)
        {
            yForce = upFireForce;
        }

        if (Input.GetKey(KeyCode.A))
        {
            xForce = -turnForce;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            xForce = turnForce;
        }

        _rb2d.AddForce(new Vector2(xForce, yForce), ForceMode2D.Impulse);
    }
    
    private void Death()
    {
        print("DEAD!");
        _transform.position = new Vector3(0, 0, 0);
        _rb2d.velocity = new Vector2(0, 0);
    }
}
