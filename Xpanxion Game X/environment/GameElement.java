package environment;

/**
 * A class representing elements and map features that occupy GameSquares
 * @author Alex White
 *
 */
public class GameElement {
	
	String name;
	int difficulty; //how difficult the element is to mine or to breakdown/move scale of 1..5
	int value; //how much it is worth to the player scale of 1..5
	int rarity; //how common it occurs in the map scale of 1..5
	
	
	public GameElement(String elementName, int elementDifficulty, int elementValue, int elementRarity) {
		name = elementName;
		difficulty = elementDifficulty;
		value = elementValue;
		rarity = elementRarity;
	}
	
	public void changeName(String newName) {
		name = newName;
	}
	
	public void changeDifficulty(int newDifficulty) {
		difficulty = newDifficulty;
	}
	
	public void changeValue(int newValue) {
		value = newValue;
	}
	
	public void changeRarity(int newRarity) {
		rarity = newRarity;
	}
	
	public boolean equals(Object element) {
		GameElement compare = (GameElement) element;
		return compare.name.equals(name) &&
				compare.difficulty == difficulty &&
				compare.value == value &&
				compare.rarity == rarity;
	}
	

}
