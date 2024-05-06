using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject gun, bullet;
    [SerializeField] private Camera myCamera;
    [SerializeField] private Slider gunSlider;
    [SerializeField] private float shotGunRecoil;
    public static int gunMode;
    private bool shootTimeBool = true;
    private float shootCD = 1;

    // Update is called once per frame
    void Update()
    {
        /*gun.GetComponent<SpriteRenderer>().enabled = CaseohMove.Instance.isRolling;
        if(CaseohMove.Instance.isRolling)
        {
            return;
        }*/
        gunSlider.maxValue = shootCD;
        Vector2 direction1 = myCamera.ScreenToWorldPoint(Input.mousePosition) - this.transform.position;
        gun.transform.up = direction1;
        switch (gunMode)
        {
            case 0:
                Pistol();
                break;
            case 1:
                Shotgun(direction1);
                break;
            case 2:
                Sniper();
                break;
        }
        if (gunSlider.value <= shootCD)
        {
            gunSlider.value += Time.deltaTime;
        }
    }
    void Pistol()
    {
        shootCD = 0.2f;
        if (Input.GetMouseButtonDown(0)&&shootTimeBool)
        {
            gunSlider.value = 0;
            GameObject bullet1 = bullet;
            Instantiate(bullet, gun.transform.position, gun.transform.rotation);
            StartCoroutine(ShootingCooldown(shootCD));
        }
    }
    void Shotgun(Vector2 direction)
    {
        shootCD = 1;
        if (Input.GetMouseButtonDown(0)&&shootTimeBool)
        {
            CaseohMove.Instance.rb.velocity -= direction.normalized*shotGunRecoil;
            gunSlider.value = 0;
            GameObject bullet1 = bullet;
            float bulletSpread = 50;
            float bulletCount = 6;
            for (int i = 0; i<bulletCount;i++)
            {
                Quaternion rotationOffset = Quaternion.Euler(0, 0, (bulletSpread/6*i)-(bulletSpread / 2)+gun.transform.rotation.eulerAngles.z);
                Instantiate(bullet, gun.transform.position, rotationOffset);
                StartCoroutine(ShootingCooldown(shootCD));
            }
        }
    }
    void Sniper()
    {
        shootCD = 1.5f;
        if (Input.GetMouseButtonDown(0)&&shootTimeBool)
        {
            gunSlider.value = 0;
            GameObject bullet1 = bullet;
            Instantiate(bullet, gun.transform.position, gun.transform.rotation);
            StartCoroutine(ShootingCooldown(shootCD));
        }
    }
    IEnumerator ShootingCooldown(float balls)
    {
        shootTimeBool = false;
        yield return new WaitForSeconds(balls);
        shootTimeBool = true; 
    }
}
