package system;

public enum ElementKind {
	
	WATER	("water", 	"you can drink it", 							1, 12, 2),
	HELIUM	("helium", 	"lighter than air", 							5, 50, 5),
	GOLD	("gold", 	"shiny, rare, conductive, and easily maliable", 4, 200, 5),
	SILVER	("silver", 	"shiny, and less rare", 						4, 100, 5),
	URANIUM	("uranium", "dense, toxic, and radioactive", 				4, 75, 4),
	LEAD	("lead", 	"toxic and malliable", 							3, 30, 2),
	COPPER	("copper", 	"conductive, shiny, and easily tarnishes", 		3, 35, 3),
	IRON	("iron", 	"strong, magnetic, and malliable", 				2, 25, 2),
	CARBON	("carbon", 	"forms strong bonds, useful in many forms", 	1, 10, 1),
	;

	private final String name, description;
	private final int elementDifficulty, elementValue, elementRarity;

	private ElementKind(String elementName, 
						String description, 
						int elementDifficulty, 
						int elementValue, 
						int elementRarity) {
		this.name = elementName;
		this.description = description;
		this.elementDifficulty = elementDifficulty;
		this.elementValue = elementValue;
		this.elementRarity = elementRarity;
	}
	
	public String getName() { return name; }
	
	public String getDescription() { return description; }
	
	public int getElementDifficulty() { return elementDifficulty; }
	
	public int getElementValue() { return elementValue; }
	
	public int getElementRarity() { return elementRarity; }
	
	@Override
	public String toString() { return getName(); }
	
}
