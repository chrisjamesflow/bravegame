using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeToWallJump : MonoBehaviour
{
    public float animationTime = 4f;
    public Animator animator;
    private AudioSource Audio;

    private void Start()
    {
        if (PlayerTouchingWallState.wallJumpAbility == true) Destroy(gameObject);
        Audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerTouchingWallState.wallJumpAbility = true;
            PlayerLedgeClimbState.wallJumpAbility = true;
            StartCoroutine(GetOrb());
        }
    }

    IEnumerator GetOrb()
    {
        Audio.Stop();
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(animationTime);
        PlayerManager.instance.transform.position = new Vector2(transform.position.x, transform.position.y + 1);
        PlayerManager.instance.transform.gameObject.SetActive(true);
        Destroy(gameObject);
    }
}
