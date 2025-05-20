using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
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
                .Where(s => s.UserId == userId)
                .CountAsync();
            var saves = await _context.Saves
                .Where(s => s.UserId == userId)
                .AsNoTracking()
                .Select(s => new Save // To avoid loading unnecessary data
                {
                    Id = s.Id,
                    Game = s.Game,
                    Party = new SaveTeam
                    {
                        Id = s.Party.Id,
                        Members = s.Party.Members.Select(m => new SaveTeamMember
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
                    Party = new SaveTeam
                    {
                        Members = s.Party.Members.Select(m => new SaveTeamMember
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

        public async Task<Save> Create(Save save)
        {
            _context.Saves.Add(save);
            await SaveChangesAsync();
            return save;
        }

        public async Task<Save> Update(Save save)
        {
            _context.Saves.Update(save);
            await SaveChangesAsync();
            return save;
        }

        public async Task<PokemonInstance> GetPokemonInstanceByIdAsync(int pokemonInstanceId)
        {
            return await _context.PokemonInstances
                .AsNoTracking()
                .Include(pi => pi.Pokemon)
                .Include(pi => pi.Form)
                .Include(pi => pi.Nature)
                .Include(pi => pi.Ability)
                .Include(pi => pi.Move1)
                .Include(pi => pi.Move2)
                .Include(pi => pi.Move3)
                .Include(pi => pi.Move4)
                .FirstOrDefaultAsync(pi => pi.Id == pokemonInstanceId);
        }

        public async Task<PokemonInstance> CreatePokemon(PokemonInstance pokemonInstance)
        {
            _context.PokemonInstances.Add(pokemonInstance);
            await SaveChangesAsync();
            return pokemonInstance;
        }

        public async Task<PokemonInstance> UpdatePokemon(PokemonInstance pokemonInstance)
        {
            _context.PokemonInstances.Update(pokemonInstance);
            await SaveChangesAsync();
            return pokemonInstance;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
