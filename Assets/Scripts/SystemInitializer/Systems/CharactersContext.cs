namespace SystemInitializer.Systems
{
    public class CharactersContext : MonoBehaviourContext
    {
        public int currentCharacterIndex;
        public Character[] characters;

        public Character CurrentCharacter()
        {
            if (currentCharacterIndex < 0 || currentCharacterIndex >= characters.Length)
                return null;
            return characters[currentCharacterIndex];
        }
    }
}