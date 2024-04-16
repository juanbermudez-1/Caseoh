using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed;
    private void Start()
    {
        Debug.Log(this.transform.rotation.z);
        Destroy(this.gameObject, 2);
    }
    // Update is called once per frame
    void Update()
    {
        float zRot = (this.transform.rotation.eulerAngles.z+90)*Mathf.PI/180;
        float speed = bulletSpeed * Time.deltaTime;
        this.transform.position += new Vector3(Mathf.Cos(zRot),Mathf.Sin(zRot),0).normalized*speed;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("hoorah!");
        if (collision.gameObject.layer == 8)
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
        if (collision.gameObject.layer == 10)
        {
            Destroy(this.gameObject);
        }
    }
}
