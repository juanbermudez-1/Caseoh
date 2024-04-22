using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed;
<<<<<<< Updated upstream:Heavy Impact - CaseOh's Furious Rampage/Assets/Bullet.cs
    [SerializeField] int enemyLayer; 
=======
    private int thisHealth;
>>>>>>> Stashed changes:Heavy Impact - CaseOh's Furious Rampage/Assets/Scripts/Bullet.cs
    private void Start()
    {
        thisHealth = 3;
        Debug.Log(this.transform.rotation.z);
        Destroy(this.gameObject, 3);
    }
    // Update is called once per frame
    void Update()
    {
        float zRot = (this.transform.rotation.eulerAngles.z+90)*Mathf.PI/180;
        float speed = bulletSpeed * Time.deltaTime;
        this.transform.position += new Vector3(Mathf.Cos(zRot),Mathf.Sin(zRot),0).normalized*speed;
        if (thisHealth <= 0) Destroy(this.gameObject);
    }
<<<<<<< Updated upstream:Heavy Impact - CaseOh's Furious Rampage/Assets/Bullet.cs
    private void OnCollisionEnter2D(Collision2D collision)
=======
    private void OnTriggerEnter2D(Collider2D collision)
>>>>>>> Stashed changes:Heavy Impact - CaseOh's Furious Rampage/Assets/Scripts/Bullet.cs
    {
        if(collision.collider.gameObject.layer == 8)
        {
            Destroy(collision.gameObject);
            if (Gun.gunMode == 2)
            {
                --thisHealth;
            }
            else Destroy(this.gameObject);
        }
<<<<<<< Updated upstream:Heavy Impact - CaseOh's Furious Rampage/Assets/Bullet.cs
        else if(collision.collider.gameObject.layer == 7)
=======
        if (collision.gameObject.layer == 7)
>>>>>>> Stashed changes:Heavy Impact - CaseOh's Furious Rampage/Assets/Scripts/Bullet.cs
        {
            Destroy(this.gameObject);
        }
    }
}
