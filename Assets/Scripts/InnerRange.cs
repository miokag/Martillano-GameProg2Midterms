using System.Collections.Generic;
using UnityEngine;

public class InnerRange : MonoBehaviour
{
    private List<GameObject> enemiesInArea = new List<GameObject>();
    private PlayerController playerController;
    private GameObject currentTargetEnemy;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerController = player.GetComponent<PlayerController>();
            Debug.Log("PlayerController found: " + playerController);
        }
        else
        {
            Debug.LogError("Player not found. Make sure the player GameObject is tagged as 'Player'.");
        }
    }

    private void Update()
    {
        // Update the current target enemy
        UpdateTargetEnemy();

        if (currentTargetEnemy != null && playerController != null)
        {
            playerController.PointTowardsEnemy(currentTargetEnemy.transform);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInArea.Add(other.gameObject);
            Debug.Log($"Enemy entered the spawn area: {other.gameObject.name}");

            // Immediately check if we need to set a new target
            UpdateTargetEnemy();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInArea.Remove(other.gameObject);
            Debug.Log($"Enemy exited the spawn area: {other.gameObject.name}");

            // Update the target enemy
            UpdateTargetEnemy();
        }
    }

    private void UpdateTargetEnemy()
    {
        if (enemiesInArea.Count == 0)
        {
            currentTargetEnemy = null; // No enemies in area
            return;
        }

        float closestDistance = float.MaxValue;
        GameObject closestEnemy = null;

        foreach (GameObject enemy in enemiesInArea)
        {
            // Check if the enemy is still valid
            if (enemy == null)
                continue;

            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        currentTargetEnemy = closestEnemy; // Update the current target

        // Only point towards the new target if it's valid
        if (currentTargetEnemy != null)
        {
            playerController.PointTowardsEnemy(currentTargetEnemy.transform); // Point towards the new closest enemy
        }
    }
}
