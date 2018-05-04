using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;


public static class GameDimensions {

    public static readonly Vector2 CardProportions = new Vector2(2.0f, 3.0f);
    public static readonly float CardScale = 75f;
    public static readonly float CardSpaceRemainVisibleX = 0.8f;
    public static readonly float CardSpaceRemainVisibleY = 0.5f;
    public static readonly float CardWidth = CardProportions.x * CardScale;
    public static readonly float CardHeight = CardProportions.y * CardScale;
    public static readonly Vector2 CardSize = CardProportions * CardScale;

    public static readonly float MarginSmall = 8f;
    public static readonly float MarginBig = 24f;

    public static readonly Vector2 ScreenResolution = new Vector2(1920f, 1080f);
    public static readonly Vector2 ScreenTopRight = new Vector2(960f, 540f);
    public static readonly Vector2 ScreenTopLeft = new Vector2(-960f, 540f);
    public static readonly Vector2 ScreenTopCenter = new Vector2(0, 540f);
    public static readonly Vector2 ScreenBottomRight = new Vector2(960f, -540f);
    public static readonly Vector2 ScreenBottomLeft = new Vector2(-960f, -540f);
    public static readonly Vector2 ScreenBottomCenter = new Vector2(0, -540f);

    public static readonly Vector2 CardsSpace = new Vector2((ScreenResolution.x - (4 * MarginBig)) / 3, CardHeight);
    public static readonly Vector2 PositionProjectDiceI = ScreenTopLeft + new Vector2(MarginBig + CardWidth / 2, -(MarginBig + CardHeight / 2));
    public static readonly Vector2 PositionProjectDiceII = PositionProjectDiceI + new Vector2(0, -(CardHeight + MarginBig));
    public static readonly Vector2 PositionProjectDiceIII = ScreenTopLeft + new Vector2(2 * MarginBig + CardWidth / 2 + CardsSpace.x, -(MarginBig + CardHeight / 2));
    public static readonly Vector2 PositionProjectDiceIV = PositionProjectDiceIII + new Vector2(0, -(CardHeight + MarginBig));
    public static readonly Vector2 PositionProjectDiceV = ScreenTopLeft + new Vector2(3 * MarginBig + CardWidth / 2 + 2 * CardsSpace.x, -(MarginBig + CardHeight / 2));
    public static readonly Vector2 PositionProjectDiceVI = PositionProjectDiceV + new Vector2(0, -(CardHeight + MarginBig));

    public static Vector2 PositionForActionCard(Vector2 startingPoint, float index, float howManyInARow) {

        float spaceForActionCards = CardsSpace.x - (CardWidth + MarginSmall);

        if ((spaceForActionCards / howManyInARow) > CardWidth) {
            return startingPoint + new Vector2((CardWidth + MarginSmall) * (index + 1), 0);
        } else {
            float factor = ((1 + (2 * index)) / (2 * howManyInARow));
            float offsetDueToIndex = spaceForActionCards * factor;
            float offsetDueToDiceCard = (CardWidth / 2) + MarginSmall;

            return startingPoint + new Vector2(offsetDueToDiceCard + offsetDueToIndex, 0);
        }

    }

    public static readonly Vector2 PositionAnimalToTakeCard1 = ScreenBottomLeft + new Vector2(CardWidth / 2 + MarginBig, CardHeight / 2 + MarginBig);
    public static readonly Vector2 PositionAnimalToTakeCard2 = PositionAnimalToTakeCard1 + new Vector2(CardWidth * 0.8f, 0);

    public static readonly Vector2 PositionGoodsToTakeCard1 = ScreenBottomLeft + new Vector2(CardWidth / 2 + MarginBig, CardHeight / 2 + CardHeight + MarginBig + MarginSmall);
    public static readonly Vector2 PositionGoodsToTakeCard2 = PositionGoodsToTakeCard1 + new Vector2(CardWidth * 0.8f, 0);

    public static readonly Vector2 PositionAllEstatesCard = new Vector2(PositionAnimalToTakeCard2.x + CardWidth / 2 + CardHeight / 2 + MarginBig, ScreenBottomLeft.y + CardWidth / 2 + MarginBig);
    public static readonly Vector2 PositionAllProjectsCard = PositionAllEstatesCard + new Vector2(0, CardWidth + MarginSmall);
    public static readonly Vector2 PositionAllStoragesCard = PositionAllProjectsCard + new Vector2(0, CardWidth + MarginSmall);

    public static readonly Vector2 PositionCurrentBonusCard = new Vector2(PositionAllEstatesCard.x + CardHeight / 2 + CardWidth / 2 + MarginBig, ScreenBottomLeft.y + MarginBig + CardHeight / 2);
    public static readonly Vector2 PositionAllAnimalsCard = PositionCurrentBonusCard + new Vector2(0, MarginSmall + CardHeight);
    public static readonly Vector2 PositionSilverCard = PositionAllAnimalsCard + new Vector2(MarginSmall + CardWidth, 0);
    public static readonly Vector2 PositionWorkerCard = PositionSilverCard + new Vector2(MarginSmall + CardWidth, 0);
    public static readonly Vector2 PositionShipGoodsCard = PositionSilverCard + new Vector2(0, -(MarginSmall + CardHeight));
    public static readonly Vector2 PositionSellSilverAndWorkersCard = PositionWorkerCard + new Vector2(0, -(MarginSmall + CardHeight));

    public static readonly Vector2 PositionEndTurnButton = ScreenBottomRight + new Vector2(-(MarginBig + CardWidth / 2), MarginBig + CardWidth / 2);
    public static readonly Vector2 PositionPointsButton = PositionEndTurnButton + new Vector2(0, MarginBig + CardWidth);
    public static readonly Vector2 PositionExitButton = PositionPointsButton + new Vector2(0, MarginBig + CardWidth);

    public static readonly Vector2 PositionCardProjectsBigCard = new Vector2((PositionPointsButton.x + PositionWorkerCard.x) / 2, ScreenBottomCenter.y + MarginBig + CardWidth);
    public static readonly Vector2 PositionHandCard1 = PositionCardProjectsBigCard + new Vector2(-CardHeight / 2, CardWidth * 1.3f);
    public static readonly Vector2 PositionHandCard2 = PositionCardProjectsBigCard + new Vector2(CardHeight / 2, CardWidth * 1.3f);

    public static readonly Vector2 PositionProjectCard1 = PositionCardProjectsBigCard + new Vector2(-PositionCardProjectsBigCard.x * 1.8f / 6, 0);
    public static readonly Vector2 PositionProjectCard2 = PositionCardProjectsBigCard;
    public static readonly Vector2 PositionProjectCard3 = PositionCardProjectsBigCard + new Vector2(PositionCardProjectsBigCard.x * 1.8f / 6, 0);

}

