using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NetworkModels {

    public class Game {

        public string Id;
        public bool Available;
        public string CreatorName;
        public int PlayersMax;
        public int PlayersNow;
        public List<string> PlayersIds;

        public Game(string id,
                    bool available,
                    string creatorName,
                    int playersMax,
                    int playersNow,
                    List<string> playersIds) {

            Id = id;
            Available = available;
            CreatorName = creatorName;
            PlayersMax = playersMax;
            PlayersNow = playersNow;
            PlayersIds = playersIds;

        }

        public string Describe() {
            string players = "";
            foreach (string s in PlayersIds) {
                players = players + s + " ";
            }
            return "id:" + Id
                + "\navailable:" + Available
                + "\ncreatorName" + CreatorName
                + "\nMax:" + PlayersMax
                + "\nNow:" + PlayersNow
                + "\nplayers:" + players;
        }
    }

    public class GameStateWithId {
        public string id;
        public string game_data;
    }

    public class ResponseOrError<T> {
        public readonly T Response;
        public readonly string ErrorMessage;
        private bool isError = false;
        public bool IsError { get { return isError; } }
        public bool IsSuccess { get { return !isError; } }

        public ResponseOrError(T response) {
            Response = response;
            isError = false;
        }

        public ResponseOrError(string error) {
            ErrorMessage = error;
            isError = true;
        }

    }


}