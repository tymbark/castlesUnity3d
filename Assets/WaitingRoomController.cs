using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;
using NetworkModels;
using System;

public class WaitingRoomController : MonoBehaviour {

    private void Start() {
        GetGameInfo();
    }

    private void GetGameInfo() {
        //StartCoroutine(NetworkController.GetGameInfo(AllGamesResponse));
    }
}
