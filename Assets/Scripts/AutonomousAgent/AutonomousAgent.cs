using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutonomousAgent : Agent
{
    [SerializeField] Perception perception;
    [SerializeField] Perception flockPerception;
    [SerializeField] ObstaclePerception obstaclePerception;
    [SerializeField] Steering steering;
    [SerializeField] AutonomousAgentData agentData;

    void Update()
    {
        GameObject[] gameObjects = perception.GetGameObjects();

        //seek/flee
        if (gameObjects.Length != 0) {
            movement.ApplyForce(steering.Seek(this, gameObjects[0]) * agentData.seekWeight);
            movement.ApplyForce(steering.Flee(this, gameObjects[0]) * agentData.fleeWeight);
        }
        //Flocking
        gameObjects = flockPerception.GetGameObjects();
        if (gameObjects.Length != 0) {
            movement.ApplyForce(steering.Cohesion(this, gameObjects) * agentData.cohesionWeight);
            movement.ApplyForce(steering.Seperation(this, gameObjects, agentData.separationRadius) * agentData.separationWeight);
            movement.ApplyForce(steering.Alignment(this, gameObjects) * agentData.alignmentWeight);
        }

        // obstacle avoidance
        if (obstaclePerception.IsObstacleInFront())
		{
            Vector3 direction = obstaclePerception.GetOpenDirection();
            movement.ApplyForce(steering.CalculateSteering(this, direction) * agentData.obstacleWeight);
        }
        //wander
        if (movement.acceleration.sqrMagnitude <= movement.maxForce * 0.1f) {
            movement.ApplyForce(steering.Wander(this));
        }
        transform.position = Utilities.Wrap(transform.position, new Vector3(-20, -20, -20), new Vector3(20, 20, 20));
    }
}
