using System;
using Player.Player;
using Player.Team;
using system.GameObjectList;
using static environment.TileCover.EMPTY_COVER;

namespace Environment {

	/**
	 * A class representing a square element of space that can be occupied with 0..n elements or objects
	 * @author Alex White
	 *
	 */
	public class Tile {

		Point coordinates;
		private GameObjectList lyingOnGround;
		private GameObjectList buried;
		private TileCover cover;
		private int inherentValue;
		Player occupyingPlayer; // not sure how this should be implemented; we'll see
		private Team owner;

		public Tile(Point assignedCoords) {
			this(assignedCoords, EMPTY_COVER);
		}

		public Tile(Point assignedCoords, TileCover cover) {
			this.coordinates = assignedCoords;
			this.lyingOnGround = new GameObjectList();
			this.buried = new GameObjectList();
			this.cover = cover;
		}

		public Point getCoordinates() { return coordinates.getLocation(); }

		public String getSymbol() {
			if (occupyingPlayer != null) {
				return occupyingPlayer.getSymbol();
			} else if (cover != EMPTY_COVER) {
				return cover.getSymbol();
			}
			if (!lyingOnGround.isEmpty())
				return lyingOnGround.mostValuableObject().get().getSymbol();
			else
				return cover.getSymbol(); // EMPTY_COVER symbol

		}

		public int getValue() { return inherentValue; }

		public boolean canPickUp() { return !lyingOnGround.isEmpty(); }

		public GameObject pickUp(GameObject pickedUp) {
			return lyingOnGround.remove(lyingOnGround.indexOf(pickedUp));
		}

		public GameObjectList pickUpAll() {
			GameObjectList grabbed = lyingOnGround;
			lyingOnGround = new GameObjectList();
			return grabbed;
		}

		public void drop(GameObject dropped) {
			lyingOnGround.add(dropped);
		}

		public void dropAll(GameObjectList dropped) {
			lyingOnGround.addAll(dropped);
		}

		public GameObject unearth(GameObject dugUp) {
			return buried.remove(buried.indexOf(dugUp));
		}

		public GameObjectList unearthAll() {
			GameObjectList unearthed = buried;
			buried = new GameObjectList();
			return unearthed;
		}

		public void bury(GameObject toBury) {
			buried.add(toBury);
		}

		public void buryAll(GameObjectList toBury) {
			buried.addAll(toBury);
		}

		public boolean hasCover() { return cover != EMPTY_COVER; }

		TileCover getCover() { return cover; }

		public void addCover(TileCover newCover) throws TileOccupiedException {
			if (occupyingPlayer != null || hasCover()) {
				throw new TileOccupiedException();
			}
			cover = newCover;
		}

		/**
	 * Removes the cover of the tile, setting this tile's cover to {@link TileCover#EMPTY_COVER}
	 * @return the {@code TileCover}
	 */
		private TileCover removeCover() {
			TileCover theCover = cover;
			cover = EMPTY_COVER;
			return theCover;
		}

		/**
	 * @return the {@code TileCover} that was able to be dug out from this tile. 
	 * Else, returns {@link TileCover#EMPTY_COVER} if the cover was not 
	 * {@link TileCover#isDiggable() diggable} or if there was no cover
	 */
		TileCover digCover() {
			return cover.isDiggable() ? removeCover() : EMPTY_COVER;
		}

		void addPlayer(Player player) throws TileOccupiedException {
			if (occupyingPlayer != null || !cover.isTraversible()) {
				throw new TileOccupiedException();
			}
			occupyingPlayer = player;
		}

		void removePlayer() { occupyingPlayer = null; }

		public Team getOwner() { return owner; }
		void setOwner(Team newOwner) { this.owner = newOwner; }

		public class TileOccupiedException extends Exception {
			private static final long serialVersionUID = 9030915798299569126L;

			public TileOccupiedException() {
				super("Tile occupied!");
			}
		}

	}

}