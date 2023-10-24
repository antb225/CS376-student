using UnityEngine;

public class TargetBox : MonoBehaviour
{
    /// <summary>
    /// Targets that move past this point score automatically.
    /// </summary>
    public static float OffScreen;

    internal void Start() {
        OffScreen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width-100, 0, 0)).x;
    }

    internal void Update()
    {
        if (transform.position.x > OffScreen)
            Scored();
    }

    private void Scored()
    {
        // FILL ME IN
        if (gameObject.GetComponent<SpriteRenderer>().material.color != Color.green) {
            ScoreKeeper.AddToScore(gameObject.GetComponent<Rigidbody2D>().mass);
            gameObject.GetComponent<SpriteRenderer>().material.color = Color.green;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Scored();
        }
    }
}
