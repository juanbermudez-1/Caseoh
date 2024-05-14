using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class HealthManager : MonoBehaviour
{
    [SerializeField]private float health,damage,invFrames;
    private bool invincibility = false;
    [SerializeField]private TextMeshProUGUI healthText;
    [SerializeField] private GameObject sparks;
    void Update()
    {
        healthText.text = health.ToString();
        if (health <= 0)
        {
            CaseohMove.Instance.hiScoreObj.SetActive(true);
            CaseohMove.Instance.enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            StartCoroutine(WaitForEnd());
        }
    }
    public void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8&&!invincibility)
        {
            StartCoroutine(InvincFrames());
            health -= damage;
            CaseohMove.hiScore -= 2;
            Vector2 collisionPoint = collision.GetContact(0).point;
            GameObject sparks1 = sparks;
            Instantiate(sparks1, collisionPoint,Quaternion.identity);
            Destroy(sparks1, 1);
        }
    }
    IEnumerator InvincFrames()
    {
        invincibility = true;
        yield return new WaitForSeconds(invFrames);
        invincibility = false;
    }
    IEnumerator WaitForEnd()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("SampleScene");

    }
}
