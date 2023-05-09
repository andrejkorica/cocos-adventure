using UnityEngine;

public class AnimationCoco : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            animator.SetBool("isRunning", true);
        }
        else if (animator.GetBool("isRunning"))
        {
            animator.SetTrigger("isStopping");
            animator.SetBool("isRunning", false);
        }
    }

    // Ova metoda poziva se na kraju animacije "CocoSlowing"
    public void StopRunning()
    {
        animator.SetBool("isStanding", true);
    }
}
