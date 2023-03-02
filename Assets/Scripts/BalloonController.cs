using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BalloonController : MonoBehaviour
{

    private Rigidbody2D rb2d;
    private Transform transform;
    
    [SerializeField] private float upFireForce = 0.3f;
    [SerializeField] private float turnForce = 0.3f;
    [SerializeField] private float disableFireForceYHeight = 4.7f;
    [SerializeField] private float deathYHeight = -4.7f;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        var xForce = 0f;
        var yForce = 0f;
        
        if (Input.GetKey(KeyCode.Space) && transform.position.y <= disableFireForceYHeight)
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
        
        rb2d.AddForce(new Vector2(xForce, yForce), ForceMode2D.Impulse);

        if (transform.position.y <= deathYHeight)
        {
            Death();
        }
    }


    private void Death()
    {
        print("DEAD!");
        transform.position = new Vector3(0, 0, 0);
        rb2d.velocity = new Vector2(0, 0);
    }
}
