using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoToDos.Web.Models;

namespace ToDoToDos.Web.Interfaces
{
	public interface IMissionRepository
	{
		IEnumerable<Mission> Missions { get; }

		bool IsExist(int missionId);

		Mission GetMissionById(int missionId);

		Task<bool> AddMission(Mission mission);

		Task<bool> UpdateMission(Mission mission);

		Task<bool> RemoveMission(int missionId);

		Task<bool> UpdateMissionList(IEnumerable<Mission> missions);
	}
}
