using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;
using GD = GameDimensions;
using GSP = GameStateProvider;

public class ChooseBonusController : MonoBehaviour {

    private GameObject bonus1;
    private GameObject bonus2;
    private GameObject bonus3;
    private GameObject bonus4;
    private GameObject bonus5;

    private Vector2 posCardCurrentRound = new Vector2(-480f, 200f);
    private Vector2 posCard11;
    private Vector2 posCard12;
    private Vector2 posCard13;
    private Vector2 posCard21;
    private Vector2 posCard22;
    private Vector2 posCard23;
    private Vector2 posCard31;
    private Vector2 posCard32;
    private Vector2 posCard33;
    private Vector2 posCard41;
    private Vector2 posCard42;
    private Vector2 posCard43;
    private Vector2 posCard51;
    private Vector2 posCard52;
    private Vector2 posCard53;

    public System.Action DoneCallback;

    void Start() {
        bonus1 = GameObject.Find("bonus1");
        bonus2 = GameObject.Find("bonus2");
        bonus3 = GameObject.Find("bonus3");
        bonus4 = GameObject.Find("bonus4");
        bonus5 = GameObject.Find("bonus5");

        posCard11 = bonus1.transform.position + new Vector3(-(GD.CardWidth + GD.MarginSmall), 0);
        posCard12 = posCard11 + new Vector2(GD.CardWidth * 0.7f, 0);
        posCard13 = posCard12 + new Vector2(GD.CardWidth * 0.7f, 0);

        posCard21 = bonus2.transform.position + new Vector3(-(GD.CardWidth + GD.MarginSmall), 0);
        posCard22 = posCard21 + new Vector2(GD.CardWidth * 0.7f, 0);
        posCard23 = posCard22 + new Vector2(GD.CardWidth * 0.7f, 0);

        posCard31 = bonus3.transform.position + new Vector3(-(GD.CardWidth + GD.MarginSmall), 0);
        posCard32 = posCard31 + new Vector2(GD.CardWidth * 0.7f, 0);
        posCard33 = posCard32 + new Vector2(GD.CardWidth * 0.7f, 0);

        posCard41 = bonus4.transform.position + new Vector3(-(GD.CardWidth + GD.MarginSmall), 0);
        posCard42 = posCard41 + new Vector2(GD.CardWidth * 0.7f, 0);
        posCard43 = posCard42 + new Vector2(GD.CardWidth * 0.7f, 0);

        posCard51 = bonus5.transform.position + new Vector3(-(GD.CardWidth + GD.MarginSmall), 0);
        posCard52 = posCard51 + new Vector2(GD.CardWidth * 0.7f, 0);
        posCard53 = posCard52 + new Vector2(GD.CardWidth * 0.7f, 0);

        DrawBonusCards();

        bonus1.GetComponent<ClickActionScript>()
              .ClickMethod = obj => { ChooseBonus(1); };

        bonus2.GetComponent<ClickActionScript>()
              .ClickMethod = obj => { ChooseBonus(2); };

        bonus3.GetComponent<ClickActionScript>()
              .ClickMethod = obj => { ChooseBonus(3); };

        bonus4.GetComponent<ClickActionScript>()
              .ClickMethod = obj => { ChooseBonus(4); };

        bonus5.GetComponent<ClickActionScript>()
              .ClickMethod = obj => { ChooseBonus(5); };
    }

