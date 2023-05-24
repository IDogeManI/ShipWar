package com.idmi.app.screener.models;


public class Player 
{
    private static int playerCount = 0;
    private int playerId;
    public Player() 
    {
        playerCount++;
        playerId = playerCount;
    }
    public int getPlayerId() 
    {
        return playerId;
    }
    
}
