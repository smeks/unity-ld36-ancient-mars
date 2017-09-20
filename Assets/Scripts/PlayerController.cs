using UnityEngine;
using System.Collections;
using Spine.Unity;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

    [SpineEvent]
    public string footstepEventName = "footstep";
    public string jumpEventName = "jump";
    public bool nearStructure;
    public Vector2 cameraOffsetNearStructure;
    public float cameraOrthoNearStructure;

    public AudioClip walkStepSound;
    public AudioClip jumpSound;

    public GameObject rightArmIK;
    public GameObject leftArmIK;

    public GameObject bulletPrefab;

    private List<string> inventory;
    private Rigidbody2D rigidBody2d;
    private SkeletonAnimation skeletonAnimation;
    private Spine.AnimationState animationState;
    private AudioSource audioSource;
    private bool jumping = false;

    public void AddInventory(string item)
    {
        inventory.Add(item);
    }

    public bool GetInventory(string item)
    {
        return inventory.Contains(item);
    }

    // Use this for initialization
    void Start () {
        rigidBody2d = GetComponent<Rigidbody2D>();
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        audioSource = GetComponent<AudioSource>();
        animationState = skeletonAnimation.state;
        inventory = new List<string>();

        // This is how you subscribe via a declared method. The method needs the correct signature.
        skeletonAnimation.state.Event += HandleEvent;

        skeletonAnimation.state.Start += delegate (Spine.AnimationState state, int trackIndex) {
            // You can also use an anonymous delegate.
            //Debug.Log(string.Format("track {0} started a new animation.", trackIndex));
        };

        skeletonAnimation.state.End += delegate {
            // ... or choose to ignore its parameters.
            //Debug.Log("An animation ended!");
        };
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        // apply left/right forces
        if (Input.GetKey(KeyCode.D))
        {
            if (rigidBody2d.velocity.magnitude < 5.0f)
                rigidBody2d.AddForce(new Vector2(700.0f * Time.fixedDeltaTime, 0.0f));
            skeletonAnimation.skeleton.flipX = false;
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (rigidBody2d.velocity.magnitude < 5.0f)
                rigidBody2d.AddForce(new Vector2(-700.0f * Time.fixedDeltaTime, 0.0f));
            skeletonAnimation.skeleton.flipX = true;
        }

        // start jump animation on space key
        if (Input.GetKeyDown(KeyCode.Space) && !jumping)
        {
            skeletonAnimation.state.SetAnimation(0, "jump", false);
            jumping = true;
        }

        // if not jumping, perform walk/idle
        if (!jumping)
        {
            if (rigidBody2d.velocity.magnitude > 0.2)
            {
                skeletonAnimation.AnimationName = "walk";
                skeletonAnimation.loop = true;
            }
            else
            {
                skeletonAnimation.AnimationName = "idle";
                skeletonAnimation.loop = true;
            }
        }

        var mousePos = Input.mousePosition;
        var mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);

        var mdelta = mouseWorldPos - transform.position;
        var newIKPos = mdelta.normalized * 1.0f;
        var newLeftIKPos = mdelta.normalized * 3.0f;
        rightArmIK.transform.position = transform.position + newIKPos;

        if (Input.GetMouseButton(0))
        {
            var newbullet = Instantiate(bulletPrefab, rightArmIK.transform.position + new Vector3(5.0f,0.0f,0.0f), rightArmIK.transform.rotation) as GameObject;
            newbullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(1000,0));
        }

    }

    void HandleEvent(Spine.AnimationState state, int trackIndex, Spine.Event e)
    {
        // Play some sound if the event named "footstep" fired.
        if (e.Data.Name == footstepEventName)
        {
            audioSource.clip = walkStepSound;
            audioSource.Play();
        }

        if(e.Data.Name == jumpEventName)
        {
            audioSource.clip = jumpSound;
            audioSource.Play();
            rigidBody2d.AddForce(new Vector2(0.0f, 750.0f));
            jumping = true;
        }
    }


    void OnCollisionEnter2D(Collision2D collide)
    {
        if (collide.gameObject.tag == "Ground" && jumping)
        {
            jumping = false;
        }
    }
    void OnCollisionStay2D(Collision2D collide)
    {
        
 
    }


}
