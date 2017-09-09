package main;

import java.util.ArrayList;
import java.util.List;
import java.util.function.Consumer;

import environment.GameMap;
import environment.GameMap.Direction;
import environment.GenerateMap;
import player.Player;
import player.Team;

public class GameRunner {

private static final int CELL_LENGTH = 5;
	
	public static void main(String[] args) throws Exception {
		List<Team> teams = new ArrayList<>();
		
		Team team1 = new Team("One!");
		Team team2 = new Team("Two!");
		Team team3 = new Team("Three!");
		Team team4 = new Team("Four!");
		
		Player player1 = new Player('1');
		Player player2 = new Player('2');
		Player player3 = new Player('3');
		Player player4 = new Player('4');
		
		team1.addPlayer(player1);
		team2.addPlayer(player2);
		team3.addPlayer(player3);
		team4.addPlayer(player4);
		
		teams.add(team1);
		teams.add(team2);
		teams.add(team3);
		teams.add(team4);
		
		GameMap map = GenerateMap.generateMap(15, 10, teams);
		
		clearConsole();
		map.basicPrintMap();
//		map.printMap(CELL_LENGTH);
		
		
		
		
		
		
		map.movePlayer(player1, Direction.UP);
		waitAndPrint(map);
		
		map.movePlayer(player2, Direction.LEFT);
		waitAndPrint(map);
		
		map.movePlayer(player3, Direction.RIGHT);
		waitAndPrint(map);
		
		map.movePlayer(player4, Direction.DOWN);
		waitAndPrint(map);
	}
	
	private static void waitAndPrint(GameMap map) {
		try { Thread.sleep(2500); } catch (Exception e) {}
		clearConsole();
		map.basicPrintMap();
//		map.printMap(CELL_LENGTH);
	}

	private static void clearConsole() {
		for (int i=0; i < 40; i++)
			System.out.println();
	}
	
}
