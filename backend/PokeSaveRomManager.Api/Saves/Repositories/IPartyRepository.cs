namespace PokeSaveRomManager.Api.Saves.Repositories
{
    public interface IPartyRepository
    {
        Task DeleteParty(int partyId);
    }
}
