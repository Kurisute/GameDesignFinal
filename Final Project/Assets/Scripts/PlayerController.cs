using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//TODO: add a death timer of arrow and summon char, add a bool control for summon char, add a timer for char, on timer, reset bool,
//TODO: add a fall death box
//TODO: timer for score count
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    protected GeneralController CC2D;
    [SerializeField]
    protected Animator animator;


    [Header("Character Parameters")]
    [SerializeField]
    private float Speed = 5f;
    [SerializeField]
    private float walkSpeed = 0.2f;
    [SerializeField]
    [Range(0, 1000)]
    protected float jumpForce = 200;

    [Header("Bow Parameters")]
    [SerializeField]
    protected GameObject ArrowPrefab;
    [SerializeField]
    protected Transform arrowSpawnPoint;
    [SerializeField]
    protected float fireForce = 400f;

    public float maxFireForce = 1f;
    [SerializeField]
    protected float currentFireForce = 0f;

    [Header("Collison Adjustment")]
    [SerializeField]
    protected float rushHitForce = 2000f;
    [SerializeField]
    protected float rushHitForceY = 2f;

    [Header("Companion Bot")]
    [SerializeField]
    protected GameObject companionBot;
    public bool canSummon = false;
    public float maxCoolDown = 10.0f;

    //for test only to use public
    public float coolDown = 0;


    //Used to modified enemy clash damage.
    [SerializeField]
    protected float enemyRushDamage = 10f;

    //[SerializeField]
    //protected LevelLoader loaderScript;

    protected bool prepareForShot = false;
    protected bool canShot = false;
    protected bool isDead = false;
    private bool dieOnce = false;

    public float arrowDestroyTimer = 3.0f;
    public float companionDestroyTimer = 5.0f;

    private bool isCharged = false;

    /*
    AudioSource[] soundEffects;
    AudioSource arrowEffect;
    AudioSource jumpEffect;
    AudioSource chargeShot;
    AudioSource collisionSound;
    */






    void Start()
    {
        /*
        soundEffects = GetComponents<AudioSource>();
        jumpEffect = soundEffects[1];
        arrowEffect = soundEffects[0];
        chargeShot = soundEffects[2];
        collisionSound = soundEffects[3];
        */
    }


    void Update()
    {
        /*
         * for death scene handling
        if (isDead)
        {

            this.CC2D.m_rigidbody2D.simulated = false;
            if (!dieOnce)
            {
                loaderScript.manualLoad(3);
            }
            return;
        }
        */
        CC2D.Move(new Vector2(Input.GetAxis("Horizontal") * this.Speed, 0f));
        CC2D.MoveV(new Vector2(0f, Input.GetAxis("Vertical") * this.Speed));

        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, Mathf.Abs(diff.x)) * Mathf.Rad2Deg;
        //this.transform.rotation = Quaternion.Euler(0f, 0f, rot_z);

        /*
        if (Input.GetKeyDown(KeyCode.R))
        {
            prepareForShot = true;
            currentFireForce = 0f;
        }

        if (Input.GetKeyDown(KeyCode.E) && canSummon)
        {
            ultimateSpawn();
            canSummon = false;
            coolDown = maxCoolDown;
        }
        if (coolDown > 0)
        {
            coolDown -= Time.deltaTime;
        }
        else
        {
            canSummon = true;
        }

        if (Input.GetButton("Attack") && canShot)
        {
            //holddown for higher power shot
            if (currentFireForce < maxFireForce)
            {
                currentFireForce += 0.04f;
                if (currentFireForce > maxFireForce)
                {
                    isCharged = true;
                    currentFireForce = maxFireForce;
                }
            }

        }
        Vector2 fire = (fireForce * currentFireForce) * diff;
        if (Input.GetButtonUp("Attack") && canShot)
        {
            //Debug.Log("shot fired");
            prepareForShot = false;
            if (isCharged)
            {
                chargeShot.Play();
            }
            else
            {
                arrowEffect.Play();
            }

            isCharged = false;

            FireArrow(fire);

            //currentFireForce = 0f;
            animator.SetTrigger("Shot");
        }
        */
        /*
        if (Input.GetButton("Walk"))
        {
            CC2D.Move(new Vector2(Input.GetAxis("Horizontal") * this.walkSpeed, 0f));
        }
        */

        if (CC2D.m_rigidbody2D.velocity.x < 0 && CC2D.FacingRight)
        {
            CC2D.Flip();
        }
        else if (CC2D.m_rigidbody2D.velocity.x > 0 && !CC2D.FacingRight)
        {
            CC2D.Flip();
        }


        //animation for walking
        animator.SetFloat("SpeedX", Mathf.Abs(CC2D.m_rigidbody2D.velocity.x));
        animator.SetFloat("SpeedY", Mathf.Abs(CC2D.m_rigidbody2D.velocity.y));

        //TODO: optional animations
        //animator.SetFloat("BodyRotation", rot_z);
        //animator.SetFloat("CurrentFireForce", currentFireForce);
        //animator.SetBool("CanShot", canShot);
        //animator.SetBool("hasUltimate", canSummon);
    }

    //Fix collision damage if touched enemy
    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("collision registered");
        //only destroy object if lid is hit, meant to be stored in box
        if (collision.gameObject.tag == "Enemy")
        {

            //collisionSound.Play();
            Rigidbody2D playerBody = this.GetComponent<Rigidbody2D>();
            /*
             * damage script
            //Health health = this.GetComponent<Health>();

            //playerBody.AddForce(new Vector2(this.transform.localScale.x * rushHitForce * (-1), 0));
            //health.TakeDamage(enemyRushDamage);
            //health.ApplyEffects(hitInfos[i].point);
            */

        }
    }

    /*
     * used to fire projectires
    public virtual void FireArrow(Vector2 force)
    {
        GameObject Arrow = Instantiate<GameObject>(ArrowPrefab, arrowSpawnPoint.position, Quaternion.identity);
        Rigidbody2D rigidbody = Arrow.GetComponent<Rigidbody2D>();
        rigidbody.AddForce(force);
        Destroy(Arrow, arrowDestroyTimer);
    }
    */

    /*
     * for death animator
    public virtual void Death()
    {
        isDead = true;
        //in death animation an event in health Die() is added
        animator.SetTrigger("Death");
    }
    
    public virtual void CanShotTrigger()
    {
        canShot = true;
    }
    public virtual void CanShotTriggerFalse()
    {
        canShot = false;

    }
    
    public virtual void ultimateSpawn()
    {

        if (CC2D.FacingRight)
        {
            GameObject companion = Instantiate<GameObject>(companionBot, new Vector2(this.transform.position.x + 2, this.transform.position.y + 2), Quaternion.identity);
            Destroy(companion, companionDestroyTimer);
        }

        else
        {
            GameObject companion = Instantiate<GameObject>(companionBot, new Vector2(this.transform.position.x - 2, this.transform.position.y + 2), Quaternion.identity);
            Destroy(companion, companionDestroyTimer);
        }

    }
    */
}
