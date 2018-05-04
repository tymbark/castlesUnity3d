using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;

public static class UtilsDataParse {

    private static readonly char CardSeparator = ';';

    public static string Stringify(this Card c) {
        return "" + (int)c.Class + CardSeparator + (int)c.Dice;
    }

    public static Card ParseToCard(this string card) {
        CardClass cardClass = (CardClass)int.Parse(card.Split(CardSeparator)[0]);
        CardDice cardDice = (CardDice)int.Parse(card.Split(CardSeparator)[1]);

        return new Card(cardClass, cardDice);
    }

    public static string Stringify(this ProjectCard c) {
        return "" + (int)c.Card.Class + CardSeparator + (int)c.Card.Dice + CardSeparator + (int)c.TakeProjectDice;
    }

    public static ProjectCard ParseToProjectCard(this string card) {
        CardClass cardClass = (CardClass)int.Parse(card.Split(CardSeparator)[0]);
        CardDice cardDice = (CardDice)int.Parse(card.Split(CardSeparator)[1]);
        CardDice projectCardDice = (CardDice)int.Parse(card.Split(CardSeparator)[2]);

        return new ProjectCard(new Card(cardClass, cardDice), projectCardDice);
    }

    public static string Stringify(this List<Card> cards) {
        return JsonUtility.ToJson(new CardsWrapper(cards));
    }

    public static List<Card> ParseToCardsList(this string input) {
        CardsWrapper cardsWrapper = JsonUtility.FromJson<CardsWrapper>(input);
        List<Card> output = new List<Card>();

        foreach (string card in cardsWrapper.cards) {
            output.Add(card.ParseToCard());
        }

        return output;
    }

    private class CardsWrapper {
        public string[] cards;
        public CardsWrapper(List<Card> cardsList) {
            this.cards = cardsList.ConvertAll((Card input) => input.Stringify()).ToArray(); ;
        }
    }

    public static string Stringify(this List<ProjectCard> cards) {
        return JsonUtility.ToJson(new ProjectCardsWrapper(cards));
    }

    public static List<ProjectCard> ParseToProjectCardList(this string input) {
        ProjectCardsWrapper wrapper = JsonUtility.FromJson<ProjectCardsWrapper>(input);
        List<ProjectCard> output = new List<ProjectCard>();

        foreach (string card in wrapper.cards) {
            output.Add(card.ParseToProjectCard());
        }

        return output;
    }

    private class ProjectCardsWrapper {
        public string[] cards;
        public ProjectCardsWrapper(List<ProjectCard> cardsList) {
            this.cards = cardsList.ConvertAll((ProjectCard input) => input.Stringify()).ToArray(); ;
        }
    }

    public static string Stringify(this Deck deck) {
        return deck.Cards.Stringify();
    }

    public static Deck ParseToDeck(this string input) {
        List<Card> output = input.ParseToCardsList();
        return new Deck(output, false);
    }

    public static string Stringify(this Player player) {
        string cards = player.Cards.Stringify();
        string futureCards = player.FutureCards.Stringify();
        string animals = player.Animals.Stringify();
        string goods = player.Goods.Stringify();
        string projectArea = player.ProjectArea.Stringify();
        string silverActionCards = player.SilverActionCards.Stringify();
        string completedProjects = player.CompletedProjects.Stringify();

        var sp = new SerializedPlayer(cards, futureCards, animals, goods,
                                      projectArea, silverActionCards, completedProjects,
                                      player.Name, player.Score, player.WorkersCount,
                                      player.SilverCount, player.SilverActionDoneThisTurn);

        return JsonUtility.ToJson(sp);
    }

    public static Player ParseToPlayer(this string input) {
        var sp = JsonUtility.FromJson<SerializedPlayer>(input);

        List<Card> cards = sp.Cards.ParseToCardsList();
        List<Card> futureCards = sp.FutureCards.ParseToCardsList();
        List<Card> animals = sp.Animals.ParseToCardsList();
        List<Card> goods = sp.Goods.ParseToCardsList();
        List<Card> projectArea = sp.ProjectArea.ParseToCardsList();
        List<Card> silverActionCards = sp.SilverCards.ParseToCardsList();
        List<Card> completedProjects = sp.Completed.ParseToCardsList();

        return new Player(cards, futureCards, animals, goods, projectArea,
                          silverActionCards, completedProjects, sp.Name,
                          sp.Score, sp.WorkersCount, sp.SilverCount,
                          sp.SilverDone);
    }

    private class SerializedPlayer {
        public string Name;

        public string Cards;
        public string FutureCards;
        public string Animals;
        public string Goods;
        public string ProjectArea;
        public string SilverCards;
        public string Completed;

