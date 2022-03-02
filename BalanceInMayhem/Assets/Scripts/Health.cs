using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Health : MonoBehaviour
{
    public int healthPoints = 100;
    public Text hpDisplay;
    PhotonView view; 
    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDmg()
    {
        view.RPC("TakeDmgRPC", RpcTarget.All);
    }

    [PunRPC]
    void TakeDmgRPC()
    {
        healthPoints--;
        hpDisplay.text = healthPoints.ToString();
    }
}
