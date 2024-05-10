using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGuns : MonoBehaviour
{
    [SerializeField] private Sprite bananaShotgun, bananaGun, bananaSniper;
    [SerializeField] private SpriteRenderer gunSR;
    [SerializeField] private Transform gun;
    public static ChangeGuns Instance;
    private void Start()
    {
        Instance = this.GetComponent<ChangeGuns>();
    }
    public void SwitchGuns(int balls)
    {
        Gun.gunMode = balls;
        switch(balls)
        {
            case 0:
                {
                    gunSR.sprite = bananaGun;
                    gunSR.transform.rotation = Quaternion.Euler(0, 0, 90 + gun.rotation.eulerAngles.z);
                    break;
                }
                
            case 1:
                {
                    gunSR.sprite = bananaShotgun;
                    gunSR.transform.rotation = Quaternion.Euler(0, 0, 90 + gun.rotation.eulerAngles.z);

                    break;
                }
            case 2:
                {
                    gunSR.sprite = bananaSniper;
                    gunSR.transform.rotation = Quaternion.Euler(0, 0, 90 + gun.rotation.eulerAngles.z);
                    break;
                }
        }
    }
}
