using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ExitGames.Client.Photon;

public class CPlayerStat : Photon.MonoBehaviour {

    Text _nameText;
    Text _scoreText;

    private void Awake()
    {
        _nameText = GetComponentInChildren<CPlayerName>().GetComponent<Text>();
        _scoreText = GetComponentInChildren<CPlayerScore>().GetComponent<Text>();

    }

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (photonView.owner.NickName != null)
        {
            _nameText.text = photonView.owner.NickName;
        }

        _scoreText.text = photonView.owner.GetScore().ToString();
	}

    public void Save(Hashtable info)
    {
        PhotonNetwork.player.SetCustomProperties(info);
    }

    public Hashtable Load()
    {
        return photonView.owner.CustomProperties;
    }

    public void ClearInfo()
    {
        Hashtable info = Load();

        info.Clear();

        photonView.owner.SetScore(0);
    }

}
