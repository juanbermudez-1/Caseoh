using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGuns : MonoBehaviour
{
    [SerializeField] private Sprite bananaShotgun, bananaGun;
    [SerializeField] private SpriteRenderer playerSR;
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
                    playerSR.sprite = bananaGun;
                    playerSR.transform.rotation = Quaternion.Euler(0, 0, 66 + gun.rotation.eulerAngles.z);
                    break;
                }
                
            case 1:
                {
                    playerSR.sprite = bananaShotgun;
                    playerSR.transform.rotation = Quaternion.Euler(0, 0, 180 + gun.rotation.eulerAngles.z);

                    break;
                }

        }
    }
}
