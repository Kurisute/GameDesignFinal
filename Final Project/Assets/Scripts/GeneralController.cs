using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralController : MonoBehaviour
{
    [SerializeField]
    protected Rigidbody2D rigidbody2D;
    
    

    bool facingRight = false;
    

    //currently has no use since animation is one sided
    //TODO: need to make mirror walk animation for this to work
    public virtual bool FacingRight
    {
        get
        {
            return this.facingRight;
        }
    }
    public virtual Rigidbody2D m_rigidbody2D
    {
        get
        {
            return this.rigidbody2D;
        }
    }
    
    
    public virtual void Move(Vector2 newVelocity)
    {
        
        Vector2 velocity = rigidbody2D.velocity;
        velocity.x = newVelocity.x;
        rigidbody2D.velocity = velocity;
    }
    public virtual void MoveV(Vector2 newVelocity)
    {
        
        Vector2 velocity = rigidbody2D.velocity;
        velocity.y = newVelocity.y;
        rigidbody2D.velocity = velocity;
    }

    public void Flip()
    {

        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;


    }
}