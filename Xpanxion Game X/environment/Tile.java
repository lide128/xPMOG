package environment;

import java.awt.Point;

import player.Player;
import player.Team;
import system.GameObjectList;
import static environment.TileCover.EMPTY_COVER;

/**
 * A class representing a square element of space that can be occupied with 0..n elements or objects
 * @author Alex White
 *
 */
public class Tile {
	
	Point coordinates;
	GameObjectList gameObjects;
	private TileCover cover = EMPTY_COVER;
	private int inherentValue;
	Player occupyingPlayer; // not sure how this should be implemented; we'll see
	private Team owner;
	
	public Tile(Point assignedCoords) {
		this(assignedCoords, EMPTY_COVER);
	}
	
	public Tile(Point assignedCoords, TileCover cover) {
		this.coordinates = assignedCoords;
		this.gameObjects = new GameObjectList();
		this.cover = cover;
	}
	
	public Point getCoordinates() { return coordinates; }
	
	public TileCover getCover() { return cover; }
	
	/**
	 * Removes the cover of the tile, setting this tile's cover to {@link TileCover#EMPTY_COVER}
	 * @return the {@code TileCover}
	 */
	public TileCover removeCover() {
		TileCover theCover = cover;
		cover = EMPTY_COVER;
		return theCover;
	}
	
	public int getValue() { return inherentValue; }
	
	public boolean hasCover() { return cover != EMPTY_COVER; }
	
	public char getSymbol() {
		return occupyingPlayer != null ? occupyingPlayer.getSymbol() : cover.getSymbol();
	}
	
	public void addPlayer(Player player) throws TileOccupiedException {
		if (occupyingPlayer != null || !cover.isTraversible()) {
			throw new TileOccupiedException();
		}
		occupyingPlayer = player;
	}
	
	public void removePlayer() { occupyingPlayer = null; }
	
	public Team getOwner() { return owner; }
	public void setOwner(Team newOwner) { this.owner = newOwner; }
	
	/**
	 * @return the {@code TileCover} that was able to be dug out from this tile. 
	 * Else, returns {@link TileCover#EMPTY_COVER} if the cover was not 
	 * {@link TileCover#isDiggable() diggable} or if there was no cover
	 */
	public TileCover digCover() {
		return cover.isDiggable() ? removeCover() : EMPTY_COVER;
	}

	public class TileOccupiedException extends Exception {
		private static final long serialVersionUID = 9030915798299569126L;

		public TileOccupiedException() {
			super("Tile occupied!");
		}
	}

}
