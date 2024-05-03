using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class HealthManager : MonoBehaviour
{
    private float health = 5;
    private bool invincibility = false;
    [SerializeField]private TextMeshProUGUI healthText;
    void Update()
    {
        healthText.text = health.ToString();
        if (health <= 0)
        {
            this.gameObject.SetActive(false);
            SceneManager.LoadScene("SampleScene");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8&&!invincibility)
        {
            StartCoroutine(InvincFrames());
            --health;
        }
    }
    IEnumerator InvincFrames()
    {
        invincibility = true;
        yield return new WaitForSeconds(1);
        invincibility = false;
    }
}
