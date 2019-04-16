using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoToDos.Web.Interfaces;
using ToDoToDos.Web.Models;

namespace ToDoToDos.Web.Data
{
	public class MissionRepository : IMissionRepository
	{
		private readonly ApplicationDbContext _applicationDbContext;

		public MissionRepository(ApplicationDbContext applicationDbContext)
		{
			_applicationDbContext = applicationDbContext;
		}

		public IEnumerable<Mission> Missions => _applicationDbContext.Missions;

		public bool IsExist(int missionId) => _applicationDbContext.Missions
												.Any(m => m.MissionID == missionId);

		public Mission GetMissionById(int missionId) => _applicationDbContext.Missions
														.FirstOrDefault(m => m.MissionID == missionId);

		public async Task<bool> AddMission(Mission mission)
		{
			_applicationDbContext.Missions.Add(mission);

			if (await _applicationDbContext.SaveChangesAsync() > 0)
				return true;

			return false;
		}

		public async Task<bool> UpdateMission(Mission mission)
		{
			_applicationDbContext.Missions.Update(mission);

			if (await _applicationDbContext.SaveChangesAsync() > 0)
				return true;

			return false;
		}

		public async Task<bool> UpdateMissionList(IEnumerable<Mission> missions)
		{
			if (_applicationDbContext.Missions.Any())
			{
				_applicationDbContext.Missions.RemoveRange(Missions);
				await _applicationDbContext.SaveChangesAsync();
			}

			try
			{
				foreach (var mission in missions)
				{
					_applicationDbContext.Missions.Add(new Mission()
					{
						Name = mission.Name,
						Description = mission.Description,
						DateToDo = mission.DateToDo,
						Created = mission.Created,
						IsDone = mission.IsDone,
						Importance = mission.Importance
					});

					_applicationDbContext.SaveChanges();
				}
			}
			catch (DbUpdateException e)
			{
				var error = e.Message;

				if (error != null)
					return false; 
				throw; 
			}

			return true;
		}

		public async Task<bool> RemoveMission(int missionId)
		{
			var dbEntry = _applicationDbContext.Missions.FirstOrDefault(m => m.MissionID == missionId);

			if (dbEntry != null)
				_applicationDbContext.Missions.Remove(dbEntry);

			if (await _applicationDbContext.SaveChangesAsync() > 0)
				return true;

			return false;
		}
	}
}
