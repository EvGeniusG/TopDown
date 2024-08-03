using UnityEditor;
using UnityEngine;

public class PlayerView : MonoBehaviour, IPlayerView
{
    [SerializeField] GameObject Ragdoll;
    private Animator animator;
    private IPlayerModel playerModel;
    private IPlayerController playerController;

    [SerializeField] GameObject SpeedEffect, InvincibilityEffect;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<IPlayerController>();
        playerModel = GetComponent<IPlayerModel>();
    }

    private void FixedUpdate()
    {
        UpdateMovementAnimation();
        UpdateWeaponLayerWeights();
        SetSpeedEffect();
        SetInvincibilityEffect();
    }

    public void ActivateRagdoll()
    {
        Instantiate(Ragdoll, transform.position, transform.rotation);
    }

    private void SetSpeedEffect(){
        SpeedEffect.SetActive(playerModel.HasSpeedBoost());
    }
    private void SetInvincibilityEffect(){
        InvincibilityEffect.SetActive(playerModel.IsInvincible());
    }


    private void UpdateMovementAnimation()
    {
        if (playerController == null) return;

        Vector3 velocity = playerController.move;
        float speed = velocity.magnitude / Time.fixedDeltaTime;
        Vector3 forward = transform.forward;


        if(Vector3.Angle(velocity, forward)  > 180)
            animator.SetFloat("Speed", -speed);
        else
            animator.SetFloat("Speed", speed);
        animator.SetBool("Run", speed > 0); 
    }

    private void UpdateWeaponLayerWeights()
    {
        if (playerModel == null) return;

        Weapon currentWeapon = playerModel.GetWeapon();
        for (int i = 0; i < animator.layerCount; i++)
        {
            string layerName = animator.GetLayerName(i);
            if(layerName == "Move Layer") continue;
            float targetWeight = (layerName == currentWeapon.name + " Layer") ? 1f : 0f;
            animator.SetLayerWeight(i, targetWeight);
        }
    }
}
