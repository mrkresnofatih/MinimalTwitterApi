using MinimalTwitterApi.Templates;

namespace MinimalTwitterApi.Utilities
{
    public class PlayerPasswordUtility : BcryptTemplate
    {
        protected override string GetSalt()
        {
            return "0LkOlin3mutYnRzlQ8acYNK5tmAjVSGvQgjRkpfR";
        }
    }
}