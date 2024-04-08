using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaseohMove : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject gun, bullet, dashParticles;
    [SerializeField] private float cameraSmoothTime, dashAmount, moveSpeed, rollSpeed, momentumFalloff;
    [SerializeField] private Camera myCamera;
    [SerializeField] private Slider dashSlider;
    private bool dashCD = false;
    private bool isRolling = false;
    private Vector2 momentum = Vector3.zero;
    private float lateVelocityMag;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Gun();
        CameraFollow();
        if (dashSlider.value <= 1)
        {
            dashSlider.value += Time.deltaTime;
        }
    }
    private void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        Vector2 direction = (moveX * Vector3.right) + (moveY * Vector3.up);
        momentum = (rb.velocity.magnitude-lateVelocityMag)*(rb.velocity.normalized/2+(direction.normalized/2));
        if (Input.GetKey(KeyCode.LeftShift))
        {
            rb.AddForce((direction * rollSpeed * Time.deltaTime * 100) + momentum);
            isRolling = true;
        }
        else
        {
            rb.velocity = direction * moveSpeed * Time.deltaTime * 15 + (momentum);
            isRolling = false;
        }
        if (Input.GetKeyDown(KeyCode.Space)&&!isRolling)
            Dash(direction);
        momentum = Vector3.Lerp(momentum, Vector3.zero,1- Mathf.Pow(momentumFalloff,Time.deltaTime));
        lateVelocityMag = rb.velocity.magnitude-momentum.magnitude;
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
    private void Dash(Vector2 direction)
    {
        if (!dashCD)
        {
            momentum += direction * dashAmount;
            StartCoroutine(DashCD());
            GameObject dashParticles1 = Instantiate(dashParticles, this.transform.position, this.transform.rotation);
            Destroy(dashParticles1, 1);
            dashCD = true;
            dashSlider.value = 0;
        }

    }
    private IEnumerator DashCD()
    {
        yield return new WaitForSeconds(1);
        dashCD = false;

    }
}
