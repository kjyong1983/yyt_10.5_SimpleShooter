﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (!PhotonNetwork.isMasterClient) return;

        

    }

    [PunRPC]
    void DestroyObject(Collider other)
    {

        PhotonNetwork.Destroy(other.gameObject);
    }
}
