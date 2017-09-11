package main;

import java.util.ArrayList;
import java.util.List;

import environment.GameMap;
import static environment.GameMap.Direction.*;
import environment.GenerateMap;
import player.Player;
import player.Team;

public class GameRunner {

	
	public static void main(String[] args) throws Exception {
		List<Team> teams = new ArrayList<>();
		
		Team team1 = new Team("Team One!");
		Team team2 = new Team("Team Two!");
		Team team3 = new Team("Team Three!");
		Team team4 = new Team("Team Four!");
		
		Player player1 = new Player("Player One", '1');
		Player player2 = new Player("Player Two", '2');
		Player player3 = new Player("Player Three", '3');
		Player player4 = new Player("Player Four", '4');
		
		team1.addPlayer(player1);
		team2.addPlayer(player2);
		team3.addPlayer(player3);
		team4.addPlayer(player4);
		
		teams.add(team1);
		teams.add(team2);
		teams.add(team3);
		teams.add(team4);
		
		int mapX = 15;
		int mapY = 10;
		GameMap map = GenerateMap.generateMap(mapX, mapY, teams);
		
		Session session = new Session(map, teams);

		for (int i = 0; i < Math.min(mapX, mapY) - 2; i++) {
			session.movePlayerOrDig(player1, UP);
			session.movePlayerOrDig(player2, LEFT);
			session.movePlayerOrDig(player3, RIGHT);
			session.movePlayerOrDig(player4, DOWN);
		}
	}
	
}
