using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]


public class BulletBehaviour : MonoBehaviour
{
    public float speed;
    public Vector3 direction;
    public float range;
    public float radius;
    public bool debug;
    public bool isColliding;
    public Vector3 collisionNormal;


    public Vector3 size;
    public Vector3 max;
    public Vector3 min;
    public List<Contact> contacts;

    public BulletBehaviour cube;
    public Vector3 face;
    public float penetration;

    private MeshFilter meshFilter;
    public Bounds bounds;


    public BulletManager bulletManager;
    public CollisionManager Collision;
    public BulletBehaviour bullet;
    //public Contact(BulletBehaviour cube)
    //{
    //    this.cube = cube;
    //    face = Vector3.zero;
    //    penetration = 0.0f;
    //}

    // Start is called before the first frame update
    void Start()
    {
        isColliding = false;
        meshFilter = GetComponent<MeshFilter>();

        bounds = meshFilter.mesh.bounds;
        size = bounds.size;
        radius = Mathf.Max(transform.localScale.x, transform.localScale.y, transform.localScale.z) * 0.5f;
        bulletManager = FindObjectOfType<BulletManager>();
        //Collision = FindObjectOfType<CollisionManager>();
        bullet = FindObjectOfType<BulletBehaviour>();

    }

    // Update is called once per frame
    void Update()
    {
        max = Vector3.Scale(bounds.max, transform.localScale) + transform.position;
        min = Vector3.Scale(bounds.min, transform.localScale) + transform.position;
        _Move();
        _CheckBounds();
    }

    private void _Move()
    {
        if (isColliding== true)
        {
            Reflect();
        }

        transform.position += direction * speed * Time.deltaTime;
    }

    private void _CheckBounds()
    {
        if (Vector3.Distance(transform.position, Vector3.zero) > range)
        {
            bulletManager.ReturnBullet(this.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        if (debug)
        {
            Gizmos.color = Color.magenta;

            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
    public  void Reflect()
    {
        if ((collisionNormal == Vector3.forward) || (collisionNormal == Vector3.back))
        {
            direction = new Vector3(direction.x, direction.y, -direction.z);
        }
        else if ((collisionNormal == Vector3.right) || (collisionNormal == Vector3.left))
        {
            direction = new Vector3(-direction.x, direction.y, direction.z);
        }
        else if ((collisionNormal == Vector3.up) || (collisionNormal == Vector3.down))
        {
            direction = new Vector3(direction.x, -direction.y, direction.z);
        }
    }
}
