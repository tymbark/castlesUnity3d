using System.Collections.Generic;
using Models;

public static class RandomModels {
    
    public static GameState RandomGameState(List<Player> players,
                                             List<ProjectCard> pc,
                                             List<BonusCard> bl) {
        return new GameState(
                    new System.Random().Next(9999) + "",
                    players,
                    new Deck(RandomList(10)),
                    new Deck(RandomList(20)),
                    new Deck(RandomList(30)),
                    pc,
                    bl,
                    (Round)new System.Random().Next((int)Round.E),
                    "" + new System.Random().Next(100),
                    new System.Random().Next(20),
                    new System.Random().Next(30),
                    new System.Random().Next(40) > 4
                );
    }

    public static Player RandomPlayer(int capacity1, int capacity2, int capacity3 = 200) {
        return new Player(
            RandomList(capacity1),
            RandomList(capacity2),
            RandomList(capacity1),
            RandomList(capacity2),
            RandomList(capacity1),
            RandomList(capacity2),
            RandomList(capacity1),
            RandomBonusList(capacity1),
            "" + new System.Random().Next(capacity3),
            new System.Random().Next(capacity1),
            new System.Random().Next(capacity2),
            new System.Random().Next(capacity1),
            false);
    }

    public static List<Card> RandomList(int capacity) {
        List<Card> result = new List<Card>();
        for (int i = 0; i < capacity; i++) {
            result.Add(RandomCard());
        }
        return result;
    }

    public static Card RandomCard(int classNo = -1) {
        System.Random random = new System.Random();
        CardClass cc;
        if (classNo == -1) {
            cc = (CardClass)random.Next((int)CardClass.ActionCityHall);
        } else {
            cc = (CardClass)classNo;
        }

        CardDice cd = (CardDice)random.Next(10);
        return new Card(cc, cd);
    }

    public static List<BonusCard> RandomBonusList(int capacity) {
        List<BonusCard> result = new List<BonusCard>();
        for (int i = 0; i < capacity; i++) {
            result.Add(RandomBonusCard());
        }
        return result;
    }

    public static BonusCard RandomBonusCard() {
        System.Random random = new System.Random();
        BonusCard bc = (BonusCard)random.Next((int)BonusCard.AllSeven1);
        return bc;
    }

}
