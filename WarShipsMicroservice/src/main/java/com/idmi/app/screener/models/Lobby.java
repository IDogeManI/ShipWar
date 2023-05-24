package com.idmi.app.screener.models;

import java.util.ArrayList;
import java.util.List;

public class Lobby
{
    
    private static int lobbyCount = 0;
    private boolean isHostTurn = true;
    private int lobbyId;
    private int hostId;
    private int memberId;
    private GameState gameState = new GameState();
    private List<ShipMatrix> hostShipMatrix = new ArrayList<>();
    private List<ShipMatrix> memberShipMatrix = new ArrayList<>();
    private int hostHealth = 20;
    private int memberHealth = 20;
    private ShipShoot lastHostShoot = new ShipShoot();
    private ShipShoot lastMemberShoot = new ShipShoot();
    
    public Lobby(int hostId) 
    {
        lobbyCount++;
        lobbyId = lobbyCount;
        this.hostId = hostId;
        lastHostShoot.x = -1;
        lastHostShoot.y = -1;
        lastMemberShoot.x = -1;
        lastMemberShoot.y = -1;
        gameState.setCurrentState(GameStates.None);
    }

    public int getLobbyId() 
    {
        return lobbyId;
    }
    public int getHostId() 
    {
        return hostId;
    }
    public int getMemberId() 
    {
        return memberId;
    }
    public GameStates getGameState() 
    {
        return gameState.getCurrentState();
    }
    public List<ShipMatrix> getHostShipMatrix() 
    {
        return hostShipMatrix;
    }
    public List<ShipMatrix> getMemberShipMatrix() 
    {
        return memberShipMatrix;
    }
    
    public boolean Connect(int memberId)
    {
        this.memberId = memberId;
        gameState.setCurrentState(GameStates.ShipCreation);
        return true;
    }

    public boolean NextTurn()
    {
        if (gameState.getCurrentState()==GameStates.End) 
        {
            return true;
        }
        if (isHostTurn) 
            gameState.setCurrentState(GameStates.MemberTurn);
        else
            gameState.setCurrentState(GameStates.HostTurn);
        isHostTurn = !isHostTurn;
        return true;
    }

    public boolean setMatrx(int id, List<ShipMatrix> shipMatrix)
    {
        if(id == hostId)
        {
            hostShipMatrix.clear();
            hostShipMatrix.addAll(shipMatrix);
        }
        else if(id == memberId)
        {
            memberShipMatrix.clear();
            memberShipMatrix.addAll(shipMatrix);
        }
        if (hostShipMatrix.size()!=0&&memberShipMatrix.size()!=0) 
        {
            gameState.setCurrentState(GameStates.HostTurn);
        }
        return true;
    }

    public ShipShoot getLastShipShoot(int id)
    {
        if(id == hostId)
        {
            return lastMemberShoot;
        }
        else if(id == memberId)
        {
            return lastHostShoot;
        }
        return new ShipShoot();
    }

    public boolean getShootResult(int id, ShipShoot shoot)
    {
        if(id == hostId)
        {
            lastHostShoot.x = shoot.x;
            lastHostShoot.y = shoot.y;
            for (ShipMatrix shipMatrix : memberShipMatrix)
            {
                if (shoot.x==shipMatrix.x&&shoot.y==shipMatrix.y) 
                {
                    if (shipMatrix.isShipped) 
                    {
                        memberHealth--;
                        if (memberHealth == 0) 
                        {
                            gameState.setCurrentState(GameStates.End);
                        }
                    }
                    NextTurn();
                    return shipMatrix.isShipped;
                }
            }
        }
        else if(id == memberId)
        {
            lastMemberShoot.x = shoot.x;
            lastMemberShoot.y = shoot.y;
            for (ShipMatrix shipMatrix : hostShipMatrix)
            {
                if (shoot.x==shipMatrix.x&&shoot.y==shipMatrix.y) 
                {
                    if (shipMatrix.isShipped) 
                    {
                        hostHealth--;
                        if (hostHealth == 0) 
                        {
                            gameState.setCurrentState(GameStates.End);
                        }
                    }
                    NextTurn();
                    return shipMatrix.isShipped;
                }
            }
        }
        return false;
    }
}
