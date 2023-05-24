package com.idmi.app.screener.models;

public enum GameStates 
{
    None(0),
    ShipCreation(1),
    HostTurn(2),
    MemberTurn(3),
    End(4);

    public int value;
    private GameStates(int num) 
    {
        value = num;
    }
    
}
