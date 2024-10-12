using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float speed = 20f;
    private Vector3 direction;
    public float lifetime = 5f;

    private void Start()
    {
        StartCoroutine(DestroyAfterTime(lifetime));
    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    public void SetDirection(Vector3 dir)
    {
        direction = dir; 
        direction.Normalize(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Color enemyColor = other.GetComponent<Renderer>().material.color;
            Color bulletColor = GetComponent<Renderer>().material.color;
            Debug.Log("Bullet Color:" + bulletColor);
            Debug.Log("Enemy Color:" + enemyColor);

            if (bulletColor == enemyColor)
            {
                Destroy(other.gameObject); 
            }

            Destroy(gameObject); 
        }
    }

    private IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject); 
    }
}
