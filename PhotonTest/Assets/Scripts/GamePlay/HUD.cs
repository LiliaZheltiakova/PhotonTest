using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    static private HUD m_instance;
    static public HUD Instance 
    {
        get { return m_instance; }
    }

    [SerializeField] private GameObject heart;
    [SerializeField] private Image numberLabel;
    [SerializeField] private Sprite[] numberUnits = new Sprite[10];
    private int m_crystallNumber;
    public int Crystalls 
    {
        get { return m_crystallNumber; }
        set { m_crystallNumber = value; }
    }

    void Awake()
    {
        m_instance = this;
    }
    void Start()
    {
    }

    public void UpdateCrystall()
    {
        Crystalls++;
        if(Crystalls < 10)
        {
            numberLabel.sprite = numberUnits[Crystalls];
        }

        else
        {
            // case when need add decimal part of number
            Debug.Log("Decimal part of number is reqiered!");
        }
    }
}
