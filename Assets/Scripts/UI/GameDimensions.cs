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

    public static readonly float marginSmall = 8f;
    public static readonly float marginBig = 16f;

    public static readonly Vector2 ScreenResolution = new Vector2(1920f, 1080f);
    public static readonly Vector2 ScreenTopRight = new Vector2(960f, 540f);
    public static readonly Vector2 ScreenTopLeft = new Vector2(-960f, 540f);
    public static readonly Vector2 ScreenTopCenter = new Vector2(0, 540f);
    public static readonly Vector2 ScreenBottomRight = new Vector2(960f, -540f);
    public static readonly Vector2 ScreenBottomLeft = new Vector2(-960f, -540f);
    public static readonly Vector2 ScreenBottomCenter = new Vector2(0, -540f);

    public static readonly Vector2 PositionHandCard2 =
        ScreenBottomRight + new Vector2(-CardWidth, CardWidth);
    public static readonly Vector2 PositionHandCard1 =
        ScreenBottomRight + new Vector2(-2 * CardWidth, CardWidth);

    public static readonly Vector2 CardsSpace = new Vector2((ScreenResolution.x - (4 * marginBig)) / 3, CardHeight);
    public static readonly Vector2 PositionProjectDiceI = ScreenTopLeft + new Vector2(marginBig + CardWidth / 2, -(marginBig + CardHeight / 2));
    public static readonly Vector2 PositionProjectDiceII = PositionProjectDiceI + new Vector2(0, -(CardHeight + marginBig));
    public static readonly Vector2 PositionProjectDiceIII = PositionProjectDiceI + new Vector2(CardsSpace.x + marginBig + CardWidth / 2, 0);
    public static readonly Vector2 PositionProjectDiceIV = PositionProjectDiceIII + new Vector2(0, -(CardHeight + marginBig));
    public static readonly Vector2 PositionProjectDiceV = PositionProjectDiceIII + new Vector2(CardsSpace.x + marginBig + CardWidth / 2, 0);
    public static readonly Vector2 PositionProjectDiceVI = PositionProjectDiceV + new Vector2(0, -(CardHeight + marginBig));

    public static Vector2 PositionForActionCard(Vector2 startingPoint, float index, float howManyInARow) {

        float spaceForActionCards = CardsSpace.x - (CardWidth + marginSmall);

        if ((spaceForActionCards / howManyInARow) > CardWidth) {
            return startingPoint + new Vector2((CardWidth + marginSmall) * (index + 1), 0);
        } else {
            float factor = ((1 + (2 * index)) / (2 * howManyInARow));
            float offsetDueToIndex = spaceForActionCards * factor;
            float offsetDueToDiceCard = (CardWidth / 2) + marginSmall;

            return startingPoint + new Vector2(offsetDueToDiceCard + offsetDueToIndex, 0);
        }

    }

    //public static readonly Vector2 PositionProjectDiceI = ScreenTopLeft
    //+ new Vector2(marginBig + CardWidth / 2, -(marginBig + CardHeight / 2));
    public static readonly Vector2 PositionProjectDiceI1 = PositionProjectDiceI
        + new Vector2(marginSmall + CardWidth, 0);
    public static readonly Vector2 PositionProjectDiceI2 = PositionProjectDiceI1
        + new Vector2(marginSmall + CardWidth, 0);
    public static readonly Vector2 PositionProjectDiceI3 = PositionProjectDiceI2
        + new Vector2(marginSmall + CardWidth, 0);
    public static readonly Vector2 PositionProjectDiceI4 = PositionProjectDiceI3
        + new Vector2(marginSmall + CardWidth, 0);

    //public static readonly Vector2 PositionProjectDiceII = ScreenTopLeft
    //+ new Vector2(marginBig + CardWidth / 2, -(2 * marginBig + CardHeight + CardHeight / 2));
    public static readonly Vector2 PositionProjectDiceII1 = PositionProjectDiceII
        + new Vector2(marginSmall + CardWidth, 0);
    public static readonly Vector2 PositionProjectDiceII2 = PositionProjectDiceII1
        + new Vector2(marginSmall + CardWidth, 0);
    public static readonly Vector2 PositionProjectDiceII3 = PositionProjectDiceII2
        + new Vector2(marginSmall + CardWidth, 0);
    public static readonly Vector2 PositionProjectDiceII4 = PositionProjectDiceII3
        + new Vector2(marginSmall + CardWidth, 0);

    //public static readonly Vector2 PositionProjectDiceIII = ScreenTopCenter
    //+ new Vector2(-CardWidth * 2, -(marginBig + CardHeight / 2));
    public static readonly Vector2 PositionProjectDiceIII1 = PositionProjectDiceIII
        + new Vector2(marginSmall + CardWidth, 0);
    public static readonly Vector2 PositionProjectDiceIII2 = PositionProjectDiceIII1
        + new Vector2(marginSmall + CardWidth, 0);
    public static readonly Vector2 PositionProjectDiceIII3 = PositionProjectDiceIII2
        + new Vector2(marginSmall + CardWidth, 0);
    public static readonly Vector2 PositionProjectDiceIII4 = PositionProjectDiceIII3
        + new Vector2(marginSmall + CardWidth, 0);

    //public static readonly Vector2 PositionProjectDiceIV = ScreenTopCenter
    //+ new Vector2(-CardWidth * 2, -(2 * marginBig + CardHeight + CardHeight / 2));
    public static readonly Vector2 PositionProjectDiceIV1 = PositionProjectDiceIV
        + new Vector2(marginSmall + CardWidth, 0);
    public static readonly Vector2 PositionProjectDiceIV2 = PositionProjectDiceIV1
        + new Vector2(marginSmall + CardWidth, 0);
    public static readonly Vector2 PositionProjectDiceIV3 = PositionProjectDiceIV2
        + new Vector2(marginSmall + CardWidth, 0);
    public static readonly Vector2 PositionProjectDiceIV4 = PositionProjectDiceIV3
        + new Vector2(marginSmall + CardWidth, 0);

    //public static readonly Vector2 PositionProjectDiceV = ScreenTopRight
    //+ new Vector2(-(3 * marginSmall + marginBig + 3 * CardWidth + CardWidth / 2), -(marginBig + CardHeight / 2));
    public static readonly Vector2 PositionProjectDiceV1 = PositionProjectDiceV
        + new Vector2(marginSmall + CardWidth, 0);
    public static readonly Vector2 PositionProjectDiceV2 = PositionProjectDiceV1
        + new Vector2(marginSmall + CardWidth, 0);
    public static readonly Vector2 PositionProjectDiceV3 = PositionProjectDiceV2
        + new Vector2(marginSmall + CardWidth, 0);
    public static readonly Vector2 PositionProjectDiceV4 = PositionProjectDiceV3
        + new Vector2(marginSmall + CardWidth, 0);

    //public static readonly Vector2 PositionProjectDiceVI = ScreenTopRight
    //+ new Vector2(-(3 * marginSmall + marginBig + 3 * CardWidth + CardWidth / 2), -(2 * marginBig + CardHeight + CardHeight / 2));
    public static readonly Vector2 PositionProjectDiceVI1 = PositionProjectDiceVI
        + new Vector2(marginSmall + CardWidth, 0);
    public static readonly Vector2 PositionProjectDiceVI2 = PositionProjectDiceVI1
        + new Vector2(marginSmall + CardWidth, 0);
    public static readonly Vector2 PositionProjectDiceVI3 = PositionProjectDiceVI2
        + new Vector2(marginSmall + CardWidth, 0);
    public static readonly Vector2 PositionProjectDiceVI4 = PositionProjectDiceVI3
        + new Vector2(marginSmall + CardWidth, 0);

}

