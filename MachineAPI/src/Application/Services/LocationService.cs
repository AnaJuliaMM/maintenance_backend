using AutoMapper;
using MachineAPI.Application.Interfaces;
using MachineAPI.Domain.Entities;
using MachineAPI.Domain.Interfaces;
using MachineAPI.API.DTOs;

namespace MachineAPI.Application.Services{
public class LocationService : ILocationService
{
    private readonly ILocationRepository _locationRepository;
    private readonly IMapper _mapper;

    public LocationService(ILocationRepository locationRepository, IMapper mapper)
    {
        _locationRepository = locationRepository;
        _mapper = mapper;
    }

    public async Task<LocationDTO?> GetById(int id)
    {
        var location = await _locationRepository.GetById(id);
        return location != null ? _mapper.Map<LocationDTO>(location) : null;
    }

    public async Task<IEnumerable<LocationDTO>> GetAll()
    {
        var locations = await _locationRepository.GetAll();
        return _mapper.Map<IEnumerable<LocationDTO>>(locations);
    }
}
}