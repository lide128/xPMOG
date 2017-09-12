package main;

import java.text.NumberFormat;
import java.util.List;
import java.util.Locale;
import java.util.Optional;

import environment.GameMap;
import environment.GameMap.Direction;
import environment.GameMap.TileNotFoundException;
import environment.Tile;
import environment.TileCover;
import player.Player;
import player.Team;
import system.GameObject;

public class Session {
	
	private static int DEFAULT_PAUSE = 3000;
	private int CELL_LENGTH = 5;

	private GameMap map;
	private List<Team> teams;
	
	public Session(GameMap map, List<Team> teams) {
		this.map = map;
		this.teams = teams;
		drawAndWait(map, "Begin!");
	}
	
	void movePlayerOrDig(Player player, Direction direction) throws TileNotFoundException {
		String msg = "";
		boolean blocked = !map.movePlayer(player, direction);
		if (blocked) {
			try {
				Tile newTile = map.translate(map.getLocation(player), direction);
				TileCover dugCover = newTile.digCover();
				msg += player.getName();
				if (dugCover != TileCover.EMPTY_COVER) {
					msg += " dug cover: " + dugCover.getName();
					for (GameObject found : dugCover.getContents()) {
						Optional<? extends GameObject> spillOver = player.acquireObject(found);
						msg += " and found " + found;
						if (spillOver.isPresent()) {
							newTile.drop(spillOver.get());
							msg += " but couldn't carry any more!";
						}
					}
				} else {
					msg += " failed to dig!";
				}
			} catch (TileNotFoundException e) {
				throw e;
			}
		} else {
			msg += player.getName() + " moved " + direction.name();
		}
		drawAndWait(map, msg);
	}
	
	private void drawAndWait(GameMap map, String outputMessage) {
		clearConsole(40);
		System.out.println(outputMessage);
		System.out.println();
		System.out.println(scoreboard());
		System.out.println();
		
//		map.basicPrintMap();
		map.printMap(CELL_LENGTH);
		try { Thread.sleep(DEFAULT_PAUSE); } catch (Exception e) {}
	}
	
	private String scoreboard() {
		String output = "";
		Team leading = null;
		int topScore = 0;
		
		for (Team team : teams) {
			Player player = team.getPlayers().get(0);
			int netWorth = player.netWorth();
			if (netWorth > topScore) {
				topScore = netWorth;
				leading = team;
			}
		}
		for (Team team : teams) {
			output += team.getName() + (team.equals(leading) ? " <-- Leader" : "") + "\n";
			for (Player player : team.getPlayers()) {
				output += player.getName() + ": ";
				 NumberFormat numberFormat = NumberFormat.getNumberInstance(Locale.US);
			     output += numberFormat.format(player.netWorth()) + "\n";
			}
		}
		
		return output;
	}

	private static void clearConsole(int linesToClear) {
		for (int i=0; i < linesToClear; i++)
			System.out.println();
	}
	
}
