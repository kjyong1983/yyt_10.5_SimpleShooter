using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGameManager : MonoBehaviour {

    //public GameObject _playerPrefab;

    internal void CreateNetPlayer()
    {
        Vector3 pos = Random.insideUnitCircle * Random.Range(0f, 10f);
        GameObject g = PhotonNetwork.Instantiate("Player", pos, Quaternion.identity, 0);

        PhotonNetwork.Instantiate("ObjectSpawner", Vector3.zero, Quaternion.identity, 0);
        new GameObject("Objects");
        Camera.main.GetComponent<CFollowCam>().Init(g.transform);


    }

    //플레이어는 인디케이터를 나타내야함.
}
