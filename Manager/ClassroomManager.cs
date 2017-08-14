using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AW_UniversityManagementMvcApp.Gateway;
using AW_UniversityManagementMvcApp.Models.Entity_Models;
using AW_UniversityManagementMvcApp.Models.View_Models;

namespace AW_UniversityManagementMvcApp.Manager
{
    public class ClassroomManager
    {
        ClassroomGateway aClassroomGateway = new ClassroomGateway();

        public List<Classroom> GetAllRooms()
        {
            return aClassroomGateway.GetAllRooms();
        }

        public string Allocate(RoomAllocationVM roomAllocation)
        {
            roomAllocation.Schedule = GenarateSchedule(roomAllocation);
            if (aClassroomGateway.ScheduleExist(roomAllocation))
            {
                return "This Schedule overlaps with another class,Please choose a different time or room";
            }
            else
            {
                int rowAffected = aClassroomGateway.SaveSchedule(roomAllocation);
                if (rowAffected > 0)
                {
                    return "Save Successfull";
                }
                return "Save Failed";
            }
        }

        private string GenarateSchedule(RoomAllocationVM roomAllocation)
        {
            var rooms = GetAllRooms();
            var room = rooms.Find(x => x.Id == roomAllocation.RoomId);
            DateTime dt = roomAllocation.FromTime;
            DateTime dt2 = roomAllocation.ToTime;
            string schedule = "R. No. : " + room.RoomNo + ", " + roomAllocation.Day + ", " + dt.ToString("hh:mm tt") +
                              " - " + dt2.ToString("hh:mm tt");
            return schedule;

        }

        public List<ClassSchedule> GetGetClassScheduleByDepartment(int departmentId)
        {
            return aClassroomGateway.GetGetClassScheduleByDepartment(departmentId);
        }
        public string UnAllocateClassroom()
        {
            int rowAffected = aClassroomGateway.UnassignAllCourses();
            if (rowAffected > 0)
            {
                return "All Classrooms Unallocated";
            }
            return "Classrooms Already Unallocated";
        }   
    }
}