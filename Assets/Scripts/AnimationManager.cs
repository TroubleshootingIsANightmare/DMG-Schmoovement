using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public Animator playerAnimator;
    public PlayerMovement playerMovement;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerAnimator.SetFloat("PosX", Input.GetAxisRaw("Horizontal"));
        playerAnimator.SetFloat("PosY", Input.GetAxisRaw("Vertical"));
        playerAnimator.SetFloat("VelY", playerMovement.rb.velocity.y);
        playerAnimator.SetBool("Grounded", playerMovement.grounded);
        playerAnimator.SetBool("Shooting", Input.GetMouseButtonDown(0));
    }
}
