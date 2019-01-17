using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ExitGames.Client.Photon;

public class CPlayerHealth : Photon.MonoBehaviour {

    public float _hp = 100;
    float _maxHp = 100;
    public float _damage;
    public bool _isDead;

    CPlayerStat _stat;
    Image _hpProgress;

    private void Awake()
    {
        _stat = GetComponent<CPlayerStat>();
        _hpProgress = GetComponentInChildren<CHpProgress>().GetComponent<Image>();

        
    }

    // Use this for initialization
    void Start () {
        Hashtable info = _stat.Load();
        if (info.ContainsKey("HP"))
        {
            float hp = (float)info["HP"];
            _hp = hp;
        }
	}
	
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            if (!PhotonNetwork.isMasterClient) return;
            if (_isDead) return;

            CBulletInfo info = other.GetComponent<CBulletInfo>();

            int shooterPId = info.pId;

            if (shooterPId == photonView.viewID)
            {
                return;
            }

            photonView.RPC("Hit", PhotonTargets.AllViaServer, 20f, shooterPId);
            
        }

        Destroy(other.gameObject);
    }

    [PunRPC]
    public void Hit(float damage, int pId)
    {
        if (!photonView.isMine) return;

        HpDown(damage, pId);

    }

    public void HpDown(float damage, int pId)
    {
        _hp -= damage;

        if (photonView.isMine)
        {
            //스탯 변경 및 저장
            Hashtable info = _stat.Load();

            if (info.ContainsKey("HP"))
            {
                info["HP"] = _hp;
            }
            else info.Add("HP", _hp);

            _stat.Save(info);
        }

        _hpProgress.fillAmount -= _hp / _maxHp;

        if (_hp <= 0)
        {
            OnDie();

            if (!PhotonNetwork.isMasterClient) return;

            PhotonView shooterPv = PhotonView.Find(pId);

            if (shooterPv != null)
            {
                shooterPv.owner.AddScore(1);
            }
        }

    }

    void OnDie()
    {
        if (!photonView.isMine) return;

        _stat.ClearInfo();

        PhotonNetwork.Destroy(gameObject);

        PhotonNetwork.LeaveRoom();
        
    }
}
