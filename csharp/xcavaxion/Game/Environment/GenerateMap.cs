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
		private static final int minimumTilesPerPlayer = 10;
		private static final int percentDirtCover = 75;
		private static final int percentChanceDirtIsEmpty = 30;

		public static GameMap generateMap(int xSize, int ySize, List<Team> teams) throws Exception {
			if (teams.size() > 4 || teams.size() < 2)
				throw new IllegalArgumentException("Unsupported number of teams; only 2-4 teams supported.");
			long players = teams.stream().flatMap(team -> team.getPlayers().stream()).count();
			if (!mapLargeEnough(xSize, ySize, players))
				throw new IllegalArgumentException("The given teams have too many players for the map size requested.");
			GameMap map = new GameMap(xSize, ySize);
			populateMap(map, teams);
			return map;
		}

		private static boolean mapLargeEnough(int xSize, int ySize, long numPlayers) {
			return xSize*ySize >= numPlayers * minimumTilesPerPlayer;
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
			// XXX tile generation and assignment is currently very messy

			TileCover cover = TileCover.EMPTY_COVER;

			if (map.corners().contains(coords)) {
				cover = new Base();
			} else if (roll(percentDirtCover)) {
				cover = randomDirt();
			}

			Tile newTile = new Tile(coords);
			map.assignTile(newTile, coords);
			if (cover instanceof Base) {
				map.placeStructure((Base) cover, newTile);
				Team team = it.next();
				team.addBase((Base) cover);
				Player player = team.getPlayers().get(0);
				map.movePlayer(player, newTile);
			} else {
				try { newTile.addCover(cover); } catch (TileOccupiedException e) {}
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