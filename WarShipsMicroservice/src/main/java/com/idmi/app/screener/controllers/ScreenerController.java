package com.idmi.app.screener.controllers;

import java.util.List;

import javax.servlet.http.HttpServletRequest;

import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RestController;

import com.idmi.app.screener.logic.Logic;
import com.idmi.app.screener.models.ShipMatrix;
import com.idmi.app.screener.models.ShipShoot;


@RestController
@CrossOrigin("*")
public class ScreenerController
{
	public Logic logic = new Logic();
	@GetMapping("/createplayer")
	public int createPlayer(HttpServletRequest request)
	{
		return logic.createPlayer();
	}
	@GetMapping("/createlobby")
	public int createLobby(HttpServletRequest request)
	{
		int hostId;
		if (request.getParameter("id") != null)
		{
			hostId = Integer.valueOf(request.getParameter("id"));
			return logic.createLobby(hostId);
		}
		else
		{ 
			return -1;
		}
	}
	@GetMapping("/connecttolobby")
	public boolean connectToLobby(HttpServletRequest request)
	{
		int memberId;
		int lobbyId;
		if (request.getParameter("id") != null&&request.getParameter("lobbyId") != null)
		{
			memberId = Integer.valueOf(request.getParameter("id"));
			lobbyId = Integer.valueOf(request.getParameter("lobbyId"));
			return logic.connetToLobby(lobbyId,memberId);
		}
		else
		{ 
			return false;
		}
	}
	@PostMapping("/setshipmatrix")
	public boolean setShipMatrix(HttpServletRequest request,@RequestBody List<ShipMatrix> shipMatrix)
	{
		int id;
		int lobbyId;
		if (request.getParameter("id") != null&&request.getParameter("lobbyId") != null)
		{
			id = Integer.valueOf(request.getParameter("id"));
			lobbyId = Integer.valueOf(request.getParameter("lobbyId"));
			return logic.setMatrx(lobbyId,id,shipMatrix);
		}
		else
		{ 
			return false;
		}
	}
	@PostMapping("/shoot")
	public boolean shoot(HttpServletRequest request,@RequestBody ShipShoot shipShoot)
	{
		int id;
		int lobbyId;
		if (request.getParameter("id") != null&&request.getParameter("lobbyId") != null)
		{
			id = Integer.valueOf(request.getParameter("id"));
			lobbyId = Integer.valueOf(request.getParameter("lobbyId"));
			return logic.getShootResult(lobbyId,id,shipShoot);
		}
		else
		{ 
			return false;
		}
	}
	@GetMapping("/getlastoppshoot")
	public ShipShoot getLastOppShoot(HttpServletRequest request)
	{
		int id;
		int lobbyId;
		if (request.getParameter("id") != null&&request.getParameter("lobbyId") != null)
		{
			id = Integer.valueOf(request.getParameter("id"));
			lobbyId = Integer.valueOf(request.getParameter("lobbyId"));
			return logic.getLastOppShoot(lobbyId,id);
		}
		else
		{ 
			return new ShipShoot();
		}
	}
	@GetMapping("/getlobbygamestate")
	public int getLobbyGameState(HttpServletRequest request)
	{
		int lobbyId;
		if (request.getParameter("lobbyId") != null)
		{
			lobbyId = Integer.valueOf(request.getParameter("lobbyId"));
			return logic.getGameState(lobbyId);
		}
		else
		{ 
			return -1;
		}
	}
}
