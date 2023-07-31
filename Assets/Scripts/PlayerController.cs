using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer sprite;
    private float speed = 5f;
    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector3 pos = new Vector3(x, y);
        transform.position += pos.normalized * speed * Time.deltaTime;
    }
    private void OnEnable()
    {
        float r = Random.Range(0f, 1f);
        float g = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);
        sprite.color = new Color(r, g, b);
    }
}
