using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;
using NetworkModels;

public static class UtilsDataParse {

    private static readonly char CardSeparator = ';';

    public static string Stringify(this Card c) {
        return "" + (int)c.Class + CardSeparator
                  + (int)c.Dice + CardSeparator
                  + c.Number + CardSeparator
                  + c.TripleId + CardSeparator;
    }

    public static Card ParseToCard(this string card) {
        CardClass cardClass = (CardClass)int.Parse(card.Split(CardSeparator)[0]);
        CardDice cardDice = (CardDice)int.Parse(card.Split(CardSeparator)[1]);
        int cardNumber = int.Parse(card.Split(CardSeparator)[2]);
        int tripleId = int.Parse(card.Split(CardSeparator)[3]);

        return new Card(cardClass, cardDice, cardNumber, tripleId);
    }

    public static string Stringify<T>(this List<T> listToParse) {
        T[] array = listToParse.ToArray();
        var wrapper = new Wrapper<T[]>(array);

        return JsonUtility.ToJson(wrapper);
    }

    public static List<T> ParseToList<T>(this string input) {
        Wrapper<T[]> wrapper = JsonUtility.FromJson<Wrapper<T[]>>(input);
        List<T> newList = new List<T>();

        foreach (T item in wrapper.item) {
            newList.Add(item);
        }

        return newList;
    }

    private class Wrapper<T> {
        public Wrapper(T item) {
            this.item = item;
        }
        public T item;
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
        string bonusActionCards = player.BonusActionCards.Stringify();
        string completedProjects = player.CompletedProjects.Stringify();
        string receivedBonuses = player.ReceivedBonuses.Stringify();

        var sp = new SerializedPlayer(cards,
                                      futureCards,
                                      animals,
                                      goods,
                                      projectArea,
                                      bonusActionCards,
                                      completedProjects,
                                      receivedBonuses,
                                      player.Name,
                                      player.Score,
                                      player.WorkersCount,
                                      player.SilverCount,
                                      player.SilverActionDoneThisTurn);

        return JsonUtility.ToJson(sp);
    }

    public static Player ParseToPlayer(this string input) {
        var sp = JsonUtility.FromJson<SerializedPlayer>(input);

        List<Card> cards = sp.Cards.ParseToCardsList();
        List<Card> futureCards = sp.FutureCards.ParseToCardsList();
        List<Card> animals = sp.Animals.ParseToCardsList();
        List<Card> goods = sp.Goods.ParseToCardsList();
        List<Card> projectArea = sp.ProjectArea.ParseToCardsList();
        List<Card> bonusCards = sp.BonusCards.ParseToCardsList();
        List<Card> completed = sp.Completed.ParseToCardsList();
        List<BonusCard> bonusesReceived = sp.Bonuses.ParseToList<BonusCard>();

        return new Player(cards,
                          futureCards,
                          animals,
                          goods,
                          projectArea,
                          bonusCards,
                          completed,
                          bonusesReceived,
                          sp.Name,
                          sp.Score,
                          sp.WorkersCount,
                          sp.SilverCount,
                          sp.SilverDone);
    }

    private class SerializedPlayer {
        public string Name;

        public string Cards;
        public string FutureCards;
        public string Animals;
        public string Goods;
        public string ProjectArea;
        public string BonusCards;
        public string Completed;

        public string Bonuses;

        public int Score;
        public int WorkersCount;
        public int SilverCount;
        public bool SilverDone;

        public SerializedPlayer(string cards,
                                string futureCards,
                                string animals,
                                string goods,
                                string projectArea,
                                string bonusCards,
                                string completedProjects,
                                string bonuses,
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
            BonusCards = bonusCards;
            Completed = completedProjects;
            Score = score;
            WorkersCount = workersCount;
            SilverCount = silverCount;
            SilverDone = silverDoneThisTurn;
            Bonuses = bonuses;
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

        string mainDeck = gameState.MainDeck.Stringify();
        string animals = gameState.AnimalsDeck.Stringify();
        string goods = gameState.GoodsDeck.Stringify();
        string projects = gameState.AvailableProjectCards.Stringify();
        string bonuses = gameState.AvailableBonusCards.Stringify();

        var serializedGameState = new SerializedGameState(
            gameState.Id,
            player1,
            player2,
            player3,
            player4,
            mainDeck,
            animals,
            goods,
            projects,
            bonuses,
            gameState.CurrentRound,
            gameState.CurrentPlayerIndex,
            gameState.CurrentTurn,
            gameState.HowManyPlayers,
            gameState.IsFinished);


        return JsonUtility.ToJson(serializedGameState);
    }

