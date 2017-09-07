package system;


/**
 * A class representing resources, map features, and code nuggets that occupy GameSquares
 * @author Alex White
 *
 */
public class GameElement extends GameObject {
	
	int difficulty; //how difficult the element is to mine or to breakdown/move scale of 1..5
	int rarity; //how common it occurs in the map scale of 1..5
	
	
	public GameElement(String elementName, String elementDesc, int elementDifficulty, int elementValue, int elementRarity) {
		name = elementName;
		description = elementDesc;
		difficulty = elementDifficulty;
		value = elementValue;
		rarity = elementRarity;
	}
	
	
	public void changeDifficulty(int newDifficulty) {
		difficulty = newDifficulty;
	}
	
	
	public void changeRarity(int newRarity) {
		rarity = newRarity;
	}
	
	public boolean equals(Object element) {
		GameElement compare = (GameElement) element;
		return compare.name.equals(name) &&
				compare.description.equals(description) &&
				compare.difficulty == difficulty &&
				compare.value == value &&
				compare.rarity == rarity;
	}
	

}
