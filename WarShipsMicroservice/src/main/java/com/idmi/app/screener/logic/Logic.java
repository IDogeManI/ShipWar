package com.idmi.app.screener.logic;

import java.util.ArrayList;
import java.util.List;

import com.idmi.app.screener.models.GameState;
import com.idmi.app.screener.models.GameStates;
import com.idmi.app.screener.models.Lobby;
import com.idmi.app.screener.models.Player;
import com.idmi.app.screener.models.ShipMatrix;
import com.idmi.app.screener.models.ShipShoot;

public class Logic 
{
    List<Lobby> allLobbies = new ArrayList<Lobby>();
    List<Player> players = new ArrayList<>();
    public Logic() {}
    public int createLobby(int hostId)
    {
        Lobby lobby = new Lobby(hostId);
        allLobbies.add(lobby);
        return lobby.getLobbyId();
    }
    public int createPlayer()
    {
        Player player = new Player();
        players.add(player);
        return player.getPlayerId();
    }
    public boolean connetToLobby(int lobbyId, int memberId)
    {
        for (Lobby lobby : allLobbies) 
        {
            if(lobby.getLobbyId() == lobbyId)
            {
                return lobby.Connect(memberId);
            }
        }
        return false;
    }
    public boolean setMatrx(int lobbyId,int id, List<ShipMatrix> shipMatrix)
    {
        for (Lobby lobby : allLobbies) 
        {
            if(lobby.getLobbyId() == lobbyId)
            {
                return lobby.setMatrx(id,shipMatrix);
            }
        }
        return false;
    }
    public boolean getShootResult(int lobbyId,int id, ShipShoot shipShoot)
    {
        for (Lobby lobby : allLobbies) 
        {
            if(lobby.getLobbyId() == lobbyId)
            {
                return lobby.getShootResult(id, shipShoot);
            }
        }
        return false;
    }
    public ShipShoot getLastOppShoot(int lobbyId,int id)
    {
        for (Lobby lobby : allLobbies) 
        {
            if(lobby.getLobbyId() == lobbyId)
            {
                return lobby.getLastShipShoot(id);
            }
        }
        return new ShipShoot();
    }
    public int getGameState(int lobbyId)
    {
        for (Lobby lobby : allLobbies) 
        {
            if(lobby.getLobbyId() == lobbyId)
            {
                return lobby.getGameState().value;
            }
        }
        return -1;
    }
}
