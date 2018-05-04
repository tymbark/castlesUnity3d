using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GD = GameDimensions;

public static class EstatesDimensions {

    public static readonly Vector2 PlayerTextSize = new Vector2(400f, 100f);

    public static readonly Vector2 Player4Name = new Vector2(GD.ScreenBottomLeft.x + GD.MarginBig + PlayerTextSize.x / 2, GD.ScreenBottomLeft.y + GD.MarginBig + GD.CardHeight / 2);
    public static readonly Vector2 Player3Name = Player4Name + new Vector2(0, GD.CardHeight + GD.MarginSmall);
    public static readonly Vector2 Player2Name = Player3Name + new Vector2(0, GD.CardHeight + GD.MarginSmall);
    public static readonly Vector2 Player1Name = Player2Name + new Vector2(0, GD.CardHeight + GD.MarginSmall);

    public static readonly Vector2 Player4Points = Player4Name + new Vector2(PlayerTextSize.x / 2 + GD.MarginSmall, 0);
    public static readonly Vector2 Player3Points = Player3Name + new Vector2(PlayerTextSize.x / 2 + GD.MarginSmall, 0);
    public static readonly Vector2 Player2Points = Player2Name + new Vector2(PlayerTextSize.x / 2 + GD.MarginSmall, 0);
    public static readonly Vector2 Player1Points = Player1Name + new Vector2(PlayerTextSize.x / 2 + GD.MarginSmall, 0);

    public static readonly float CardsSpaceStart = Player4Points.x + GD.CardWidth / 2 + GD.MarginBig;

}
