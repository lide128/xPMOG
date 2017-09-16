using System;
using Player.Player;
using Player.Team;
using system.ElementKind;
using system.GameElement;

namespace Environment {
	
	/**
	 * A class that randomly generates a game play map 
	 * @author Alex White
	 *
	 */
	public class GenerateMap {

		private static final Random rand = new Random();
		private static final int percentDirtCover = 75;
		private static final int percentChanceDirtIsEmpty = 30;
		
		public static GameMap generateMap(int xSize, int ySize, List<Team> teams) throws Exception {
			if (teams.size() > 4 || teams.size() < 2)
				throw new IllegalArgumentException("Unsupported number of teams; only 2-4 teams supported.");
			GameMap map = new GameMap(xSize, ySize);
			populateMap(map, teams);
			return map;
		}
		
		/**
		 * Create a new game map
		 * @param map
		 */
		public static void populateMap(GameMap map, List<Team> teams) {
			Iterator<Team> it = teams.iterator();
			
			for(int j = 0; j < map.mapY; j++) {
				for(int i = 0; i < map.mapX; i++) {
					addTile(map, it, new Point(i, j));
				}
			}
		}

		private static void addTile(GameMap map, Iterator<Team> it, Point coords) {
			TileCover cover = TileCover.EMPTY_COVER;
			
			if (map.corners().contains(coords)) {
				cover = new Base();
			} else if (roll(percentDirtCover)) {
				cover = randomDirt();
			}
			
			Tile newTile = new Tile(coords, cover);
			map.assignTile(newTile, coords);
			if (cover instanceof Base) {
				Team team = it.next();
				Player player = team.getPlayers().get(0);
				map.movePlayer(player, newTile);
			}
		}
		
		private static Dirt randomDirt() {
			int aveElementVolume = 35000; // 35 liters
			int stdDevElementVolume = 10000; // 10 liters
			Dirt dirt = new Dirt();
			if (!roll(percentChanceDirtIsEmpty)) {
				ElementKind kind = ElementKind.randomElementWeighted(rand);
				GameElement element = new GameElement(kind, gauss(aveElementVolume, stdDevElementVolume));
				dirt.addContents(element);
			}
			return dirt;
		}
		
		private static boolean roll(int percentChance) {
			return rand.nextInt(100) < percentChance;
		}
		
		private static int gauss(int mean, int stdDev) {
			return mean + stdDev * (int) rand.nextGaussian();
		}
		
	}
}