    private void DrawBonusCards() {

        DrawCard(posCardCurrentRound, GSP.GameState.CurrentRound);

        switch (GSP.GameState.CurrentRound) {
            case Round.A:
                DrawCard(posCard11, new Card(CardClass.Barrel), bonus1);
                DrawCard(posCard12, new Card(CardClass.Barrel), bonus1);
                DrawCard(posCard13, new Card(CardClass.Barrel), bonus1);

                if (GSP.GameState.AnimalsDeck.Cards.Count > 0)
                    DrawCard(posCard21, GSP.GameState.AnimalsDeck.Cards[0], bonus2);
                if (GSP.GameState.AnimalsDeck.Cards.Count > 1)
                    DrawCard(posCard22, GSP.GameState.AnimalsDeck.Cards[1], bonus2);

                if (GSP.GameState.GoodsDeck.Cards.Count > 0)
                    DrawCard(posCard31, GSP.GameState.GoodsDeck.Cards[0], bonus3);
                if (GSP.GameState.GoodsDeck.Cards.Count > 1)
                    DrawCard(posCard32, GSP.GameState.GoodsDeck.Cards[1], bonus3);

                DrawCard(posCard41, new Card(CardClass.Worker), bonus4);
                DrawCard(posCard42, new Card(CardClass.Worker), bonus4);
                DrawCard(posCard43, new Card(CardClass.Worker), bonus4);

                DrawCard(posCard51, new Card(CardClass.Silver), bonus5);
                DrawCard(posCard52, new Card(CardClass.Silver), bonus5);
                DrawCard(posCard53, new Card(CardClass.Silver), bonus5);
                break;
            case Round.B:
                if (GSP.GameState.AnimalsDeck.Cards.Count > 0)
                    DrawCard(posCard11, GSP.GameState.AnimalsDeck.Cards[0], bonus2);
                if (GSP.GameState.AnimalsDeck.Cards.Count > 1)
                    DrawCard(posCard12, GSP.GameState.AnimalsDeck.Cards[1], bonus2);

                if (GSP.GameState.GoodsDeck.Cards.Count > 0)
                    DrawCard(posCard21, GSP.GameState.GoodsDeck.Cards[0], bonus3);
                if (GSP.GameState.GoodsDeck.Cards.Count > 1)
                    DrawCard(posCard22, GSP.GameState.GoodsDeck.Cards[1], bonus3);

                DrawCard(posCard41, new Card(CardClass.Worker), bonus4);
                DrawCard(posCard42, new Card(CardClass.Worker), bonus4);
                DrawCard(posCard43, new Card(CardClass.Worker), bonus4);

                DrawCard(posCard51, new Card(CardClass.Silver), bonus5);
                DrawCard(posCard52, new Card(CardClass.Silver), bonus5);
                DrawCard(posCard53, new Card(CardClass.Silver), bonus5);
                break;
            case Round.C:
                if (GSP.GameState.AnimalsDeck.Cards.Count > 0)
                    DrawCard(posCard12, GSP.GameState.AnimalsDeck.Cards[0], bonus2);

                if (GSP.GameState.GoodsDeck.Cards.Count > 0)
                    DrawCard(posCard22, GSP.GameState.GoodsDeck.Cards[0], bonus3);

                DrawCard(posCard31, new Card(CardClass.Worker), bonus4);
                DrawCard(posCard32, new Card(CardClass.Worker), bonus4);

                DrawCard(posCard41, new Card(CardClass.Silver), bonus4);
                DrawCard(posCard42, new Card(CardClass.Silver), bonus4);

                DrawCard(posCard51, new Card(CardClass.Worker), bonus5);
                DrawCard(posCard52, new Card(CardClass.Silver), bonus5);
                break;
            case Round.D:
                DrawCard(posCard11, new Card(CardClass.Worker), bonus4);
                DrawCard(posCard12, new Card(CardClass.Worker), bonus4);

                DrawCard(posCard21, new Card(CardClass.Silver), bonus4);
                DrawCard(posCard22, new Card(CardClass.Silver), bonus4);

                DrawCard(posCard41, new Card(CardClass.Worker), bonus5);
                DrawCard(posCard42, new Card(CardClass.Silver), bonus5);
                break;
            case Round.E:
                DrawCard(posCard12, new Card(CardClass.Worker), bonus4);

                DrawCard(posCard22, new Card(CardClass.Silver), bonus4);
                break;
        }
    }

