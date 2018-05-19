using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NetworkModels {

    public class GameInfo {

        public string Id;
        public bool Available;
        public string CreatorNickName;
        public int PlayersMax;
        public int PlayersNow;
        public List<string> PlayersNicknames;

        public GameInfo(string id,
                        bool available,
                        string creatorNickName,
                        int playersMax,
                        int playersNow,
                        List<string> playersNicknames) {

            Id = id;
            Available = available;
            CreatorNickName = creatorNickName;
            PlayersMax = playersMax;
            PlayersNow = playersNow;
            PlayersNicknames = playersNicknames;

        }

        public string Describe() {
            string players = "";
            foreach (string s in PlayersNicknames) {
                players = players + s + " ";
            }
            return "id:" + Id
                + "\navailable:" + Available
                + "\ncreatorNickName" + CreatorNickName
                + "\nMax:" + PlayersMax
                + "\nNow:" + PlayersNow
                + "\nplayers:" + players;
        }
    }

    public class GameStateWithId {
        public string id;
        public string game_data;
        public GameStateWithId() { }
        public GameStateWithId(string id, string data) {
            this.id = id;
            this.game_data = data;
        }
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