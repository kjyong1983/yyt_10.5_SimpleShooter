using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CObjectSpawner : Photon.MonoBehaviour
{

    float _timer = 0;
    public float _delayTime = 1f;

    public string[] _objectPrefabs;


    private void Update()
    {
        if (!PhotonNetwork.isMasterClient) return;

        _timer += Time.deltaTime;
        
        if (_timer > _delayTime)
        {
            photonView.RPC("CreateObject", PhotonTargets.AllViaServer);
            _timer = 0;
        }
    }

    [PunRPC]
    void CreateObject()
    {
        if (!PhotonNetwork.isMasterClient) return;

        Transform pTransform = GameObject.Find("Objects").transform;
        if (pTransform.childCount > 20) return;
        
        string prefabNum = _objectPrefabs[Random.Range(0, _objectPrefabs.Length)];

        float x = Random.Range(-45f, 45f);
        float y = Random.Range(-45f, 45f);

        //Vector2 pos = Random.insideUnitCircle * Random.Range(0.1f, 10f);
        Vector2 pos = new Vector2(x, y);
        GameObject newObject = PhotonNetwork.InstantiateSceneObject(prefabNum, pos, Quaternion.identity, 0, null);
        newObject.transform.SetParent(pTransform);
    }

}
