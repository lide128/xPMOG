using System;
using Environment.GameMap;
using Environment.GameMap.Direction;
using Environment.Tile;

namespace Player {

	public class AI {

		private static final Random rand = new Random();
		
		public static Action takeTurn(GameMap map, Player player) {
			if (map.getLocation(player).canPickUp() && !player.getInventory().isFull())
				return new PickUpAllAction();
				
			if (isHome(map, player) && !player.getInventory().isEmpty())
				return new DepositAllAction(); // dump inventory
			
			Direction direction;
			if (player.getInventory().isFull())
				direction = goHome(map, player);
			else
				direction = Direction.random(rand);
			return new AdjacentAction(direction);
		}
		
		private static boolean isHome(GameMap map, Player player) {
			return goHome(map, player) == null;
		}
		
		private static Direction goHome(GameMap map, Player player) {
			return navigateTo(map.getLocation(player), map.getLocation(player.getHomeBase()));
		}
		
		private static Direction navigateTo(Tile from, Tile to) {
			Point origin = from.getCoordinates();
			Point destination = to.getCoordinates();
			Point distance = destination.getLocation();
			distance.translate(-origin.x, -origin.y);
			return Direction.onto(distance);
		}
		
		public static interface Action {
			
		}
		
		public static class AdjacentAction implements Action {
			// move or dig adjacent
			// construct building adjacent
			
			private final Direction direction; 
			
			public AdjacentAction(Direction direction) {
				this.direction = direction;
			}
			
			public Direction getDirection() { return direction; }
		}
		
		public static class HereAction implements Action {
			// turn?
			// get GameObject from here
			// get all from here
			// put GameObject here
			// put all
			// unearth all here
			// bury GameObject here
		}
		
		public static class PickUpAllAction extends HereAction {
		}
		
		public static class DepositAllAction extends HereAction {
		}
		
	}
}