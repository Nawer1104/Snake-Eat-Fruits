using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public float speed = 20f;

    private Rigidbody2D rb;

    private bool canMove = false;

    public GameObject vfxDestroy;

    public GameObject vfxEat;

    private Vector3 startPos;

    public Vector2 direction;

    private int Id;

    private void Awake()
    {
        startPos = transform.position;

        Id = GetInstanceID();

        rb = GetComponent<Rigidbody2D>();
    }

    private void OnMouseDown()
    {
        canMove = true;
    }

    private void Update()
    {
        if (!canMove) return;

        rb.MovePosition(rb.position + direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            GameObject vfxExplosion = Instantiate(vfxDestroy, transform.position, Quaternion.identity);
            Destroy(vfxExplosion, 0.75f);

            GameManager.Instance.levels[GameManager.Instance.GetCurrentIndex()].gameObjects.Remove(gameObject);

            gameObject.SetActive(false);

            GameManager.Instance.CheckLevelUp();
        }
        else if (collision.tag == "Fruit")
        {
            GameObject vfxStuned = Instantiate(vfxEat, transform.position, Quaternion.identity);
            Destroy(vfxStuned, 0.75f);

            collision.gameObject.SetActive(false);

        }
    }
}