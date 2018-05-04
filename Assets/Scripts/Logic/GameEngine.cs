using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Models;

public class GameEngine {

    private readonly int howManyPlayers = 2; //to change this

    public GameState GameState { get; private set; }
    public ActionHandler ActionHandler { get; private set; }

    public GameEngine() {
        if (GameState == null) {
            if (false && PlayerPrefs.HasKey("game_state_cob")) {
                Debug.Log("loading game state...");
                string gameStateJson = PlayerPrefs.GetString("game_state_cob");
                GameState = JsonUtility.FromJson<GameState>(gameStateJson);
            } else {
                Debug.Log("create new game state");
                GameState = GenerateGameState();
                string gameStateJson = JsonUtility.ToJson(GameState);
                PlayerPrefs.SetString("game_state_cob", gameStateJson);
                StartTurn();
            }
        }

        ActionHandler = new ActionHandler(this, GameState.AvailableProjectCards);
    }

    private GameState GenerateGameState() {
        var mainDeck = DeckGenerator.GenerateActionsDeck();
        var animalsDeck = DeckGenerator.GenerateAnimalsDeck();
        var goodsDeck = DeckGenerator.GenerateGoodsDeck();
        var projectCards = PrepareProjectCards(mainDeck, howManyPlayers);
        var players = PreparePlayers(animalsDeck, goodsDeck);

        return new GameState(players, mainDeck, animalsDeck, goodsDeck, 
                             projectCards, Round.A, players[0], players.Count);
    }

    private List<Player> PreparePlayers(Deck animalsDeck, Deck goodsDeck) {
        List<Player> Players = new List<Player>();
        for (int i = 0; i < howManyPlayers; i++) {
            int startingWorkers;
            switch (i) {
                case 0:
                    startingWorkers = 0;
                    break;
                case 3:
                    startingWorkers = 2;
                    break;
                default:
                    startingWorkers = 1;
                    break;
            }

            Players.Add(new Player("ewa" + i, animalsDeck.DrawCard(), goodsDeck.DrawCard(), startingWorkers));
        }

        //Players[0].Estate.AddProjectToEstate(ActionsDeck.DrawCard());
        //Players[0].ProjectArea.Add(ActionsDeck.DrawCard());

        return Players;
    }


    public void StartTurn() {
        GameState.CurrentPlayer = GameState.Players[0];
        DrawCards();
    }


    public void NextTurn() {

        if (GameState.Players.IndexOf(GameState.CurrentPlayer) == GameState.Players.Count - 1) {
            GameState.CurrentPlayer = GameState.Players[0];
        } else {
            GameState.CurrentPlayer = GameState.Players[GameState.Players.IndexOf(GameState.CurrentPlayer) + 1];
        }

        if (GameState.CurrentPlayer.Cards.Count == 1) {
            DrawCards();
        }
    }

    private void DrawCards() {
        foreach (Player p in GameState.Players) {
            p.DrawCards(GameState.MainDeck);
        }
    }

    private static List<ProjectCard> PrepareProjectCards(Deck mainDeck, int playersCount) {
        var AvailableProjectCards = new List<ProjectCard>();

        int howManyProjectCardsPerTurn = 0;

        switch (playersCount) {
            case 2:
                howManyProjectCardsPerTurn = 7;
                break;
            case 3:
                howManyProjectCardsPerTurn = 10;
                break;
            case 4:
                howManyProjectCardsPerTurn = 13;
                break;
        }

        for (int i = 0; i < howManyProjectCardsPerTurn; i++) {

            Card topCardFromDeck = mainDeck.DrawCard();
            CardDice cardDice;

            switch (i) {
                case 0:
                    cardDice = CardDice.I;
                    break;
                case 1:
                    cardDice = CardDice.II;
                    break;
                case 2:
                    cardDice = CardDice.III;
                    break;
                case 3:
                    cardDice = CardDice.IV;
                    break;
                case 4:
                    cardDice = CardDice.V;
                    break;
                case 5:
                    cardDice = CardDice.VI;
                    break;
                default:
                    cardDice = topCardFromDeck.Dice;
                    break;
            }


            AvailableProjectCards.Add(new ProjectCard(topCardFromDeck, cardDice));
        }

        return AvailableProjectCards;
    }

}