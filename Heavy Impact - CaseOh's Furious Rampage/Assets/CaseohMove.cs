using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseohMove : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject gun, bullet;
    [SerializeField] float cameraSmoothTime, dashAmount, moveSpeed, rollSpeed;
    [SerializeField] Camera myCamera;
    private bool dashCD = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Gun();
        CameraFollow();
    }
    private void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        Vector3 direction = (moveX * Vector3.right) + (moveY * Vector3.up);
        if (Input.GetKey(KeyCode.LeftShift)) rb.AddForce(direction * rollSpeed * Time.deltaTime * 100);
        else rb.velocity = direction * moveSpeed * Time.deltaTime*15;
        if(Input.GetKeyDown(KeyCode.Space))Dash(direction);
    }
    private void Gun()
    {
        Vector2 direction1 = myCamera.ScreenToWorldPoint(Input.mousePosition) - this.transform.position;
        gun.transform.up = direction1;
        if(Input.GetMouseButtonDown(0))
        {
            GameObject bullet1 = bullet;
            Instantiate(bullet, gun.transform.position, gun.transform.rotation);
        }
    }
    private void CameraFollow()
    {
        Vector3 zero = Vector3.zero;
        Vector3 posOffset = new Vector3(0, 0, -10);
        myCamera.transform.position = Vector3.SmoothDamp(myCamera.transform.position, this.transform.position+posOffset, ref zero, cameraSmoothTime);
    }
    private void Dash(Vector3 direction)
    {
        if (!dashCD)
        {
            rb.AddForce(direction * dashAmount, ForceMode2D.Impulse);
            StartCoroutine(DashCD());
        }
    }
    private IEnumerator DashCD()
    {
        dashCD = true;
        yield return new WaitForSeconds(1);
        dashCD = false;

    }
}
