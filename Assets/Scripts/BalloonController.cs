using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class BalloonController : MonoBehaviour
{


    [Header("References")]
    [SerializeField] private TemperatureBar temperatureBar;
    [SerializeField] private BalloonParticlesEmitter particlesEmitter;
    [FormerlySerializedAs("enemiesSpawner")] [SerializeField] private ObstaclesSpawner obstaclesSpawner;
    
    [SerializeField] private float fireTapBoostForce = 1.5f;
    [SerializeField] private float upFireForce = 0.3f;
    [SerializeField] private float turnForce = 0.3f;
    [SerializeField] private float deathYHeight = 10f;
    [SerializeField] private float upForceTempChangeMultiplier = 3f;
    [SerializeField] private float freeFallTempChange = 1f;
    [SerializeField] private float turnForceTempChange = 0.3f;
    [SerializeField] private float turnDragWhenIdle = 0.3f;
    [SerializeField] private float maxVelocityMagnitude = 5f;

    private Rigidbody2D _rb2d;
    private Transform _transform;

    private Vector2 _forceToApplyByInput;
    private bool _shouldApplyBoost;
    private PlayerInput _playerInput;

    private float _bestHeightYValue;
    
    private void OnEnable()
    {
        TemperatureBar.TemperatureReachedMax += Death;
    }

    private void OnDisable()
    {
        TemperatureBar.TemperatureReachedMax -= Death;
    }
    
    private void Awake()
    {                                                               
        _rb2d = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();
        _playerInput = GetComponent<PlayerInput>();
    }
                                                
    private void FixedUpdate()
    {
        AddMovementForceAndUpdateTemperature(_forceToApplyByInput);
    }

    private void Update()
    {
        RecordInput();
        
        if (_bestHeightYValue - _transform.position.y >= deathYHeight)
        {
            Death();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        var obstacle = other.GetComponent<IObstacle>();
        if (obstacle == null) return;
        
        Death();
    }


    private void RecordInput()
    {
        
        var xForce = 0f;
        var yForce = 0f;
        
        // Read input values from the Input System
        var inputActions = _playerInput.actions;
        
        if (inputActions["Fire_Up"].triggered)
        {
            _shouldApplyBoost = true;
        }

        if (inputActions["Fire_Up"].inProgress)
        {
            yForce += upFireForce;
        }
        
        if (inputActions["Move_Left"].inProgress)
        {
            xForce = -turnForce;
        }
        else if (inputActions["Move_Right"].inProgress)
        {
            xForce = turnForce;
        }
        
        _forceToApplyByInput = new Vector2(xForce, yForce);
        
        particlesEmitter.EmitParticles(xForce, yForce);
    }

    private void Death()
    {
        print("DEAD!");
        _transform.position = new Vector3(0, 0, 0);
        _rb2d.velocity = new Vector2(0, 0);
        _forceToApplyByInput = new Vector2(0, 0);
        _shouldApplyBoost = false;
        _bestHeightYValue = 4f;
        
        temperatureBar.ResetTemperature();
        
        obstaclesSpawner.DestroyAllSpawnedObstacles();
    }

    private void AddMovementForceAndUpdateTemperature(Vector2 force, ForceMode2D forceMode2D = ForceMode2D.Impulse)
    {
        
        if (_transform.position.y > _bestHeightYValue)
        {
            _bestHeightYValue = _transform.position.y;
        }

        var updatedForce = force;

        if (_shouldApplyBoost)
        {
            updatedForce.y += fireTapBoostForce;
            
            _shouldApplyBoost = false; // Reset the state
        }
        
        _rb2d.AddForce(updatedForce, forceMode2D);

        // Limit the velocity magnitude
        if (_rb2d.velocity.magnitude > maxVelocityMagnitude)
        {
            _rb2d.velocity = _rb2d.velocity.normalized * maxVelocityMagnitude;
        }
        
        // Adding drag to the horizontal movement for easier control
        var velocity = _rb2d.velocity;
        
        if (updatedForce.x == 0 && velocity.x != 0)
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

        UpdateTemperature(updatedForce);
    }

    private void UpdateTemperature(Vector2 force)
    {
        switch (force.y)
        {
            // float multiplyFactor = 1;
            case > 0:
                temperatureBar.ChangeCurrentTemperature(force.y * upForceTempChangeMultiplier);
                break;
            case 0:
                temperatureBar.ChangeCurrentTemperature(-freeFallTempChange);
                break;
        }

        if (force.x != 0)
        {
            temperatureBar.ChangeCurrentTemperature(turnForceTempChange);
        }
    }
}
