using UnityEngine;
using UnityEngine.PlayerLoop;

/// <summary>
/// Control the player on screen
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    /// <summary>
    /// Prefab for the orbs we will shoot
    /// </summary>
    public GameObject OrbPrefab;

    public Rigidbody2D PlayerRigidbody;

    /// <summary>
    /// How fast our engines can accelerate us
    /// </summary>
    public float EnginePower = 1;
    
    /// <summary>
    /// How fast we turn in place
    /// </summary>
    public float RotateSpeed = 1;

    /// <summary>
    /// How fast we should shoot our orbs
    /// </summary>
    public float OrbVelocity = 10;

    public int MaxOrbs = 10;
    int orbsShot = 0;
    int FixedUpdateCalls = 0;

    private void Start()
    {
        PlayerRigidbody = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Handle moving and firing.
    /// Called by Uniity every 1/50th of a second, regardless of the graphics card's frame rate
    /// </summary>
    // ReSharper disable once UnusedMember.Local
    void FixedUpdate()
    {
        Manoeuvre();
        MaybeFire();
        if (FixedUpdateCalls < 50)
        {
            FixedUpdateCalls++;
        }
        else
        {
            FixedUpdateCalls = 0;
            orbsShot = 0;
        }
    }

    /// <summary>
    /// Fire if the player is pushing the button for the Fire axis
    /// Unlike the Enemies, the player has no cooldown, so they shoot a whole blob of orbs
    /// </summary>
    void MaybeFire()
    {
        // TODO
        if (Input.GetAxis("Fire") != 0)
        {
            if (orbsShot < MaxOrbs)
            {
                FireOrb();
                orbsShot++;
            }
        }
        
    }

    /// <summary>
    /// Fire one orb.  The orb should be placed one unit "in front" of the player.
    /// transform.right will give us a vector in the direction the player is facing.
    /// It should move in the same direction (transform.right), but at speed OrbVelocity.
    /// </summary>
    private void FireOrb()
    {
        // TODO
        GameObject newOrb = Instantiate(OrbPrefab, transform.position + transform.right, Quaternion.identity);
        Rigidbody2D newOrbRB = newOrb.GetComponent<Rigidbody2D>();
        newOrbRB.velocity = transform.right * OrbVelocity;
    }

    /// <summary>
    /// Accelerate and rotate as directed by the player
    /// Apply a force in the direction (Horizontal, Vertical) with magnitude EnginePower
    /// Note that this is in *world* coordinates, so the direction of our thrust doesn't change as we rotate
    /// Set our angularVelocity to the Rotate axis time RotateSpeed
    /// </summary>
    void Manoeuvre()
    {
        // TODO
        PlayerRigidbody.angularVelocity = Input.GetAxis("Rotate") * RotateSpeed;
        PlayerRigidbody.AddForce(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * EnginePower);
    }

    /// <summary>
    /// If this is called, we got knocked off screen.  Deduct a point!
    /// </summary>
    // ReSharper disable once UnusedMember.Local
    void OnBecameInvisible()
    {
        ScoreKeeper.ScorePoints(-1);
    }
}
