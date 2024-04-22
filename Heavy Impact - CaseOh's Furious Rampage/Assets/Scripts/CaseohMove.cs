using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaseohMove : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] private GameObject gun, bullet, dashParticles;
    [SerializeField] private float cameraSmoothTime, dashAmount, moveSpeed, rollSpeed, momentumFalloff;
    [SerializeField] private Camera myCamera;
    [SerializeField] private Slider dashSlider;
    [SerializeField] private Animator securityAnim;
    [SerializeField] private TrailRenderer dashLine;
    [SerializeField] private Collider2D slamCollider;
    private bool dashCD = false;
    private bool isRolling = false;
    //public Vector2 momentum = Vector3.zero;
    //private float lateVelocityMag;
    List<KeyCode> keycodes = new List<KeyCode>
    {
        KeyCode.W,KeyCode.A,KeyCode.S, KeyCode.D,KeyCode.UpArrow,KeyCode.LeftArrow,KeyCode.RightArrow,KeyCode.DownArrow
    };
    private KeyCode[] keycodeArray;
    public static CaseohMove Instance;
    void Start()
    {
        Instance = this.GetComponent<CaseohMove>();
        keycodeArray = new KeyCode[keycodes.Count];
        for (int i = 0;i<=keycodeArray.Length ;i++ )
        {
            keycodeArray[i] = keycodes[i];
        }
        dashLine.emitting = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        //Gun();
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
        //Vector2 directionN = direction / (Mathf.Abs(direction.x) + Mathf.Abs(direction.y));
        /*momentum = (rb.velocity.magnitude-lateVelocityMag)*((rb.velocity.normalized/2)+(direction.normalized/2));
        momentum = Vector3.Lerp(momentum, Vector3.zero, 1 - Mathf.Pow(momentumFalloff, Time.deltaTime));
        if (double.IsNaN(momentum.x) || double.IsNaN(momentum.y))
            momentum = Vector2.zero;*/
        if (Input.GetKey(KeyCode.LeftShift) && direction.magnitude > 0.1f)
        {
            rb.AddForce(direction.normalized  * rollSpeed * Time.deltaTime);
            isRolling = true;
        }
        else if (direction.magnitude > 0.1f)
        {
            //rb.velocity = (direction.normalized * moveSpeed+momentum * Time.deltaTime);
            Vector3 moveVector = direction.normalized * moveSpeed * Time.deltaTime;
            this.transform.position += moveVector;
            isRolling = false;
        }
        //else rb.velocity = momentum;
        if (Input.GetKeyDown(KeyCode.Space)&&!isRolling)
            Dash(direction.normalized);
        //lateVelocityMag = rb.velocity.magnitude-momentum.magnitude;
        Animate();
    }
    private void Gun()
    {
        Vector2 direction1 = myCamera.ScreenToWorldPoint(Input.mousePosition) - this.transform.position;
        gun.transform.up = direction1;
        if (Input.GetMouseButtonDown(0))
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
            rb.velocity += direction * dashAmount;
            StartCoroutine(DashCD());
            GameObject dashParticles1 = Instantiate(dashParticles, this.transform.position, this.transform.rotation);
            Destroy(dashParticles1, 1);
            dashCD = true;
            dashSlider.value = 0;
        }

    }
    private IEnumerator DashCD()
    {
        dashLine.emitting = true;
        yield return new WaitForSeconds(1);
        dashLine.emitting = false;
        dashCD = false;

    }
    private void Animate()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector2 directionRaw = new Vector2(moveX, moveY);
        securityAnim.SetFloat("DirectionX", moveX);
        securityAnim.SetFloat("DirectionY", moveY);
        securityAnim.SetFloat("DirectionMag", directionRaw.magnitude);
        if(Input.GetKeyDown(KeyCode.LeftShift))securityAnim.SetTrigger("IsRolling");
        securityAnim.SetBool("IsRollingBool",isRolling);

        for (int i = 0; i < keycodeArray.Length; i++)
        {
            if (Input.GetKeyDown(keycodeArray[i])|| Input.GetKeyUp(keycodeArray[i])) securityAnim.SetTrigger("DirectionChanged");
        }
    }
    private void GroundSlam()
    {
        /*Collider2D[] gay = new Collider2D[10];
        ContactFilter2D yes = new ContactFilter2D(8);
        Physics2D.OverlapCollider(slamCollider,8,gay);*/
    }
}
