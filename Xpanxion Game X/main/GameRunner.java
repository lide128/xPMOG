package main;

import java.util.ArrayList;
import java.util.List;
import java.util.Random;

import player.AI;
import player.AI.Action;
import player.AI.AdjacentAction;
import player.AI.DepositAllAction;
import player.AI.PickUpAllAction;
import player.Player;
import player.Team;
import environment.GameMap;
import environment.GameMap.TileNotFoundException;
import environment.GenerateMap;

public class GameRunner {

	
	public static void main(String[] args) throws Exception {
		
		// GENERATE TEAMS
		List<Team> teams = new ArrayList<>();
		
		Team team1 = new Team("Team One!");
		Team team2 = new Team("Team Two!");
		Team team3 = new Team("Team Three!");
		Team team4 = new Team("Team Four!");
		
		Player player1 = new Player("Player One", "1", team1);
		Player player2 = new Player("Player Two", "2", team2);
		Player player3 = new Player("Player Three", "3", team3);
		Player player4 = new Player("Player Four", "4", team4);
		
		team1.addPlayer(player1);
		team2.addPlayer(player2);
		team3.addPlayer(player3);
		team4.addPlayer(player4);
		
		teams.add(team1);
		teams.add(team2);
		teams.add(team3);
		teams.add(team4);
		
		// GENERATE MAP
		int mapX = 30;
		int mapY = 14;
		GameMap map = GenerateMap.generateMap(mapX, mapY, teams);
		
		Session session = new Session(map, teams);

		// DO STUFF FOR A WHILE
		Random rand = new Random();
		
		for (int i = 0; i < 1000; i++) {
			moveAllPlayers(session);
		}
	}

	private static void moveAllPlayers(Session session) {
		for (Team team : session.getTeams()) {
			for (Player player : team.getPlayers()) {
				boolean moved = false;
				int safetyCounter = 0;
				
				while (!moved && safetyCounter++ < 100) {
					try {
						Action action = AI.takeTurn(session.getMap(), player);
						if (action instanceof AdjacentAction)
							session.movePlayerOrDig(player, ((AdjacentAction) action).getDirection());
						else if (action instanceof PickUpAllAction)
							session.pickUpAll(player);
						else if (action instanceof DepositAllAction)
							session.depositAll(player);
						moved = true;
					} catch (TileNotFoundException e) {
						// tried to walk off the map, try again
					}
				}
			}
		}
	}
	
}