        public int Score;
        public int WorkersCount;
        public int SilverCount;
        public bool SilverDone;

        public SerializedPlayer(string cards,
                                string futureCards,
                                string animals,
                                string goods,
                                string projectArea,
                                string silverActionCards,
                                string completedProjects,
                                string name,
                                int score,
                                int workersCount,
                                int silverCount,
                                bool silverDoneThisTurn) {
            Name = name;
            Cards = cards;
            FutureCards = futureCards;
            Animals = animals;
            Goods = goods;
            ProjectArea = projectArea;
            SilverCards = silverActionCards;
            Completed = completedProjects;
            Score = score;
            WorkersCount = workersCount;
            SilverCount = silverCount;
            SilverDone = silverDoneThisTurn;
        }

    }

    public static string Stringify(this GameState gameState) {
        string player1 = "";
        string player2 = "";
        string player3 = "";
        string player4 = "";

        if (gameState.Players.Count > 0) {
            player1 = gameState.Players[0].Stringify();
        }

        if (gameState.Players.Count > 1) {
            player2 = gameState.Players[1].Stringify();
        }

        if (gameState.Players.Count > 2) {
            player3 = gameState.Players[2].Stringify();
        }

        if (gameState.Players.Count > 3) {
            player4 = gameState.Players[3].Stringify();
        }

        string currentPlayer = gameState.CurrentPlayer.Stringify();
        string mainDeck = gameState.MainDeck.Stringify();
        string animals = gameState.AnimalsDeck.Stringify();
        string goods = gameState.GoodsDeck.Stringify();
        string projects = gameState.AvailableProjectCards.Stringify();

        var serializedGameState = new SerializedGameState(
            player1, player2, player3, player4, mainDeck, animals, goods, projects,
            gameState.CurrentRound, currentPlayer, gameState.HowManyPlayers);

        return JsonUtility.ToJson(serializedGameState);
    }

    public static GameState ParseToGameState(this string input) {

        SerializedGameState sgs = JsonUtility.FromJson<SerializedGameState>(input);
        List<Player> players = new List<Player>();

        if (sgs.Player1 != "") {
            UnityEngine.Debug.Log("player1 :  " + sgs.Player1);
            players.Add(sgs.Player1.ParseToPlayer());
        }

        if (sgs.Player2 != "") {
            UnityEngine.Debug.Log("player2 :  " + sgs.Player2);
            players.Add(sgs.Player2.ParseToPlayer());
        }

        if (sgs.Player3 != "") {
            UnityEngine.Debug.Log("player3 :  " + sgs.Player3);
            players.Add(sgs.Player3.ParseToPlayer());
        }

        if (sgs.Player4 != "") {
            UnityEngine.Debug.Log("player4 :  " + sgs.Player4);
            players.Add(sgs.Player4.ParseToPlayer());
        }

        UnityEngine.Debug.Log("current player :  " + sgs.CurrentPlayer);

        Player currentPlayer = sgs.CurrentPlayer.ParseToPlayer();
        Deck mainDeck = sgs.MainDeck.ParseToDeck();
        Deck animalsDeck = sgs.AnimalsDeck.ParseToDeck();
        Deck goodsDeck = sgs.GoodsDeck.ParseToDeck();
        List<ProjectCard> availableProjectCards = sgs.AvailableProjectCards.ParseToProjectCardList();

        return new GameState(players, mainDeck, animalsDeck, goodsDeck,
                             availableProjectCards, sgs.CurrentRound,
                             currentPlayer, sgs.HowManyPlayers);
    }

    private class SerializedGameState {

        public string Player1;
        public string Player2;
        public string Player3;
        public string Player4;
        public int HowManyPlayers;
        public Round CurrentRound;
        public string CurrentPlayer;

        public string MainDeck;
        public string AnimalsDeck;
        public string GoodsDeck;
        public string AvailableProjectCards;

        public SerializedGameState(string player1, string player2, string player3,
                                   string player4, string mainDeck, string animalsDeck,
                                   string goodsDeck, string availableProjectCards,
                                   Round currentRound, string currentPlayer, int howManyPlayers) {
            this.Player1 = player1;
            this.Player2 = player2;
            this.Player3 = player3;
            this.Player4 = player4;
            this.MainDeck = mainDeck;
            this.AnimalsDeck = animalsDeck;
            this.GoodsDeck = goodsDeck;
            this.AvailableProjectCards = availableProjectCards;
            this.HowManyPlayers = howManyPlayers;
            this.CurrentRound = currentRound;
            this.CurrentPlayer = currentPlayer;
        }

    }

}

