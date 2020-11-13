using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour, IDestructable
{
    [SerializeField] protected Animator m_anim;
    [SerializeField] protected Rigidbody2D m_rigidbody;

    [SerializeField] protected float m_speed;
    public float Speed 
    { 
        get { return m_speed; }
        set { m_speed = value;}
    }

    [SerializeField] protected float m_health;
    public float Health 
    {
        get { return m_health; }
        set { m_health = value; }
    }

    [SerializeField] protected float m_damage;
    public float Damage 
    {
        get { return m_damage; }
        set { m_damage = value; }
    }

    void Awake()
    {
        m_anim = GetComponent<Animator>();
        m_rigidbody = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        if(m_anim == null)
        {
            Debug.Log("no animator!");
        }
        else { Debug.Log("anim is here"); }
    }

    public virtual void Die()
    {

    }

    public void RecieveHit(float damage) 
    {

    }
}
