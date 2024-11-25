using AutoMapper;
using MachineAPI.API.DTOs;
using MachineAPI.Application.Interfaces;
using MachineAPI.Domain.Entities;
using MachineAPI.Domain.Interfaces;

namespace MachineAPI.Application.Services
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IMapper _mapper;

        public LocationService(ILocationRepository locationRepository, IMapper mapper)
        {
            _locationRepository = locationRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LocationDTO>> GetAll()
        {
            var locations = await _locationRepository.GetAll();
            return _mapper.Map<IEnumerable<LocationDTO>>(locations);
        }

        public async Task<LocationDTO?> GetById(int id)
        {
            var location = await _locationRepository.GetById(id);
            return location != null ? _mapper.Map<LocationDTO>(location) : null;
        }

        public async Task Add(LocationDTO locationDTO)
        {
            Location location = _mapper.Map<Location>(locationDTO);
            await _locationRepository.Add(location);
        }

        public async Task Update(int id, LocationDTO locationDTO)
        {
            Location? location = await _locationRepository.GetById(id);

            if (location == null)
            {
                throw new KeyNotFoundException($"Category with ID {id} not found.");
            }
            _mapper.Map(locationDTO, location);

            await _locationRepository.Update(location);
        }

        public async Task Delete(int id)
        {
            await _locationRepository.Delete(id);
        }
    }
}
