using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BalloonParticlesEmitter : MonoBehaviour
{
    [SerializeField] private ParticleSystem leftParticleSystem;
    [SerializeField] private ParticleSystem rightParticleSystem;

    [SerializeField] private int horizontalMultiplierMin = 100;
    [SerializeField] private int horizontalMultiplierMax = 500;
    
    [SerializeField] private int verticalMultiplierMin = 100;
    [SerializeField] private int verticalMultiplierMax = 500;

    [SerializeField] private int defaultCount = 10;

    public void EmitParticles(float horizontal, float vertical)
    {
        var horizontalMultiplier = Random.Range(horizontalMultiplierMin, horizontalMultiplierMax);
        var horizontalCount = (int)Math.Abs(horizontal * horizontalMultiplier);

        print(horizontalCount);
        switch (horizontal)
        {
            case > 0:
                // Moving right, emit particles from the left side
                leftParticleSystem.Emit(horizontalCount);
                break;
            case < 0:
                // Moving left, emit particles from the right side
                rightParticleSystem.Emit(horizontalCount);
                break;
        }
        
        var verticalMultiplier = Random.Range(verticalMultiplierMin, verticalMultiplierMax);
        var verticalCount = (int)Math.Abs(vertical * verticalMultiplier);

        if (vertical > 0)
        {
            // Moving up, emit particles from both sides
            leftParticleSystem.Emit(verticalCount);
            rightParticleSystem.Emit(verticalCount);
        }
        else
        {
            leftParticleSystem.Emit(defaultCount); 
            rightParticleSystem.Emit(defaultCount);
        }
    }
}
