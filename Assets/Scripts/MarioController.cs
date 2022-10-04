using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioController : MonoBehaviour
{
	public float Gravity = -9.81f;
	[Range(0, 0.4f)] public float Acceleration = 0.01f;
	[Range(0, 0.4f)] public float AirAcceleration = 0.01f;
	[Range(0, 0.4f)] public float JumpyAirAcceleration = 0.01f;
	public float MaximumVelocity = 3f;
	public float JumpHeight = 1;
	public Animator Anim;
	public float radius;
	public BoxCollider2D Col;
	public Vector3 PivPoint;
	public Rigidbody2D RB2D;

	public bool isGrounded;
	public bool isDead;
	public bool isJumping;

	public AudioSource JumpFX;
	public AudioSource StompFX;

	public NPC LatestNPC = null;
	public IInteractable Latestinteractable;

	public List<string> InteractableTags = new List<string>();
	public bool CanPlayerMove;

    public DialogueUI DialogueUI;

	public GameObject Shadow;

	public float ShadowDistance;

	public AudioSource L;
	public AudioSource R;

    void Start ()
	{
        DialogueUI = FindObjectOfType<DialogueUI>();
        isDead = false;
		isJumping = false;
		Anim = GetComponentInChildren<Animator> ();
		RB2D = GetComponent<Rigidbody2D>();
		Shadow = GameObject.FindWithTag("KekShadow");
		//JumpFX = GameObject.Find ("JumpFX").GetComponent<AudioSource>();
		//StompFX = GameObject.Find ("StompFX").GetComponent<AudioSource>();
		PivPoint = gameObject.transform.position + new Vector3(0, -0.4f);
	}

	public void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("NPC"))
		{
			Latestinteractable = null;
            LatestNPC = collision.gameObject.GetComponent<NPC>();
		}
		
		if (InteractableTags.Contains(collision.gameObject.tag))
		{
			LatestNPC = null;
			Latestinteractable = (IInteractable)collision.gameObject.GetComponent(typeof(IInteractable));
        }
    }

	public void OnTriggerExit2D(Collider2D collision)
	{
        if (collision.gameObject.CompareTag("NPC"))
			LatestNPC = null;

        if (InteractableTags.Contains(collision.gameObject.tag))
			Latestinteractable = null;
    }

	void Update()
	{
		Shadow.transform.position = new Vector3(Shadow.transform.position.x, Shadow.transform.position.y, ShadowDistance);

        if (CanPlayerMove)
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
				if (DialogueUI.CurrentlyActive)
				{
                    DialogueUI.RollText();
					return;
                }
				if (LatestNPC != null)
				{
					LatestNPC.Interact();
					return;
				}
				if (Latestinteractable != null)
				{
					Latestinteractable.Interact();
					return;
				}
			}
		}
	}

	public void EnableMovement()
	{
		CanPlayerMove = true;
	}

	public void DisableMovement()
	{
		CanPlayerMove = false;
	}

	void FixedUpdate ()
	{
		PivPoint = gameObject.transform.position + new Vector3(0, -0.4f);
		if (!CanPlayerMove || DialogueUI.CurrentlyActive)
		{
			Anim.SetBool("isRunning", false);
			return;
		}

		if (Input.GetAxis ("Horizontal") > 0) // right
		{
			transform.rotation = Quaternion.Euler (0, 180, 0);
			Anim.SetBool("isRunning", true);
			if (Mathf.Abs(RB2D.velocity.x) < MaximumVelocity)
			{
				if (isJumping)
					RB2D.velocity = new Vector2(RB2D.velocity.x + JumpyAirAcceleration, RB2D.velocity.y);
				else
					RB2D.velocity = new Vector2(RB2D.velocity.x + Acceleration, RB2D.velocity.y);
			}
			else if (Mathf.Abs(RB2D.velocity.x) >= MaximumVelocity)
			{
				RB2D.velocity = new Vector2(MaximumVelocity, RB2D.velocity.y);
			}
		}
		else if (Input.GetAxis("Horizontal") < 0) // left
		{
			transform.rotation = Quaternion.Euler (0, 0, 0);
			Anim.SetBool("isRunning", true);
			if (Mathf.Abs(RB2D.velocity.x) < MaximumVelocity)
				if (isJumping)
					RB2D.velocity = new Vector2(RB2D.velocity.x - JumpyAirAcceleration, RB2D.velocity.y);
				else
					RB2D.velocity = new Vector2(RB2D.velocity.x - Acceleration, RB2D.velocity.y);
			else if (Mathf.Abs(RB2D.velocity.x) >= MaximumVelocity)
				RB2D.velocity = new Vector2(-MaximumVelocity, RB2D.velocity.y);
		}
		else if (Input.GetAxis("Horizontal") == 0) // stop
		{
			Anim.SetBool("isRunning", false);
			if (RB2D.velocity.x > 0)
			{
				if (!isJumping)
				{
					RB2D.velocity = new Vector2 (RB2D.velocity.x - Acceleration, RB2D.velocity.y);
					if (RB2D.velocity.x <= 0.01f)
					{
						RB2D.velocity = new Vector2(0, RB2D.velocity.y);
					}
				}
				else
				{
					RB2D.velocity = new Vector2 (RB2D.velocity.x - AirAcceleration, RB2D.velocity.y);
					if (RB2D.velocity.x <= 0.01f)
					{
						RB2D.velocity = new Vector2(0, RB2D.velocity.y);
					}
				}
			}
			else if(RB2D.velocity.x < 0)
			{
				if (!isJumping)
				{
					RB2D.velocity = new Vector2 (RB2D.velocity.x + Acceleration, RB2D.velocity.y);
					if (RB2D.velocity.x >= 0.01f)
					{
						RB2D.velocity = new Vector2(0, RB2D.velocity.y);
					}
				}
				else
				{
					RB2D.velocity = new Vector2 (RB2D.velocity.x + AirAcceleration, RB2D.velocity.y);
					if (RB2D.velocity.x >= 0.01f)
					{
						RB2D.velocity = new Vector2(0, RB2D.velocity.y);
					}
				}
			}
		}
	}

	public void LeftStep()
	{
		L.Play();
	}

	public void RightStep()
	{
		R.Play();
	}
}
