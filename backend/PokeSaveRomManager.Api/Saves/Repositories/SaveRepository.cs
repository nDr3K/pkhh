using Microsoft.EntityFrameworkCore;
using PokeSaveRomManager.Data;
using PokeSaveRomManager.Data.Domain;

namespace PokeSaveRomManager.Api.Saves.Repositories
{
    public class SaveRepository : ISaveRepository
    {
        private readonly PokemonDbContext _context;

        public SaveRepository(PokemonDbContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<Save> Saves, int TotalCount)> GetAllAsync(string userId, int pageNumber, int pageSize)
        {
            var totalCount = await _context.Saves
                .Where(s => s.User.Auth0Id == userId)
                .CountAsync();
            var saves = await _context.Saves
                .Where(s => s.User.Auth0Id == userId)
                .AsNoTracking()
                .Select(s => new Save // To avoid loading unnecessary data
                {
                    Id = s.Id,
                    Game = s.Game,
                    Team = new SaveTeam
                    {
                        Id = s.Team.Id,
                        Name = s.Team.Name,
                        Members = s.Team.Members.Select(m => new SaveTeamMember
                        {
                            Id = m.Id,
                            PokemonInstance = new PokemonInstance
                            {
                                Id = m.PokemonInstance.Id,
                                PokemonId = m.PokemonInstance.PokemonId
                            }
                        }).ToList()
                    }
                })
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (saves, totalCount);
        }

        public async Task<Save> GetByIdAsync(int id)
        {
            return await _context.Saves
                .AsNoTracking()
                .Select(s => new Save
                {
                    Id = s.Id,
                    Game = s.Game,
                    Team = new SaveTeam
                    {
                        Name = s.Team.Name,
                        Members = s.Team.Members.Select(m => new SaveTeamMember
                        {
                            Id = m.Id,
                            PokemonInstance = new PokemonInstance
                            {
                                Pokemon = new Pokemon {
                                    Name = m.PokemonInstance.Pokemon.Name,
                                    DexNumber = m.PokemonInstance.Pokemon.DexNumber,
                                },
                                Form = new PokemonForm
                                {
                                    Name = m.PokemonInstance.Form.Name,
                                    Type1 = m.PokemonInstance.Form.Type1,
                                    Type2 = m.PokemonInstance.Form.Type2
                                },
                                Nature = m.PokemonInstance.Nature,
                                Ability = m.PokemonInstance.Ability,
                                Level = m.PokemonInstance.Level,
                                Move1 = m.PokemonInstance.Move1,
                                Move2 = m.PokemonInstance.Move2,
                                Move3 = m.PokemonInstance.Move3,
                                Move4 = m.PokemonInstance.Move4
                            }
                        }).ToList()
                    }
                })
                .FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}
