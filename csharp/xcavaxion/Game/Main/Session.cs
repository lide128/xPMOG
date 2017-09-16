using System;

using Player.Player;
using Player.Team;
using Environment.GameMap;
using Environment.GameMap.Direction;
using Environment.Tile;
using Environment.TileCover;

namespace Main {

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
		
		void movePlayerOrDig(Player player, Direction direction) {
			String msg = "";
			boolean blocked = !map.movePlayer(player, direction);
			if (blocked) {
				Tile newTile = map.translate(map.getLocation(player), direction);
				TileCover dugCover = newTile.digCover();
				msg += player.getName();
				if (dugCover != TileCover.EMPTY_COVER) {
					msg += " dug cover: " + dugCover.getName();
					for (GameObject found : dugCover.getContents()) {
						player.acquireObject(found);
						msg += " and found " + found;
					}
				} else {
					msg += " failed to dig!";
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
			
			for (Team team : teams) {
				output += team.getName() + ":\n";
				for (Player player : team.getPlayers()) {
					output += player.getName() + ": ";
					output += player.netWorth() + "\n";
				}
			}
			
			return output;
		}

		private static void clearConsole(int linesToClear) {
			for (int i=0; i < linesToClear; i++)
				System.out.println();
		}
		
	}
}