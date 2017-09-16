package environment;

public class Dirt extends TileCover {

	public Dirt() {
		super("Dirt", "X");
	}
	
	@Override
	public boolean isDiggable() { return true; }
	
}
