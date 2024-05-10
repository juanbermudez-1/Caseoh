using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CaseohMove : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] private GameObject gun, bullet, dashParticles;
    [SerializeField] private float cameraSmoothTime, dashAmount, moveSpeed, rollSpeed, momentumFalloff,deceleration;
    [SerializeField] private Camera myCamera;
    [SerializeField] private Slider dashSlider;
    [SerializeField] private Animator securityAnim;
    [SerializeField] private TrailRenderer dashLine;
    [SerializeField] private ParticleSystem rollSmoke;
    [SerializeField] private TextMeshProUGUI scoreText,hiScoreText;
    private bool dashCD = false;
    public bool isRolling = false;
    public static float hiScore;
    public GameObject hiScoreObj;
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
        hiScoreObj.SetActive(false);
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
        scoreText.text = "Score: " + Mathf.RoundToInt(Time.timeSinceLevelLoad * 5).ToString();
        hiScoreText.text = hiScore.ToString();
        Move();
        CameraFollow();
        if (dashSlider.value <= 1)
        {
            dashSlider.value += Time.deltaTime;
        }
        if (Time.timeSinceLevelLoad * 5 > hiScore)
        {
            hiScore = Time.timeSinceLevelLoad * 5;
        }
    }
    private void Move()
    {
        
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        Vector2 direction = (moveX * Vector3.right) + (moveY * Vector3.up);
        float acceleration = (moveSpeed/(rb.velocity.magnitude+0.2f))-1;
        if (Input.GetKey(KeyCode.LeftShift) && direction.magnitude > 0.1f)
        {
            rb.AddForce(direction.normalized  * rollSpeed * Time.deltaTime);
            isRolling = true;
        }
        else if (direction.magnitude > 0.1f)
        {
            rb.AddForce(direction.normalized * acceleration * Time.deltaTime);
            rb.velocity = rb.velocity.magnitude * direction.normalized;
            isRolling = false;
        }
        else if(!isRolling) rb.velocity -= rb.velocity/deceleration;
        if (Input.GetKeyDown(KeyCode.Space)&&!isRolling) Dash(direction.normalized);
        if (Input.GetKeyDown(KeyCode.Q) && !isRolling) GroundSlam();
        RollingSmoke(direction);
        Animate();
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
            rb.velocity = direction.normalized * (dashAmount+rb.velocity.magnitude);
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
        Collider2D[] hit = Physics2D.OverlapCircleAll(this.transform.position,3,8);
        Debug.Log(hit);
        foreach(Collider2D a in hit)
        {
            Debug.Log("gSlamHit");
        }
    }
    private void RollingSmoke(Vector2 direction)
    {
        var emm = rollSmoke.emission;
        emm.enabled = isRolling&&(rb.velocity+direction).magnitude<2;
        var shape = rollSmoke.shape;
        shape.rotation = new Vector3(0, 0, Mathf.Atan2(direction.x, -direction.y)*180/Mathf.PI);
    }
}
