using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerAttack : Photon.MonoBehaviour {

    public GameObject _bulletPrefab;
    public float _power;

    float _timer;
    public float _fireDelay = 0.05f;

    private void Update()
    {
        if (!photonView.isMine) return;

        _timer += Time.deltaTime;
       
    }

    public void Attack(Vector3 direction, Vector3 pos, Quaternion qt, int playerId)
    {
        if (!photonView.isMine) return;

        if (_timer > _fireDelay)
        {
            photonView.RPC("Fire", PhotonTargets.AllViaServer, direction, pos, qt, playerId);
            _timer = 0;
        }
    }

    [PunRPC]
    public void Fire(Vector3 direction, Vector3 pos, Quaternion qt, int playerId)
    {
        
        GameObject bullet = Instantiate(_bulletPrefab, pos, qt);

        var bInfo = bullet.GetComponent<CBulletInfo>();
        bInfo.pId = playerId;
        bullet.GetComponent<Rigidbody>().velocity = direction * _power;

        Destroy(bullet, 5f);
    }

}
