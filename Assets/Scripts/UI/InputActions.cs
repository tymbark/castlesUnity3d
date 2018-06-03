using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InputActions {

    public enum ClickAction {
        NotSet = 0,
        EndTurn, 
        UseSilver,
        ExitGame, 

        ShowProjects, 
        ShowWorkers, 
        ShowSilver, 
        ShowEstates, 
        ShowAnimals, 
        ShowBonuses, 
        ShowStorage 
    }

    //not used
    public enum DragDropAction {
        NotSet = 0,
        BuyWorkers,
        BuySilver,
        SellSilverAndWorkers,
        TakeProject,
        BuildProject,
        ShipGoods,
        AddSilverProject
    }

    public enum OtherGameEvent {
        ChooseAnimals,
        ChooseGoods,
        PlaceCompletedProject
    }

}
