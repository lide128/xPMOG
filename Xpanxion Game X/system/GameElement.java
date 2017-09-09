package system;


/**
 * A class representing resources, map features, and code nuggets that occupy GameSquares
 * @author Alex White
 *
 */
public class GameElement extends GameObject {
	
	private final ElementKind kind;
	
	public GameElement(ElementKind kind) {
		super(kind.getName(), kind.getElementValue());
		this.kind = kind;
	}

	@Override
	public int hashCode() {
		final int prime = 31;
		int result = super.hashCode();
		result = prime * result + ((kind == null) ? 0 : kind.hashCode());
		return result;
	}

	@Override
	public boolean equals(Object obj) {
		if (this == obj)
			return true;
		if (!super.equals(obj))
			return false;
		if (getClass() != obj.getClass())
			return false;
		GameElement other = (GameElement) obj;
		if (kind != other.kind)
			return false;
		return true;
	}

}
