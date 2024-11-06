using MachineAPI.Application.Helpers;
using MachineAPI.Application.Interfaces;
using MachineAPI.Domain.Entities;
using MachineAPI.Domain.Interfaces;
using MachineAPI.API.DTOs;

namespace MachineAPI.Application.Services
{
    public class MachineService : IMachineService
    {
        private readonly IMachineService _machineRepository;

        public MachineService(IMachineRepository machineRepository, ILocationRepository locationRepository, ICategoryRepository categoryRepository)
        {
            _machineRepository = machineRepository;
            _locationRepository = locationRepository;
            _categoryRepository = categoryRepository;

        }

        public async Task<IEnumerable<MachineDTO>> GetAll()
        {
            var machines = await _machineRepository.GetAll();
            return machines.Select(machine => new MachineDTO
            {
                Id = machine.Id,
                SerialNumber = machine.SerialNumber,
                Name = machine.Name,
                ManufactureDate = machine.ManufactureDate,
                Category = new MachineCategoryDTO{
                    Id= machine.Category.Id,
                    Label= machine.Category.Label,
                },
                Location = new MachineLocationDTO{
                    Id= machine.Location.Id,
                    Name= machine.Location.Name,
                    Description= machine.Location.Description,
                }      
            });
        }

        public async Task<MachineDTO> GetById(int id)
        {
            var machine = await _machineRepository.GetById(id);
            if (machine == null) return null;

            return new MachineDTO
            {
                Id = machine.Id,
                SerialNumber = machine.SerialNumber,
                Name = machine.Name,
                ManufactureDate = machine.ManufactureDate,
                Category = new MachineCategoryDTO{
                    Id= machine.Category.Id,
                    Label= machine.Category.Label,
                },
                Location = new MachineLocationDTO{
                    Id= machine.Location.Id,
                    Name= machine.Location.Name,
                    Description= machine.Location.Description,
                }      
            };
        }

        public async Task Add(MachineDTO machineDTO)
        {
            var machine = new Machine
            {
                Id = machineDTO.Id,
                SerialNumber = machineDTO.SerialNumber,
                Name = machineDTO.Name,
                Model = machineDTO.Model,
                ManufactureDate = machineDTO.ManufactureDate,    
            };

            await _machineRepository.Add(machine);

            // Persist Location on database
            var existingLocation = await 

            // Persist Category on database
        }

        public async Task UpdateUser(int id, UserDTO userDTO)
        {

        }

        public async Task DeleteUser(int id)
        {
            
        }
    }
}
