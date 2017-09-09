package environment;

public class Base extends Structure {

	public Base() {
		this.symbol = 'B';
		this.name = "Base";
		this.value = Integer.MAX_VALUE;
	}
	
	@Override
	public boolean isTraversible() { return true; }
	
}
