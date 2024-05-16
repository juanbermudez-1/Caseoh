using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private float balls;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //case0 = health pickup
        //case1 = point pickup
        //case2 = invincibility pickup
        switch (balls)
        {
            case 0:
                {
                    
                    break;
                }

            case 1:
                {
                    break;
                }
            case 2:
                {
                    break;
                }
        }
    }
}
