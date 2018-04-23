using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {

    [SerializeField]
    private float speed;
    protected Vector2 direction;

    protected Animator myAnimator;

    private Rigidbody2D myRigidBody;

    protected bool IsAttacking = false;

    protected Coroutine attackRoutine;

    public bool IsMoving
    { 
        get
    {
        return direction.x != 0 || direction.y != 0;
    }
}


    // Use this for initialization
   protected virtual void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected virtual void Update() {
        handleLayers();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        /*
        Not optimal on all devices
        transform.Translate(direction * speed * Time.deltaTime);
        */
        myRigidBody.velocity = direction.normalized * speed;
    }

    public void handleLayers()
    {
        if (IsMoving)
        {
            ActivateLayer("WalkLayer");

            myAnimator.SetFloat("x", direction.x);
            myAnimator.SetFloat("y", direction.y);

            stopAttack();
        }
        else if (IsAttacking)
        {
            ActivateLayer("AttackLayer");
        }
        else
        {
            ActivateLayer("IdleLayer");
        }

    }

    public void ActivateLayer(string layerName)
    {
        for (int i = 0; i < myAnimator.layerCount; i++)
        {
            myAnimator.SetLayerWeight(i, 0);
        }
        myAnimator.SetLayerWeight(myAnimator.GetLayerIndex(layerName), 1);
    }

    public void stopAttack()
    {
        if (attackRoutine != null)
        {
            StopCoroutine(attackRoutine);
            IsAttacking = false;
            myAnimator.SetBool("attack", IsAttacking);
        }
    }
}
