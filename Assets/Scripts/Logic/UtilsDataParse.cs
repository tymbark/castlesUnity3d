using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;

public static class UtilsDataParse {

    public static string Stringify(this Card c) {
        return (int)c.Class + "|" + (int)c.Dice;
    }

    public static Card ParseToCard(this string card) {
        CardClass cardClass = (CardClass)int.Parse(card.Split('|')[0]);
        CardDice cardDice = (CardDice)int.Parse(card.Split('|')[1]);

        return new Card(cardClass, cardDice);
    }

    public static string Stringify(this ProjectCard c) {
        return (int)c.Card.Class + "|" + (int)c.Card.Dice + "|" + (int)c.TakeProjectDice;
    }

    public static ProjectCard ParseToProjectCard(this string card) {
        CardClass cardClass = (CardClass)int.Parse(card.Split('|')[0]);
        CardDice cardDice = (CardDice)int.Parse(card.Split('|')[1]);
        CardDice projectCardDice = (CardDice)int.Parse(card.Split('|')[2]);

        return new ProjectCard(new Card(cardClass, cardDice), projectCardDice);
    }

    public static string Stringify(this List<Card> cards) {
        string output = "";

        for (int i = 0; i < cards.Count; i++) {
            Card c = cards[i];
            output += c.Stringify();
            if (i + 1 != cards.Count) {
                output += ";";
            }
        }

        return output;
    }

    public static List<Card> ParseToCardsList(this string input) {
        List<Card> output = new List<Card>();

        foreach (string item in input.Split(';')) {
            output.Add(item.ParseToCard());
        }

        return output;
    }

    public static string Stringify(this List<ProjectCard> cards) {
        string output = "";

        for (int i = 0; i < cards.Count; i++) {
            ProjectCard c = cards[i];
            output += c.Stringify();
            if (i + 1 != cards.Count) {
                output += ";";
            }
        }

        return output;
    }

    public static List<ProjectCard> ParseToProjectCardList(this string input) {
        List<ProjectCard> output = new List<ProjectCard>();

        foreach (string item in input.Split(';')) {
            output.Add(item.ParseToProjectCard());
        }

        return output;
    }

    public static string Stringify(this Deck deck) {
        string output = "";

        for (int i = 0; i < deck.Cards.Count; i++) {
            Card c = deck.Cards[i];
            output += c.Stringify();
            if (i + 1 != deck.Cards.Count) {
                output += ";";
            }
        }

        return output;
    }

    public static Deck ParseToDeck(this string input) {
        List<Card> output = new List<Card>();

        foreach (string item in input.Split(';')) {
            UnityEngine.Debug.Log(item);
            output.Add(item.ParseToCard());
        }

        return new Deck(output, false);
    }


}

