package environment;

public abstract class Structure extends TileCover {

	int value;
	
	public Structure(String name, char symbol) {
		super(name, symbol);
	}
	
	public int getValue() { return value; }

}