    private void ChooseBonus(int number) {
        bool bonusAquired = true;

        switch (GSP.GameState.CurrentRound) {
            case Round.A:
                switch (number) {
                    case 1:
                        GSP.GameState.CurrentPlayer.Score = GSP.GameState.CurrentPlayer.Score + 3;
                        break;
                    case 2:
                        GSP.GameState.CurrentPlayer.Animals.Add(GSP.GameState.AnimalsDeck.DrawCard());
                        GSP.GameState.CurrentPlayer.Animals.Add(GSP.GameState.AnimalsDeck.DrawCard());
                        break;
                    case 3:
                        GSP.GameState.CurrentPlayer.Goods.Add(GSP.GameState.GoodsDeck.DrawCard());
                        GSP.GameState.CurrentPlayer.Goods.Add(GSP.GameState.GoodsDeck.DrawCard());
                        break;
                    case 4:
                        GSP.GameState.CurrentPlayer.WorkersCount = GSP.GameState.CurrentPlayer.WorkersCount + 3;
                        break;
                    case 5:
                        GSP.GameState.CurrentPlayer.SilverCount = GSP.GameState.CurrentPlayer.SilverCount + 3;
                        break;
                    default:
                        bonusAquired = false;
                        break;
                }
                break;
            case Round.B:
                switch (number) {
                    case 1:
                        GSP.GameState.CurrentPlayer.Animals.Add(GSP.GameState.AnimalsDeck.DrawCard());
                        GSP.GameState.CurrentPlayer.Animals.Add(GSP.GameState.AnimalsDeck.DrawCard());
                        break;
                    case 2:
                        GSP.GameState.CurrentPlayer.Goods.Add(GSP.GameState.GoodsDeck.DrawCard());
                        GSP.GameState.CurrentPlayer.Goods.Add(GSP.GameState.GoodsDeck.DrawCard());
                        break;
                    case 4:
                        GSP.GameState.CurrentPlayer.WorkersCount = GSP.GameState.CurrentPlayer.WorkersCount + 3;
                        break;
                    case 5:
                        GSP.GameState.CurrentPlayer.SilverCount = GSP.GameState.CurrentPlayer.SilverCount + 3;
                        break;
                    default:
                        bonusAquired = false;
                        break;
                }
                break;
            case Round.C:
                switch (number) {
                    case 1:
                        GSP.GameState.CurrentPlayer.Animals.Add(GSP.GameState.AnimalsDeck.DrawCard());
                        break;
                    case 2:
                        GSP.GameState.CurrentPlayer.Goods.Add(GSP.GameState.GoodsDeck.DrawCard());
                        break;
                    case 3:
                        GSP.GameState.CurrentPlayer.WorkersCount = GSP.GameState.CurrentPlayer.WorkersCount + 2;
                        break;
                    case 4:
                        GSP.GameState.CurrentPlayer.SilverCount = GSP.GameState.CurrentPlayer.SilverCount + 2;
                        break;
                    case 5:
                        GSP.GameState.CurrentPlayer.WorkersCount = GSP.GameState.CurrentPlayer.WorkersCount + 1;
                        GSP.GameState.CurrentPlayer.SilverCount = GSP.GameState.CurrentPlayer.SilverCount + 1;
                        break;
                    default:
                        bonusAquired = false;
                        break;
                }
                break;
            case Round.D:
                switch (number) {
                    case 1:
                        GSP.GameState.CurrentPlayer.WorkersCount = GSP.GameState.CurrentPlayer.WorkersCount + 2;
                        break;
                    case 2:
                        GSP.GameState.CurrentPlayer.SilverCount = GSP.GameState.CurrentPlayer.SilverCount + 2;
                        break;
                    case 4:
                        GSP.GameState.CurrentPlayer.WorkersCount = GSP.GameState.CurrentPlayer.WorkersCount + 1;
                        GSP.GameState.CurrentPlayer.SilverCount = GSP.GameState.CurrentPlayer.SilverCount + 1;
                        break;
                    default:
                        bonusAquired = false;
                        break;
                }
                break;
            case Round.E:
                switch (number) {
                    case 1:
                        GSP.GameState.CurrentPlayer.WorkersCount = GSP.GameState.CurrentPlayer.WorkersCount + 1;
                        break;
                    case 2:
                        GSP.GameState.CurrentPlayer.SilverCount = GSP.GameState.CurrentPlayer.SilverCount + 1;
                        break;
                    default:
                        bonusAquired = false;
                        break;
                }
                break;
        }

        if (bonusAquired) {
            DoneCallback();
            //GSP.GameState.SaveGameState();
        }
    }

    private static void DrawCard(Vector2 position, Card card, GameObject parent) {
        Object obj = Resources.Load("Prefabs/Card");
        GameObject prefab = Instantiate(obj) as GameObject;

        prefab.transform.SetParent(parent.transform);

        Image image = prefab.GetComponent<Image>();
        image.overrideSprite = CardsGenerator.GetSpriteForCard(card);

        prefab.transform.position = new Vector3(position.x, position.y, 0);
    }

    private static void DrawCard(Vector2 position, Round round) {
        Object obj = Resources.Load("Prefabs/Card");
        GameObject prefab = Instantiate(obj) as GameObject;

        prefab.transform.SetParent(GameObject.Find("Canvas").transform);

        Image image = prefab.GetComponent<Image>();
        image.overrideSprite = CardsGenerator.GetSpriteForCurrentRoundBonus(round);

        prefab.transform.position = new Vector3(position.x, position.y, 0);
    }

}
