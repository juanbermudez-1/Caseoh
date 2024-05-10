using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Animator animator;
    private bool nearby;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            nearby = true;
            animator.SetBool("Near", nearby);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            nearby = false;
            animator.SetBool("Near", nearby);
        }

    }
    private void Interact()
    {
        if (!nearby)
            return;
        animator.SetTrigger("Interact");
        ChangeGuns.Instance.SwitchGuns(Random.Range(0,3));
    }
}
