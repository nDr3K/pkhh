using PokeSaveRomManager.Parser.Core.Models;

namespace PokeSaveRomManager.Parser.Gen1.RomParser
{
    public class PokedexOrderMap
    {
        private readonly Dictionary<int, int> _internalToPokedex = [];
        private readonly Dictionary<int, int> _pokedexToInternal = [];

        public void Load(byte[] romData, DataSection pokedexOffsets)
        {
            for (int i = 0; i < pokedexOffsets.Count; i++)
            {
                int pokedexNumber = romData[pokedexOffsets.Offset + i];
                int internalId = i;

                _pokedexToInternal[pokedexNumber] = internalId;
                _internalToPokedex[internalId] = pokedexNumber;
            }
        }

        public int? GetPokedexNumber(int internalId)
        {
            if (!_internalToPokedex.TryGetValue(internalId, out var dex))
                throw new KeyNotFoundException($"Internal ID {internalId} not found in Pokédex map.");
            return dex;
        }

        public int GetInternalId(int pokedexNumber)
        {
            if (!_pokedexToInternal.TryGetValue(pokedexNumber, out var id))
                throw new KeyNotFoundException($"Pokédex number {pokedexNumber} not found in internal ID map.");
            return id;
        }
    }

}