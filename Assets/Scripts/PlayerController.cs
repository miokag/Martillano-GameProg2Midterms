using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject bulletPrefab; 
    private Color playerColor;
    private int colorIndex = 0;
    public float shootInterval = 4f;
    public float speedDuration = 1f;

    private Color[] primaryColors = { Color.red, Color.blue, Color.yellow };

    public Transform pointer; 

    private void Start()
    {
        playerColor = primaryColors[0];
        GetComponent<Renderer>().material.color = playerColor;
        SetChildColors();

        StartCoroutine(ShootCoroutine());
    }

    private void Update()
    {
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ChangeColor();
        }
    }

    private void ChangeColor()
    {
        colorIndex = (colorIndex + 1) % primaryColors.Length;
        playerColor = primaryColors[colorIndex];
        GetComponent<Renderer>().material.color = playerColor;
        SetChildColors();
    }

    private void SetChildColors()
    {
        foreach (Renderer childRenderer in GetComponentsInChildren<Renderer>())
        {
            childRenderer.material.color = playerColor;
        }
    }

    public void Shoot()
    {
        Vector3 spawnPosition = pointer.position; 
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
        bullet.GetComponent<Renderer>().material.color = playerColor;

        Vector3 direction = (pointer.position - transform.position).normalized;
        bullet.GetComponent<BulletBehavior>().SetDirection(direction);
    }


    private IEnumerator ShootCoroutine()
    {
        while (true)
        {
            Shoot(); 
            yield return new WaitForSeconds(shootInterval); 
        }
    }

    public void PointTowardsEnemy(Transform enemyTransform)
    {
        Vector3 direction = (enemyTransform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
