using UnityEngine;

public interface IPlayerController{
    Transform playerTransform {get;}
    Vector3 move {get;}

    void Hit(IPlayerDamageSource source);

    IPlayerModel GetModel();

    void SetMoveDirection(Vector3 direction);
    void SetTargetDirection(Vector3 direction);
    void StopShooting();
}