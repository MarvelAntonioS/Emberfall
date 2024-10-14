using UnityEngine;
using System.Collections;

public class AxeSwing : MonoBehaviour
{
    public GameObject axe; // Reference to the axe object
    private Animator axeAnimator; // Cache the Animator component

    void Start()
    {
        if (axe != null)
        {
            axeAnimator = axe.GetComponent<Animator>();
        }
        else
        {
            Debug.LogError("Axe GameObject not assigned.");
        }
    }

    // This method will be called to play the axe swing animation
    public void TriggerAxeSwing()
    {
        if (axeAnimator != null)
        {
            StartCoroutine(PlayAxeSwingAnimation());
        }
    }

    IEnumerator PlayAxeSwingAnimation()
    {
        // Check if the animation is already playing to prevent overlap
        if (axeAnimator.GetCurrentAnimatorStateInfo(0).IsName("AxeSwingAni"))
        {
            yield break;
        }

        axeAnimator.Play("AxeSwingAni");
        yield return new WaitForSeconds(1.0f); // Adjust the wait time based on the animation length
        axeAnimator.Play("Idle"); // Change "Idle" to whatever the default state is
    }
}