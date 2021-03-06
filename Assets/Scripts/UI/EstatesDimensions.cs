﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GD = GameDimensions;

public static class EstatesDimensions {

    public static readonly Vector2 PlayerTextSize = new Vector2(400f, 100f);

    public static readonly Vector2 Player4Points = new Vector2(GD.ScreenBottomLeft.x + GD.MarginBig + GD.CardWidth / 2, GD.ScreenBottomLeft.y + GD.MarginBig + GD.CardHeight / 2);
    public static readonly Vector2 Player3Points = Player4Points + new Vector2(0, GD.CardHeight + GD.MarginSmall);
    public static readonly Vector2 Player2Points = Player3Points + new Vector2(0, GD.CardHeight + GD.MarginSmall);
    public static readonly Vector2 Player1Points = Player2Points + new Vector2(0, GD.CardHeight + GD.MarginSmall);

    public static readonly Vector2 Player4Name = Player4Points + new Vector2(GD.CardWidth / 2 + PlayerTextSize.x / 2, 0);
    public static readonly Vector2 Player3Name = Player4Name + new Vector2(0, GD.CardHeight + GD.MarginSmall);
    public static readonly Vector2 Player2Name = Player3Name + new Vector2(0, GD.CardHeight + GD.MarginSmall);
    public static readonly Vector2 Player1Name = Player2Name + new Vector2(0, GD.CardHeight + GD.MarginSmall);

    public static readonly float CardsSpaceStart = Player4Name.x + PlayerTextSize.x / 2 + GD.CardWidth / 2;

    public static readonly Vector2 Player4Workers = new Vector2(CardsSpaceStart + 3 * GD.CardWidth + 3 * GD.MarginSmall + GD.MarginBig * 2, Player4Name.y);
    public static readonly Vector2 Player3Workers = Player4Workers + new Vector2(0, GD.CardHeight + GD.MarginSmall);
    public static readonly Vector2 Player2Workers = Player3Workers + new Vector2(0, GD.CardHeight + GD.MarginSmall);
    public static readonly Vector2 Player1Workers = Player2Workers + new Vector2(0, GD.CardHeight + GD.MarginSmall);

    public static readonly Vector2 Player4Silver = Player4Workers + new Vector2(GD.CardWidth + GD.MarginBig * 2, 0);
    public static readonly Vector2 Player3Silver = Player3Workers + new Vector2(GD.CardWidth + GD.MarginBig * 2, 0);
    public static readonly Vector2 Player2Silver = Player2Workers + new Vector2(GD.CardWidth + GD.MarginBig * 2, 0);
    public static readonly Vector2 Player1Silver = Player1Workers + new Vector2(GD.CardWidth + GD.MarginBig * 2, 0);


}
