using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Models;

public class GameEngine {

    int howManyPlayers = 2;

    public GameState GameState { get; private set; }
    public ActionHandler ActionHandler { get; private set; }

    public Deck ActionsDeck { get; private set; }
    public Deck AnimalsDeck { get; private set; }
    public Deck GoodsDeck { get; private set; }
    public List<ProjectCard> AvailableProjectCards { get; private set; }
    public List<Player> Players = new List<Player>();

    public GameEngine() {
        PrepareDecks();
        PreparePlayers();
        PrepareProjectCards();

        GameState = new GameState(Players, ActionsDeck);
        GameState.StartTurn();

        ActionHandler = new ActionHandler(GameState, AvailableProjectCards);
    }

    void PrepareDecks() {
        ActionsDeck = DeckGenerator.GenerateActionsDeck();
        AnimalsDeck = DeckGenerator.GenerateAnimalsDeck();
        GoodsDeck = DeckGenerator.GenerateGoodsDeck();
    }

    void PreparePlayers() {
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

            Players.Add(new Player("ewa" + i, AnimalsDeck.DrawCard(), GoodsDeck.DrawCard(), startingWorkers));
        }

        //Players[0].Estate.AddProjectToEstate(ActionsDeck.DrawCard());
        //Players[0].Estate.AddProjectToEstate(ActionsDeck.DrawCard());
        //Players[0].Estate.AddProjectToEstate(ActionsDeck.DrawCard());
        //Players[0].Estate.AddProjectToEstate(ActionsDeck.DrawCard());
        Players[0].ProjectArea.Add(ActionsDeck.DrawCard());
        //Players[0].ProjectArea.Add(ActionsDeck.DrawCard());
        //Players[0].ProjectArea.Add(ActionsDeck.DrawCard());
        //Players[0].ProjectArea.Add(ActionsDeck.DrawCard());
    }

    void PrepareProjectCards() {
        AvailableProjectCards = new List<ProjectCard>();

        int howManyProjectCardsPerTurn = 0;

        switch (Players.Count) {
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

            Card topCardFromDeck = ActionsDeck.DrawCard();
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
    }

}