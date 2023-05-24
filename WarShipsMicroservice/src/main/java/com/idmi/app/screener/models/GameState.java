package com.idmi.app.screener.models;

public class GameState 
{
    private GameStates currentState;

    public GameStates getCurrentState() 
    {
        return currentState;
    }

    public void setCurrentState(GameStates currentState) 
    {
        this.currentState = currentState;
    }
    
}