    public static GameState ParseToGameState(this string input) {

        SerializedGameState sgs = JsonUtility.FromJson<SerializedGameState>(input);
        List<Player> players = new List<Player>();

        if (sgs.Player1 != "") {
            players.Add(sgs.Player1.ParseToPlayer());
        }

        if (sgs.Player2 != "") {
            players.Add(sgs.Player2.ParseToPlayer());
        }

        if (sgs.Player3 != "") {
            players.Add(sgs.Player3.ParseToPlayer());
        }

        if (sgs.Player4 != "") {
            players.Add(sgs.Player4.ParseToPlayer());
        }

        Deck mainDeck = sgs.MainDeck.ParseToDeck();
        Deck animalsDeck = sgs.AnimalsDeck.ParseToDeck();
        Deck goodsDeck = sgs.GoodsDeck.ParseToDeck();
        List<ProjectCard> availableProjectCards = sgs.AvailableProjectCards.ParseToProjectCardList();
        List<BonusCard> availableBonusCards = sgs.AvailableBonusCards.ParseToList<BonusCard>();

        return new GameState(sgs.Id,
                             players,
                             mainDeck,
                             animalsDeck,
                             goodsDeck,
                             availableProjectCards,
                             availableBonusCards,
                             sgs.CurrentRound,
                             sgs.CurrentPlayerIndex,
                             sgs.CurrentTurn,
                             sgs.HowManyPlayers,
                             sgs.IsFinished);
    }

    private class SerializedGameState {

        public string Id;
        public string Player1;
        public string Player2;
        public string Player3;
        public string Player4;
        public Round CurrentRound;
        public int CurrentPlayerIndex;
        public int CurrentTurn;
        public int HowManyPlayers;
        public bool IsFinished;

        public string MainDeck;
        public string AnimalsDeck;
        public string GoodsDeck;
        public string AvailableProjectCards;
        public string AvailableBonusCards;

        public SerializedGameState(string id,
                                   string player1,
                                   string player2,
                                   string player3,
                                   string player4,
                                   string mainDeck,
                                   string animalsDeck,
                                   string goodsDeck,
                                   string availableProjectCards,
                                   string availableBonusCards,
                                   Round currentRound,
                                   int currentPlayerIndex,
                                   int currentTurn,
                                   int howManyPlayers,
                                   bool isFinished) {
            this.Id = id;
            this.Player1 = player1;
            this.Player2 = player2;
            this.Player3 = player3;
            this.Player4 = player4;
            this.MainDeck = mainDeck;
            this.AnimalsDeck = animalsDeck;
            this.GoodsDeck = goodsDeck;
            this.AvailableProjectCards = availableProjectCards;
            this.AvailableBonusCards = availableBonusCards;
            this.HowManyPlayers = howManyPlayers;
            this.CurrentRound = currentRound;
            this.CurrentPlayerIndex = currentPlayerIndex;
            this.CurrentTurn = currentTurn;
            this.IsFinished = isFinished;
        }

    }

    private class GameWrapper {
        public string id = "";
        public bool available = false;
        public string creator_name = "";
        public int players_max = 0;
        public int players_now = 0;
        public string[] players = { };

        public Game ParseToGame() {
            List<string> playersList = new List<string>();
            foreach (string s in players) {
                playersList.Add(s);
            }
            return new Game(id, available, creator_name, players_max, players_now, playersList);
        }
    }

    public static List<Game> ParseToListOfGames(this string jsonData) {
        List<Game> result = new List<Game>();

        string json = jsonData
            .Replace(" ", "")
            .Replace("\n", "")
            .Replace("{\"games\":[", "")
            .Replace("}]}", "}")
            .Replace("},{", "}###{");

        foreach (var item in json.Split(new string[] { "###" }, System.StringSplitOptions.RemoveEmptyEntries)) {
            GameWrapper gameWrapper = JsonUtility.FromJson<GameWrapper>(item);
            Game game = gameWrapper.ParseToGame();
            result.Add(game);
        }

        return result;
    }

}

