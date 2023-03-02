using UnityEngine;

public class BalloonController : MonoBehaviour
{

    private Rigidbody2D _rb2d;
    private Transform _transform;

    [SerializeField] private TemperatureBar temperatureBar;
    
    [SerializeField] private float upFireForce = 0.3f;
    [SerializeField] private float turnForce = 0.3f;
    [SerializeField] private float disableFireForceYHeight = 4.7f;
    [SerializeField] private float deathYHeight = -4.7f;
    [SerializeField] private float upForceTempChangeMultiplier = 3f;
    [SerializeField] private float freeFallTempChange = 1f;
    [SerializeField] private float turnForceTempChange = 0.3f;
    [SerializeField] private float turnDragWhenIdle = 0.3f;

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

        AddMovementForceAndUpdateTemperature(new Vector2(xForce, yForce));
    }
    
    private void Death()
    {
        print("DEAD!");
        _transform.position = new Vector3(0, 0, 0);
        _rb2d.velocity = new Vector2(0, 0);
    }

    private void AddMovementForceAndUpdateTemperature(Vector2 force, ForceMode2D forceMode2D = ForceMode2D.Impulse)
    {

        _rb2d.AddForce(force, forceMode2D);
        
        // Adding drag to the horizontal movement for easier control
        Vector2 velocity = _rb2d.velocity;
        
        if (force.x == 0 && velocity.x != 0)
        {
            if (velocity.x > 0)
            {
                velocity.x -= turnDragWhenIdle;
            }
            else
            {
                velocity.x += turnDragWhenIdle;
            }
            
            _rb2d.velocity = velocity;
        }

        UpdateTemperature(force);
    }

    private void UpdateTemperature(Vector2 force)
    {
        // float multiplyFactor = 1;
        if (force.y > 0)
        {
            print(force.y);
            temperatureBar.ChangeCurrentTemperature(force.y * upForceTempChangeMultiplier);
        }
        else if (force.y == 0)
        {
            temperatureBar.ChangeCurrentTemperature(-freeFallTempChange);
        }

        if (force.x != 0)
        {
            temperatureBar.ChangeCurrentTemperature(turnForceTempChange);
        }
    }
}
