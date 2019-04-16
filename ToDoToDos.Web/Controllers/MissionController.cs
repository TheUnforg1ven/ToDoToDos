using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoToDos.Web.Interfaces;
using ToDoToDos.Web.Models;

namespace ToDoToDos.Web.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class MissionController : ControllerBase
	{
		private readonly IMissionRepository _missionRepository;

		public MissionController(IMissionRepository missionRepository)
		{
			_missionRepository = missionRepository;
		}

		[HttpGet]
		public IEnumerable<Mission> GetMissions()
		{
			return _missionRepository.Missions; 
		}

		[HttpGet("{id}")]
		public Mission GetMission(int id)
		{
			return _missionRepository.GetMissionById(id);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] Mission mission)
		{
			if (mission == null)
				return BadRequest();

			var newMission = await _missionRepository.AddMission(mission);

			if (newMission == false)
				return BadRequest();

			return CreatedAtAction("GetMission", new { id = mission.MissionID }, mission);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, [FromBody] Mission mission)
		{
			if (mission == null || mission.MissionID != id)
				return BadRequest();

			var updatedMission = await _missionRepository.UpdateMission(mission);

			if (updatedMission == false)
				return NotFound();

			return new NoContentResult();
		}

		[HttpPut]
		public async Task<IActionResult> UpdateList([FromBody] IEnumerable<Mission> missions)
		{
			if (missions == null)
				return BadRequest();

			var updatedMission = await _missionRepository.UpdateMissionList(missions);

			if (updatedMission == false)
				return NotFound();

			return new NoContentResult();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var deletedMission = await _missionRepository.RemoveMission(id);

			if (deletedMission == false)
				return NotFound();

			return new NoContentResult();
		}

		private bool MissionExists(int id)
		{
			return _missionRepository.IsExist(id);
		}
	}
}
