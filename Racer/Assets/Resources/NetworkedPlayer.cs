using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Realtime;
using Photon.Pun;

public class NetworkedPlayer : MonoBehaviourPunCallbacks
{
    public static GameObject LocalPlayerInstance;
    public GameObject playerNamePrefab;
    public Rigidbody rb;
    public Renderer jeepMesh;
}
