using UnityEngine;

public class EnemyView : MonoBehaviour, IEnemyView
{

    [SerializeField] GameObject ragdollPrefab;
    [SerializeField] Animator animator;

    IEnemyController controller;

    private void Awake()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        controller = GetComponent<IEnemyController>();
    }

    void FixedUpdate(){
        float speed = controller.GetSpeed();
        SetAnimationRun(speed > 0);
        SetAnimationSpeed(speed);
    }

    public void CreateRagdoll()
    {
        if (ragdollPrefab != null)
        {
            Instantiate(ragdollPrefab, transform.position, transform.rotation);
        }
    }

    public void SetAnimationRun(bool isRunning)
    {
        if (animator != null)
        {
            animator.SetBool("Run", isRunning);
        }
    }

    public void SetAnimationSpeed(float speed)
    {
        if (animator != null)
        {
            animator.SetFloat("Speed", speed);
        }
    }
}
