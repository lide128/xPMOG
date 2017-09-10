package environment;

public class Base extends Structure {

	public Base() {
		super("Base", 'B');
		this.value = Integer.MAX_VALUE;
	}
	
	@Override
	public boolean isTraversible() { return true; }
	
}
