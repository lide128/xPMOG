package environment;

import static environment.TileCover.EMPTY_COVER;

import java.awt.Point;
import java.util.Optional;

import player.Player;
import player.Team;
import system.GameObject;
import system.GameObjectList;

/**
 * A class representing a square element of space that can be occupied with 0..n elements or objects
 * @author Alex White
 *
 */
public class Tile {
	
	Point coordinates;
	private GameObjectList lyingOnGround;
	private GameObjectList buried;
	private TileCover cover = EMPTY_COVER;
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
	
	public Point getCoordinates() { return coordinates; }
	
	public GameObject pickUp(GameObject pickedUp) {
		return lyingOnGround.get(lyingOnGround.indexOf(pickedUp));
	}
	
	public GameObjectList pickUpAll() {
		GameObjectList grabbed = lyingOnGround;
		lyingOnGround = new GameObjectList();
		return grabbed;
	}
	
	public void drop(GameObject dropped) {
		lyingOnGround.add(dropped);
	}
	
	public GameObject unearth(GameObject dugUp) {
		return buried.get(buried.indexOf(dugUp));
	}
	
	public void bury(GameObject toBury) {
		buried.add(toBury);
	}
	
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
