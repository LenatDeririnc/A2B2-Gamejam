using Movement;

namespace SystemInitializer.Systems
{
    public class CharactersSpeechSelector : ButtonAction
    {
        private CharactersContext CharactersContext => ContextsContainer.GetContext<CharactersContext>();
        
        public override void Execute()
        {
            CharactersContext.CurrentCharacter().Speech.Execute();
        }

        public override bool IsEnabled()
        {
            return CharactersContext.CurrentCharacter() != null;
        }
    }
}