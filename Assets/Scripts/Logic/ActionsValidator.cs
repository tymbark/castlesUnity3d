﻿using System.Collections;
using System.Collections.Generic;
using Models;
using GSP = GameStateProvider;

public static class ActionsValidator {

    public static bool TakeProjectActionAvailable(this Player p, List<ProjectCard> availableProjectCards) {
        return p.AnyActionAvailable()
            && p.ProjectArea.Count < 3
            && p.ReadyToTakeProjectActions(availableProjectCards).Count > 0;
    }

    public static bool TakeFreeSilverProjectActionsAvailable(this Player p) {
        return p.SilverActionAvailable()
            && p.ReadyToTakeFreeSilverProjectActions().Count > 0;
    }

    public static bool BuildProjectActionAvailable(this Player p) {
        return p.AnyActionAvailable()
            && p.ReadyToBuildProjectActions().Count > 0;
    }

    public static bool ShipGoodsActionAvailable(this Player p) {
        return p.AnyActionAvailable()
            && p.ReadyToShipGoodsActions().Count > 0;
    }

    public static bool BuyWorkersActionAvailable(this Player p) {
        return p.AnyActionAvailable();
    }

    public static bool BuySilverActionAvailable(this Player p) {
        return p.AnyActionAvailable();
    }

    public static bool UseSilverActionAvailable(this Player p) {
        return p.SilverCount >= 3 && !p.SilverActionDoneThisTurn;
    }

    public static bool SellWorkersAndSilverActionAvailable(this Player p) {
        return p.AnyActionAvailable()
            && p.WorkersCount + p.SilverCount >= 3;
    }

    public static bool EndTurnActionAvailable(this Player p) {
        return !p.NormalActionAvailable()
            && !p.SilverActionAvailable();
    }

    public static bool NormalActionAvailable(this Player p) {
        bool hasTwoCards = p.Cards.Count == 2;
        bool hasLastCard = p.Cards.Count == 1 && p.FutureCards.Count == 0 && GSP.GameState.CurrentTurn == 6;
        return hasTwoCards || hasLastCard;
    }

    private static bool SilverActionAvailable(this Player p) {
        return p.BonusActionCards.Count > 0;
    }

    private static bool AnyActionAvailable(this Player p) {
        return p.NormalActionAvailable() || p.SilverActionAvailable();
    }

}
