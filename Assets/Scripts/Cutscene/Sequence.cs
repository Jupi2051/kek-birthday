using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Sequence : MonoBehaviour
{

    public GameObject cam;
    public Animator CamAnimator;
    public Rigidbody2D Player;
    public float ForceStrength = 1;
    public GameObject DynamicBalloons;
    public Rigidbody2D[] DynamicBalloonsPhysics;
    public float RadiusOfBomb = 5f;
    public float ExplosionPower = 3;
    public Image darkimage;
    public bool FadeOut;
    public float FadeProgress = 0;
    public float FadeSpeed = 0.01f;

    public AudioSource Jump;
    public AudioSource Window;
    public AudioSource Balloon;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindWithTag("MainCamera");
        CamAnimator = cam.GetComponent<Animator>();
        Player = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        Player.simulated = false;
        DynamicBalloons = GameObject.Find("Dynamic Balloons");
        DynamicBalloonsPhysics = DynamicBalloons.GetComponentsInChildren<Rigidbody2D>();
        darkimage = GameObject.Find("Dark").GetComponent<Image>();
        StartCoroutine(nameof(StartSequence));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartSequence()
    {
        yield return new WaitForSeconds(1);
        Window.Play();
        yield return new WaitForSeconds(1.2f);
        Jump.Play();
        yield return new WaitForSeconds(0.3f);
        Player.simulated = true;
        Player.AddForce((Vector2.right + Vector2.up) * ForceStrength, ForceMode2D.Impulse);
        CamAnimator.SetTrigger("Move");
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Vector3 explosionPos = transform.position;
            Vector2 explosionDir = new Vector2(transform.position.x, transform.position.y);
            foreach (Rigidbody2D rb in DynamicBalloonsPhysics)
                rb.simulated = true;

            Balloon.Play();

            foreach (Rigidbody2D rb in DynamicBalloonsPhysics)
            
                rb.AddExplosionForce(ExplosionPower, explosionDir, RadiusOfBomb);

            StartCoroutine(nameof(StartFade));
        }
    }

    IEnumerator StartFade()
    {
        yield return new WaitForSeconds(3);
        FadeOut = true;
    }

    void FixedUpdate()
    {
        if (FadeOut)
        {
            darkimage.color = new Color(0, 0, 0, FadeProgress);
            FadeProgress += FadeSpeed;
            if (FadeProgress >= 1)
            {
                FadeOut = false;
                SceneManager.LoadScene(2);
            }
        }
    }
}

public static class Rigidbody2DExt
{

    public static void AddExplosionForce(this Rigidbody2D rb, float explosionForce, Vector2 explosionPosition, float explosionRadius, float upwardsModifier = 0.0F, ForceMode2D mode = ForceMode2D.Force)
    {
        var explosionDir = rb.position - explosionPosition;
        var explosionDistance = explosionDir.magnitude;

        // Normalize without computing magnitude again
        if (upwardsModifier == 0)
            explosionDir /= explosionDistance;
        else
        {
            // From Rigidbody.AddExplosionForce doc:
            // If you pass a non-zero value for the upwardsModifier parameter, the direction
            // will be modified by subtracting that value from the Y component of the centre point.
            explosionDir.y += upwardsModifier;
            explosionDir.Normalize();
        }

        rb.AddForce(Mathf.Lerp(0, explosionForce, (1 - explosionDistance)) * explosionDir, mode);
    }
}