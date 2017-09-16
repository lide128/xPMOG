using System;

using Player.Player;
using Player.Team;
using Environment.GameMap;
using Environment.GameMap.Direction;
using Environment.Tile;
using Environment.TileCover;

namespace Main {

	public class Session {

		private static int DEFAULT_PAUSE = 1000;
		private int CELL_LENGTH = 5;

		private GameMap map;
		private List<Team> teams;

		public Session(GameMap map, List<Team> teams) {
			this.map = map;
			this.teams = teams;
			drawAndWait(map, "Begin!");
		}

		GameMap getMap() { return map; }

		List<Team> getTeams() { return new ArrayList<>(teams); }

		void movePlayerOrDig(Player player, Direction direction) throws TileNotFoundException {
			String msg = player.getName();
			boolean blocked = !map.movePlayer(player, direction);
			if (blocked) {
				try {
					Tile newTile = map.translate(map.getLocation(player), direction);
					TileCover dugCover = map.digCover(newTile);
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
				msg += " moved " + direction.name();
			}
			drawAndWait(map, msg);
		}

		void pickUpAll(Player player) {
			// TODO support other kinds of picking up
			String msg = player.getName() + "tried to pick up items";
			Tile location = map.getLocation(player);
			GameObjectList allObjects = location.pickUpAll();
			if (allObjects.isEmpty()) {
				msg += ", but nothing was found!";
			} else {
				msg += ": (" +allObjects.stream().map(GameObject::getName).collect(Collectors.joining(", ")) + ")";
				Optional<GameObjectList> spillOver = player.acquireAll(allObjects);
				if (spillOver.isPresent()) {
					location.dropAll(spillOver.get());
					msg += " but couldn't carry any more!";
				}
			}
			drawAndWait(map, msg);
		}

		void depositItem(Player player, GameObject obj) {
			Tile location = map.getLocation(player);
			Base base = player.getTeam().getBases().get(0); // XXX not robust logic
			if (location.equals(map.getLocation(base))) {
				base.storeObject(obj);
			} else {
				// else, drop on the ground
				try {
					location.drop(player.loseObject(obj));
				} catch (ObjectNotFoundException e) {
					e.printStackTrace(); // TODO
				}
			}
			drawAndWait(map, player.getName() + " deposited " + obj);
		}

		void depositAll(Player player) {
			Tile location = map.getLocation(player);
			Base base = player.getTeam().getBases().get(0); // XXX not robust logic
			if (location.equals(map.getLocation(base))) {
				base.storeAll(player.loseAllObjects());
			} else {
				// else, drop on the ground
				location.dropAll(player.loseAllObjects());
			}
			drawAndWait(map, player.getName() + " deposited entire inventory");
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

}