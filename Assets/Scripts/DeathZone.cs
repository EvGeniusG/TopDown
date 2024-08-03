
using UnityEngine;

class DeathZone : MonoBehaviour, IPlayerDamageSource{
    public bool IsDamageUnavoidable() => true;

    void OnTriggerEnter(Collider collider){
        var player = collider.GetComponent<IPlayerController>();

        if(player != null){
            player.Hit(this);
        }
    }
}