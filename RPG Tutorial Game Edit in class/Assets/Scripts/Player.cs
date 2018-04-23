using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {

    [SerializeField]
    private Stat health;

    [SerializeField]
    private Stat mana;
    

    private float initialHealth = 100;
    private float initialMana = 50;

    [SerializeField]
    private GameObject[] spellprefabs;

    [SerializeField]
    private Block[] blocks;

    [SerializeField]
    private Transform[] exitPoints;

    private int exitIndex = 2;

    public Transform myTarget {get; set;}

    protected override void Start()
    {
        health.Initialize(initialHealth, initialHealth);
        mana.Initialize(initialMana, initialMana);
        

        base.Start();
    }


    // Update is called once per frame
    protected override void Update ()
    {
        getInput();

        base.Update();
	}


    
    public void getInput()
    {

        //Used for debugging only
        if (Input.GetKeyDown(KeyCode.I))
        {
            health.myCurrentValue -= 10;
            mana.myCurrentValue -= 10;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            health.myCurrentValue += 10;
            mana.myCurrentValue += 10;
        }



        direction = Vector2.zero;
        if(Input.GetKey(KeyCode.W))
        {
            exitIndex = 0;
            direction += Vector2.up;
        }
        if (Input.GetKey(KeyCode.A))
        {
            exitIndex = 3;
            direction += Vector2.left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            exitIndex = 2;
            direction += Vector2.down;
        }
        if (Input.GetKey(KeyCode.D))
        {
            exitIndex = 1;
            direction += Vector2.right;
        }
    }
    public IEnumerator Attack(int spellIndex)
    {
        
        IsAttacking = true;

        myAnimator.SetBool("attack", IsAttacking);
        yield return new WaitForSeconds(1); // hardcoded cast time for attack

        Spell s = Instantiate(spellprefabs[spellIndex], exitPoints[exitIndex].position, Quaternion.identity).GetComponent<Spell>();

        s.myTarget = myTarget;

        stopAttack();
        }

    public void castSpell(int spellIndex)
    {
        Block();

        if (myTarget != null && !IsAttacking && !IsMoving && inLineOfSight())
        {
            attackRoutine = StartCoroutine(Attack(spellIndex));
        } 
    }
    private bool inLineOfSight()
    {
        Vector3 targetDirection = (myTarget.transform.position - transform.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDirection, Vector2.Distance(transform.position, myTarget.position),256);

        if(hit.collider == null)
        {
            return true;
        }

        return false;
    }
    private void Block()
    {
        foreach(Block b in blocks)
        {
            b.Deactivate();
        }
        blocks[exitIndex].Activate();
    }
}
