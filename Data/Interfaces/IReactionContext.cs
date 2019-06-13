using System.Collections.Generic;
using Models;

namespace Data.Interfaces
{
    public interface IReactionContext
    {
        void PostReaction(Reaction reaction);
        List<Reaction> GetAllReactions(int questionId);
    }
}
