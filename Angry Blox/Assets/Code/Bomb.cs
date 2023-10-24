using UnityEngine;

public class Bomb : MonoBehaviour {
    public float ThresholdImpulse = 5;
    public GameObject ExplosionPrefab;

    internal void Destruct()
    {
        Destroy(gameObject);
    }

    internal void Boom()
    {
        gameObject.GetComponent<PointEffector2D>().enabled = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        Instantiate(ExplosionPrefab, transform.position, Quaternion.identity, transform.parent);
        Invoke("Destruct", 0.1f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        bool willBoom = false;
        foreach (ContactPoint2D contact in collision.contacts) { 
            if (contact.normalImpulse > ThresholdImpulse)
            {
                willBoom = true;
            }
        }
        if (willBoom)
        {
            Boom();
        }
    }
}
