using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private GameObject[] hearts;
    [SerializeField] private Image numberLabel;
    [SerializeField] private Sprite[] numberUnits = new Sprite[10];
    private int m_crystallNumber;
    private int current_live;
    public int Crystalls 
    {
        get { return m_crystallNumber; }
        set { m_crystallNumber = value; }
    }

    void Awake()
    {
    }
    void Start()
    {
        current_live = 3;
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

    public void UpdateLives(int amount)
    {
        if(current_live > amount)
        {
            hearts[amount].GetComponent<Animator>().SetTrigger("Destroy");
            Destroy(hearts[amount].gameObject, .5f);
            current_live--;
            Debug.Log("Destroy heart");
        }
    }
}